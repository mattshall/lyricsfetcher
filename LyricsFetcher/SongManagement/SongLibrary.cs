/*
 * A SongLibrary holds a collection of Songs and allows queries
 * and commands on those songs.
 * 
 * Author: Phillip Piper
 * Date: 2009-02-05 10:17pm
 * 
 * CHANGE LOG:
 * 2009-02-05 JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LyricsFetcher
{
    /// <summary>
    /// Which media management library are we dealing with
    /// </summary>
    public enum LibraryApplication
    {
        Unknown,
        ITunes,
        WindowsMediaPlayer
    }

    /// <summary>
    ///  A SongDB holds a collection of Songs that can be queried.
    /// </summary>
    /// <remarks>
    /// When loading songs from a library, the slowest part is reading the lyrics.
    /// All other information came from the library index (in both iTunes and WMP),
    /// but the lyrics have to be parsed from the music file itself. For this reason,
    /// we keep a cache of lyrics.
    /// Cold-start iTunes without cache: 2910 songs in 87645ms
    /// Cold-start iTunes with cache: 2910 songs in 41481ms
    /// Warm-start iTunes without cache: 2910 songs in 18629ms
    /// Warm-start iTunes with cache: 2910 songs in 10451ms
    /// 
    /// Cold-start WMP without cache: 2930 songs in 95737ms
    /// Cold-start WMP with cache: 2930 songs in 51924ms
    /// Warm-start WMP without cache: 2930 songs in 20974ms
    /// Warm-start WMP with cache: 2930 songs in 9655ms
    /// </remarks>
    abstract public class SongLibrary
    {
        #region Configuration properties

        /// <summary>
        /// When songs are read from their source, this is maximum number that will be fetched.
        /// 0 indicates all songs will be fetched
        /// </summary>
        public int MaxSongsToFetch {
            get { return this.maxSongsToFetch; }
            set { this.maxSongsToFetch = Math.Max(0, value); }
        }
        private int maxSongsToFetch = 0;

        #endregion

        #region Public properties

        /// <summary>
        /// Get or set the cache that manages our local lyrics
        /// </summary>
        public LyricsCache Cache {
            get {
                if (this.cache == null) {
                    this.cache = new LyricsCache(this.GetType().Name);
                    this.cache.LoadLyricsCache();
                }
                return this.cache;
            }
            set {
                this.cache = value;
            }
        }

        /// <summary>
        /// Have the songs been loaded from the database?
        /// </summary>
        public bool HasSongs {
            get { return this.songs != null; }
        }

        /// <summary>
        /// Is the library currently loading songs?
        /// </summary>
        public bool IsLoading {
            get { return this.loader != null && this.loader.IsRunning; }
        }

        /// <summary>
        /// The list of the songs loaded into the database
        /// </summary>
        public List<Song> Songs {
            get { return this.songs; }
        }
        private List<Song> songs;

        /// <summary>
        /// The list of songs that don't have lyrics but where there is enough information to try
        /// </summary>
        public List<Song> SongsWithoutLyrics {
            get {
                return this.Songs.FindAll(
                    delegate(Song s) {
                        LyricsStatus status = s.LyricsStatus;
                        return (status == LyricsStatus.Untried || status == LyricsStatus.Failed);
                    }
                );
            }
        }

        /// <summary>
        /// The list of songs that are missing either the title or artist
        /// </summary>
        public List<Song> SongsMissingData {
            get {
                return this.Songs.FindAll(
                    delegate(Song s) {
                        return (String.IsNullOrEmpty(s.Title) || 
                            String.IsNullOrEmpty(s.Artist) || 
                            s.Title.StartsWith("Track "));
                    }
                );
            }
        }

        /// <summary>
        /// The list of songs that have never been attempted to be retrieved
        /// </summary>
        public List<Song> UntriedSongs {
            get {
                return this.Songs.FindAll(
                    delegate(Song s) {
                        return s.LyricsStatus == LyricsStatus.Untried;
                    }
                );
            }
        }

        #endregion

        #region Library commands

        /// <summary>
        /// Initialize all events required for this library
        /// </summary>
        abstract public void InitializeEvents();

        /// <summary>
        /// Stop listening for events on this library
        /// </summary>
        abstract public void DeinitializeEvents();

        /// <summary>
        /// Close this library
        /// </summary>
        public virtual void Close() {
            if (this.IsLoading)
                this.CancelLoad();
            this.DeinitializeEvents();
            if (this.cache != null) {
                this.cache.SaveLyricsCache();
                this.cache = null;
            }
        }

        /// <summary>
        /// Discard and delete the lyrics cache
        /// </summary>
        /// <param name="song"></param>
        public void DiscardCache() {
            this.Cache.Discard();
            this.cache = null;
        }

        #endregion

        #region Song commands

        /// <summary>
        /// Is the given song currently playing
        /// </summary>
        /// <param name="song">The song to check</param>
        /// <returns>Is the given song playing</returns>
        abstract public bool IsPlaying(Song song);

        /// <summary>
        /// Start the given song playing
        /// </summary>
        /// <param name="song">The song to play</param>
        abstract public void Play(Song song);

        /// <summary>
        /// Stop playing any song
        /// </summary>
        abstract public void StopPlaying();

        /// <summary>
        /// Cache the lyrics of the given song
        /// </summary>
        /// <param name="song">The song whose lyrics are to be cache</param>
        public void CacheLyrics(Song song) {
            this.Cache.PutLyrics(song);
        }

        #endregion

        #region Song loading

        /// <summary>
        /// Stop the loading of the database. This method waitings until the cancel is complete.
        /// </summary>
        public void CancelLoad() {
            if (this.loader != null) {
                this.loader.Cancel();
                this.loader.Wait();
            }
        }

        /// <summary>
        /// This method starts the process of loading the db with songs.
        /// Call WaitLoad() to wait until the load is complete.
        /// </summary>
        public virtual void LoadSongs() {
            this.loader = this.ChooseSongLoader();

            // Configure the loader
            this.loader.LyricsCache = this.Cache;
            this.loader.MaxSongsToFetch = this.MaxSongsToFetch;
            this.loader.ProgessEvent += new EventHandler<ProgressEventArgs>(loader_ProgessEvent);
            this.loader.DoneEvent += new EventHandler<ProgressEventArgs>(loader_DoneEvent);

            // Let it do its stuff
            this.loader.RunAsync();
        }
        private SongLoader loader;
        private LyricsCache cache;

        /// <summary>
        /// If a load is in progress, wait until it has finished.
        /// </summary>
        public void WaitLoad() {
            if (this.loader != null)
                this.loader.Wait();
        }

        /// <summary>
        /// Which loader will we use to fill our database?
        /// </summary>
        /// <returns>A SongLoader</returns>
        abstract protected SongLoader ChooseSongLoader();

        #endregion

        #region Loader Event Handlers

        /// <summary>
        /// Our song loader has finished. Remember the songs it found
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loader_DoneEvent(object sender, ProgressEventArgs e) {
            // Stop listening for events before doing anything else
            this.loader.ProgessEvent -= new EventHandler<ProgressEventArgs>(loader_ProgessEvent);
            this.loader.DoneEvent -= new EventHandler<ProgressEventArgs>(loader_DoneEvent);

            this.songs = this.loader.Songs;

            this.OnDoneEvent(e);

            this.Cache.SaveLyricsCache();

            this.loader = null;
        }

        /// <summary>
        /// Our song loader is a little closer to finishing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loader_ProgessEvent(object sender, ProgressEventArgs e) {
            this.OnProgressEvent(e);
        }

        #endregion

        #region Events

        /// <summary>
        /// Tell the world that we have finished loading our songs
        /// </summary>
        public event EventHandler<ProgressEventArgs> DoneEvent;

        /// <summary>
        /// Tell the world that a song has started playing
        /// </summary>
        public event EventHandler<EventArgs> PlayEvent;

        /// <summary>
        /// Tell the world that loading has progressed
        /// </summary>
        public event EventHandler<ProgressEventArgs> ProgessEvent;

        /// <summary>
        /// Tell the world that our underlying media application has quit
        /// </summary>
        public event EventHandler<EventArgs> QuitEvent;

        protected virtual void OnDoneEvent(ProgressEventArgs args) {
            if (this.DoneEvent != null)
                this.DoneEvent(this, args);
        }

        protected virtual void OnPlayEvent(EventArgs args) {
            if (this.PlayEvent != null)
                this.PlayEvent(this, args);
        }

        protected virtual void OnProgressEvent(ProgressEventArgs args) {
            if (this.ProgessEvent != null)
                this.ProgessEvent(this, args);
        }

        protected virtual void OnQuitEvent(EventArgs args) {
            if (this.QuitEvent != null)
                this.QuitEvent(this, args);
        }

        #endregion
    }

    /// <summary>
    /// This abstract class handles the reading of songs from a library
    /// and returning a collection of those songs.
    /// </summary>
    public class SongLoader : BackgroundWorkerWithProgress
    {
        /// <summary>
        /// Create a new song loader
        /// </summary>
        public SongLoader() {
            foreach (String kind in Properties.Settings.Default.IgnoredKinds)
                this.KindsToIgnore.Add(kind);
        }

        /// <summary>
        /// The cache used to optimize the fetching of lyrics from the library
        /// </summary>
        public LyricsCache LyricsCache {
            get;
            set;
        }

        /// <summary>
        /// What is the maximum number of songs this loader should read?
        /// </summary>
        public int MaxSongsToFetch {
            get;
            set;
        }

        /// <summary>
        /// The list of songs that the loader loaded
        /// </summary>
        public List<Song> Songs = new List<Song>();

        /// <summary>
        /// Tracks with these kinds will not be included in the library
        /// </summary>
        public IList<string> KindsToIgnore {
            get { return this.kindsToIgnore; }
        }
        private List<string> kindsToIgnore = new List<string>();

        /// <summary>
        /// Add the given song to our list of loaded songs, reading
        /// the lyrics of the song from the cache if possible.
        /// </summary>
        /// <param name="song">The newly loaded song</param>
        protected void AddSong(Song song) {
            // If we couldn't get the lyrics of the song from the cache, load
            // them from the songs source itself.
            if (this.LyricsCache == null)
                song.GetLyrics();
            else {
                if (!this.LyricsCache.UpdateLyrics(song)) {
                    song.GetLyrics();
                    this.LyricsCache.PutLyrics(song);
                }
            }

            this.Songs.Add(song);
        }
    }
}
