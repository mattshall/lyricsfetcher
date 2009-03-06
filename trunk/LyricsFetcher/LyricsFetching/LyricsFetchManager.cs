/*
 * LyricsFetchManager - Manages the process of fetching lyrics for several songs
 *
 * Author: Phillip Piper
 * Date: 2009-02-07 11:15 AM
 *
 * Change log:
 * 2009-02-28  JPP  Use lock() to prevent race conditions
 * 2009-02-07  JPP  Initial version
 */

using System;
using System.Collections.Generic;
using System.Threading;

namespace LyricsFetcher
{
    /// <summary>
    /// A LyricsFetchManager manages the process of fetching lyrics for several songs.
    /// The fetching is handled asynchronously.
    /// </summary>
    public class LyricsFetchManager
    {
        #region Public Attributes

        /// <summary>
        /// Once lyrics have been fetched, should they be automatically written into
        /// the Song object?
        /// </summary>
        public bool AutoUpdateLyrics {
            get { return this.isAutoUpdateLyrics; }
            set { this.isAutoUpdateLyrics = value; }
        }
        private bool isAutoUpdateLyrics = false;

        /// <summary>
        /// How many songs are currently fetching their lyrics?
        /// </summary>
        public int CountFetching {
            get {
                return this.fetchingSongs.Count;
            }
        }

        /// <summary>
        /// How many songs are waiting to start fetching their lyrics?
        /// </summary>
        public int CountWaiting {
            get {
                return this.waitingSongs.Count;
            }
        }

        /// <summary>
        /// Return true if any lyrics are currently being fetched or are waiting to be fetched.
        /// </summary>
        public bool IsFetching {
            get { 
                return this.CountWaiting > 0 || this.CountFetching > 0;  
            }
        }

        /// <summary>
        /// How many simultaneous threads will be used for fetching lyrics?
        /// </summary>
        public int MaxFetchingThreads {
            get { return this.maxFetching; }
            set { this.maxFetching = Math.Max(1, value); }
        }
        private int maxFetching = 5;

        /// <summary>
        /// Pause the fetching of lyrics. This does not stop existing threads --
        /// it simply prevents new threads from being launched
        /// </summary>
        public bool Paused {
            get { return this.paused; }
            set {
                this.paused = value;
                if (!this.paused)
                    this.PossibleStartNewThreads();
            }
        }
        private bool paused = true;

        /// <summary>
        /// Where will fetcher used by this manager look to find their lyrics?
        /// </summary>
        /// <remarks>Use RegisterSource() to add a new source to the manager</remarks>
        public IList<ILyricsSource> Sources {
            get { return sources; }
        }
        private List<ILyricsSource> sources = new List<ILyricsSource>();

        #endregion

        #region Enquiries

        /// <summary>
        /// What is the status of the fetching of lyrics for the given song?
        /// </summary>
        /// <param name="s"></param>
        /// <returns>This can only return Waiting, Fetching or NotFound.</returns>
        public FetchStatus GetStatus(Song song)
        {
            if (this.songStatusMap.ContainsKey(song))
                return this.songStatusMap[song].Status;
            else
                return FetchStatus.NotFound;
        }

