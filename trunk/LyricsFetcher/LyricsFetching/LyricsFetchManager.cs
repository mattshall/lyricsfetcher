/*
 * LyricsFetchManager - Manages the process of fetching lyrics for several songs
 *
 * Author: Phillip Piper
 * Date: 2009-02-07 11:15 AM
 *
 * Change log:
 * 2009-03-31  JPP  Catch exceptions raised by Song.Commit()
 * 2009-03-26  JPP  Update failed lyrics marker unless the song already has lyrics
 * 2009-03-25  JPP  Split out useful base class, AbstractFetchManager
 * 2009-02-28  JPP  Use lock() to prevent race conditions
 * 2009-02-07  JPP  Initial version
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace LyricsFetcher
{
    /// <summary>
    /// A LyricsFetchManager manages the process of fetching lyrics for several songs.
    /// The fetching is handled asynchronously.
    /// </summary>
    public class LyricsFetchManager : AbstractFetchManager
    {
        #region Public Attributes

        /// <summary>
        /// Once lyrics have been fetched, should they be automatically written into
        /// the Song object?
        /// </summary>
        public bool AutoUpdateLyrics { get; set; }

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
        public LyricsFetchStatus GetStatus(Song song)
        {
            if (this.songStatusMap.ContainsKey(song))
                return this.GetFetchRequestData(song).Status;
            else
                return LyricsFetchStatus.NotFound;
        }
        
        /// <summary>
        /// Return a textual description of the status of the fetch request
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public string GetStatusString(Song song)
        {
            LyricsFetchStatus status = this.GetStatus(song);
            switch (status) {
                case LyricsFetchStatus.NotFound:
                    return "Not found";
                case LyricsFetchStatus.Fetching:
                    FetchRequestData data = this.GetFetchRequestData(song);
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

        #endregion

        #region Implementation
        
        /// <summary>
        /// Remove the given song from those being operated on.
        /// </summary>
        /// <param name="song">The song to be removed</param>
        protected override void CancelInternal(Song song)
        {
            LyricsFetchStatusEventArgs args = new LyricsFetchStatusEventArgs();
            args.Song = song;
            args.Status = LyricsFetchStatus.Cancelled;
            this.OnStatusEvent(args);
        }
        
        private FetchRequestData GetFetchRequestData(Song song) {
            return ((FetchRequestData)this.songStatusMap[song]);
        }

        /// <summary>
        /// Add the song to the list of songs whose lyrics are being fetched
        /// </summary>
        /// <param name="song"></param>
        protected override void QueueInternal(Song song)
        {
            this.songStatusMap[song] = new FetchRequestData();

            LyricsFetchStatusEventArgs args = new LyricsFetchStatusEventArgs();
            args.Song = song;
            args.Status = LyricsFetchStatus.Waiting;
            this.OnStatusEvent(args);
        }
        
        protected override void StartInternal(Song song)
        {
            this.GetFetchRequestData(song).Status = LyricsFetchStatus.Fetching;

            LyricsFetcher fetcher = new LyricsFetcher();
            fetcher.Sources = this.Sources;
            fetcher.StatusEvent += new EventHandler<LyricsFetchStatusEventArgs>(fetcher_StatusEvent);

            Thread thread = new Thread(new ParameterizedThreadStart(fetcher.FetchSongLyrics));
            thread.IsBackground = true;
            thread.Start(song);
        }

        #endregion

        #region Events

        public event EventHandler<LyricsFetchStatusEventArgs> StatusEvent;

        protected virtual void OnStatusEvent(LyricsFetchStatusEventArgs args)
        {
            if (this.StatusEvent != null)
                this.StatusEvent(this, args);
        }

        #endregion

        #region Event handlers

        private void fetcher_StatusEvent(object sender, LyricsFetchStatusEventArgs e)
        {
            //if (e.Status == FetchStatus.SourceDone && this.GetStatus(e.Song) == FetchStatus.Fetching)
            //    this.RecordAttempt();

            // Remember which source is being checked
            if (e.Status == LyricsFetchStatus.Fetching && this.GetStatus(e.Song) == LyricsFetchStatus.Fetching)
                this.GetFetchRequestData(e.Song).Source = e.LyricsSource;

            // Is this the final event for a fetch that has not already been cancelled
            bool isFetchingDone = e.Status == LyricsFetchStatus.Done && this.GetStatus(e.Song) == LyricsFetchStatus.Fetching;
            if (isFetchingDone) {
                this.GetFetchRequestData(e.Song).Status = LyricsFetchStatus.Done;
                if (this.AutoUpdateLyrics) {
                    try {
                        this.UpdateLyrics(e.Song, e.Lyrics, e.LyricsSource);
                    }
                    catch (COMException) {
                        // Something went wrong
                        //TODO: Figure out a way to report this to the user
                    }
                }
            }

            // Trigger an event while the lyrics fetch has finished but not yet gone
            this.OnStatusEvent(e);

            // Clean up the fetch
            if (isFetchingDone) {
                this.CleanupOne(e.Song);
            }
        }

        private void UpdateLyrics(Song s, string lyrics, ILyricsSource source)
        {
            lyrics = lyrics.Trim();
            if (lyrics == "") {
                // If we didn't find lyrics, we only write out a Failed marker, if the songs doesn't
                // have any lyrics or if it only has an old failed marker. 
                // We do NOT want to replace existing lyrics with a failed marker :)
                if (s.LyricsStatus != LyricsStatus.Success) {
                    string sources = "";
                    foreach (ILyricsSource x in this.Sources)
                        sources += (x.Name + " ");
                    s.Lyrics = String.Format(
                        "[[LyricsFetcher failed to find lyrics\r\nSources: {1}\r\nDate: {2:yyyy-MM-dd HH:mm:ss}]]",
                        lyrics, sources, DateTime.Now);
                    try {
                        s.Commit();
                    }
                    catch (COMException) {
                        // There are quite a few reasons why these might fail. If the track
                        // is locked, or the underlying file has been deleted/moved.
                        // There is nothing we can do if this fails.
                    }
                }
            } else {
                s.Lyrics = String.Format(
                    "{0}\r\n\r\n[[Found by LyricsFetcher\r\nSource: {1}\r\nDate: {2:yyyy-MM-dd HH:mm:ss}]]",
                    lyrics, source.Name, DateTime.Now);
                try {
                    s.Commit();
                }
                catch (COMException) {
                    // There are quite a few reasons why these might fail. If the track
                    // is locked, or the underlying file has been deleted/moved.
                    // There is nothing we can do if this fails.
                }
            }
        }

        #endregion


        /// <summary>
        /// Instances of this class track the progress of request to fetch lyrics
        /// </summary>
        private class FetchRequestData
        {
            public LyricsFetchStatus Status = LyricsFetchStatus.Waiting;
            public ILyricsSource Source;

            // THINK: Do we want to track the thread as well?
            //public Thread Thread;
        }
    }
}