        /// <summary>
        /// Return a textual description of the status of the fetch request
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public string GetStatusString(Song song)
        {
            FetchStatus status = this.GetStatus(song);
            switch (status) {
                case FetchStatus.NotFound:
                    return "Not found";
                case FetchStatus.Fetching:
                    FetchRequestData data = this.songStatusMap[song];
                    if (data != null) {
                        ILyricsSource source = data.Source;
                        if (source != null)
                            return String.Format("Trying {0}...", source.Name);
                    }
                    return "Trying ...";
                default:
                    return status.ToString();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Register the given source so it is used by all subsequent fetches
        /// </summary>
        /// <param name="source">A source for lyrics</param>
        public void RegisterSource(ILyricsSource source)
        {
            this.Sources.Add(source);
        }

        /// <summary>
        /// Start the process of fetching lyrics
        /// </summary>
        public void Start()
        {
            this.Paused = false;
        }

        /// <summary>
        /// Add the given collection of songs to those whose lyrics are being fetched
        /// </summary>
        /// <param name="songs"></param>
        public void Queue(IEnumerable<Song> songs)
        {
            foreach (Song s in songs)
                this.QueueInternal(s);
            this.PossibleStartNewThreads();
        }

        /// <summary>
        /// Add the song to the list of songs whose lyrics are being fetched
        /// </summary>
        /// <param name="song"></param>
        public void Queue(Song song)
        {
            this.QueueInternal(song);
            this.PossibleStartNewThreads();
        }

        /// <summary>
        /// Add the song to the list of songs whose lyrics are being fetched
        /// </summary>
        /// <param name="song"></param>
        private void QueueInternal(Song song)
        {
            // If the song is always being proceeded, ignore it
            if (this.songStatusMap.ContainsKey(song))
                return;

            waitingSongs.Add(song);
            this.songStatusMap[song] = new FetchRequestData();

            FetchStatusEventArgs args = new FetchStatusEventArgs();
            args.Song = song;
            args.Status = FetchStatus.Waiting;
            this.OnStatusEvent(args);
        }

        /// <summary>
        /// After new songs have been added, or old fetches have completed,
        /// possibly start a number of new threads up to our limit of concurrent
        /// fetches.
        /// </summary>
        private void PossibleStartNewThreads()
        {
            if (this.Paused)
                return;

            // Prevent race conditions
            lock (this.thisLock) {
                while (this.CountWaiting > 0 && this.CountFetching < this.maxFetching) {
                    this.StartOneFetch();
                }
            }
        }
        private Object thisLock = new Object();

        private void StartOneFetch()
        {
            Song song = this.waitingSongs[0];
            this.waitingSongs.RemoveAt(0);

            this.fetchingSongs.Add(song);
            this.songStatusMap[song].Status = FetchStatus.Fetching;

            LyricsFetcher fetcher = new LyricsFetcher();
            fetcher.Sources = this.Sources;
            fetcher.StatusEvent += new EventHandler<FetchStatusEventArgs>(fetcher_StatusEvent);

            Thread thread = new Thread(new ParameterizedThreadStart(fetcher.FetchSongLyrics));
            thread.IsBackground = true;
            thread.Start(song);
        }

        /// <summary>
        /// Cancel the fetching of lyrics of the given song
        /// </summary>
        /// <param name="s"></param>
        public void Cancel(Song song)
        {
            this.CancelInternal(song);
            this.PossibleStartNewThreads();
        }

        /// <summary>
        /// Cancel all fetches
        /// </summary>
        public void CancelAll()
        {
            foreach (Song song in this.waitingSongs.ToArray())
                this.CancelInternal(song);
            foreach (Song song in this.fetchingSongs.ToArray())
                this.CancelInternal(song);
        }

        private void CancelInternal(Song song)
        {
            this.waitingSongs.Remove(song);
            this.fetchingSongs.Remove(song);
            this.songStatusMap.Remove(song);

            FetchStatusEventArgs args = new FetchStatusEventArgs();
            args.Song = song;
            args.Status = FetchStatus.Cancelled;
            this.OnStatusEvent(args);
        }

        /// <summary>
        /// Wait until all lyrics have been fetched
        /// </summary>
        public void WaitUntilFinished()
        {
            while (this.IsFetching) {
                //System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
        }

        #endregion

        #region Events

        public event EventHandler<FetchStatusEventArgs> StatusEvent;

        protected virtual void OnStatusEvent(FetchStatusEventArgs args)
        {
            if (this.StatusEvent != null)
                this.StatusEvent(this, args);
        }

        #endregion

        #region Event handlers

        private void fetcher_StatusEvent(object sender, FetchStatusEventArgs e)
        {
            //if (e.Status == FetchStatus.SourceDone && this.GetStatus(e.Song) == FetchStatus.Fetching)
            //    this.RecordAttempt();

            // Remember which source is being checked
            if (e.Status == FetchStatus.Fetching && this.GetStatus(e.Song) == FetchStatus.Fetching)
                this.songStatusMap[e.Song].Source = e.LyricsSource;

            // Is this the final event for a fetch that has not already been cancelled
            bool isFetchingDone = e.Status == FetchStatus.Done && this.GetStatus(e.Song) == FetchStatus.Fetching;
            if (isFetchingDone) {
                this.songStatusMap[e.Song].Status = FetchStatus.Done;
                if (this.AutoUpdateLyrics)
                    this.UpdateLyrics(e.Song, e.Lyrics, e.LyricsSource);
            }

            // Trigger an event while the lyrics fetch has finished but not yet gone
            this.OnStatusEvent(e);

            // Clean up the fetch
            if (isFetchingDone) {
                this.fetchingSongs.Remove(e.Song);
                this.songStatusMap.Remove(e.Song);
                this.PossibleStartNewThreads();
            }
        }

        private void UpdateLyrics(Song s, string lyrics, ILyricsSource source)
        {
            lyrics = lyrics.Trim();
            if (lyrics == "") {
                // If we didn't find lyrics, we only write out a Failed marker, if the songs doesn't
                // have any lyrics or if it only has an old failed marker. 
                // We do NOT want to replace existing lyrics with a failed marker :)
                if (s.LyricsStatus == LyricsStatus.Untried || s.LyricsStatus == LyricsStatus.Failed) {
                    string sources = "";
                    foreach (ILyricsSource x in this.Sources)
                        sources += (x.Name + " ");
                    s.Lyrics = String.Format(
                        "[[LyricsFetcher failed to find lyrics\r\nSources: {1}\r\nDate: {2:yyyy-MM-dd HH:mm:ss}]]",
                        lyrics, sources, DateTime.Now);
                    s.Commit();
                }
            } else {
                s.Lyrics = String.Format(
                    "{0}\r\n\r\n[[Found by LyricsFetcher\r\nSource: {1}\r\nDate: {2:yyyy-MM-dd HH:mm:ss}]]",
                    lyrics, source.Name, DateTime.Now);
                s.Commit();
            }
        }

        #endregion

        private List<Song> waitingSongs = new List<Song>();
        private List<Song> fetchingSongs = new List<Song>();
        private Dictionary<Song, FetchRequestData> songStatusMap = new Dictionary<Song, FetchRequestData>();

        /// <summary>
        /// Instances of this class track the progress of request to fetch lyrics
        /// </summary>
        private class FetchRequestData
        {
            public FetchStatus Status = FetchStatus.Waiting;
            public ILyricsSource Source;

            // THINK: Do we want to track the thread as well?
            //public Thread Thread;
        }
    }
}
