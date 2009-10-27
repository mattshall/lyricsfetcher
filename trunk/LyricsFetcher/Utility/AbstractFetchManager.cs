/*
 * AbstractFetchManager - A FetchManager is a thread pool like work manager, that allows
 * songs to be queued for some long running operation.
 *
 * Author: Phillip Piper
 * Date: 2009-03-25 10:51 PM
 *
 * Change log:
 * 2009-03-25  JPP  Initial version
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace LyricsFetcher
{
    /// <summary>
    /// An AbstractFetchManager provides a thread-pool like work manager. Songs can
    /// be queued for some long running operation; those operations can be waited upon, 
    /// or cancelled.
    /// </summary>
    abstract public class AbstractFetchManager
    {
        public AbstractFetchManager() {
        }

        #region Properties

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

        #endregion

        #region Commands

        /// <summary>
        /// Start the process of fetching lyrics
        /// </summary>
        public void Start() {
            this.Paused = false;
        }

        /// <summary>
        /// Add the given collection of songs to those whose lyrics are being fetched
        /// </summary>
        /// <param name="songs"></param>
        public void Queue(IEnumerable<Song> songs) {
            foreach (Song song in songs) {
                // If the song is already being proceeded, ignore it
                if (!this.songStatusMap.ContainsKey(song)) {
                    this.waitingSongs.Add(song);
                    this.QueueInternal(song);
                }
            }
            this.PossibleStartNewThreads();
        }

        /// <summary>
        /// Add the song to the list of songs whose lyrics are being fetched
        /// </summary>
        /// <param name="song"></param>
        public void Queue(Song song) {
            this.Queue(new Song[] { song });
        }

        /// <summary>
        /// After new songs have been added, or old fetches have completed,
        /// possibly start a number of new threads up to our limit of concurrent
        /// fetches.
        /// </summary>
        protected void PossibleStartNewThreads() {
            if (this.Paused)
                return;

            // Prevent race conditions
            lock (this.thisLock) {
                while (this.CountWaiting > 0 && this.CountFetching < this.maxFetching) {
                    this.StartNewThread();
                }
            }
        }
        private Object thisLock = new Object();

        /// <summary>
        /// Start one new process for the first waiting song, removing
        /// that song from the wait queue
        /// </summary>
        protected void StartNewThread() {
            Song song = this.waitingSongs[0];
            this.waitingSongs.RemoveAt(0);
            this.fetchingSongs.Add(song);
            this.StartInternal(song);
        }

        /// <summary>
        /// Cancel the fetching of lyrics of the given song
        /// </summary>
        /// <param name="s"></param>
        public void Cancel(Song song) {
            this.CancelOne(song);
            this.PossibleStartNewThreads();
        }

        /// <summary>
        /// Cancel all fetches
        /// </summary>
        public void CancelAll() {
            foreach (Song song in this.waitingSongs.ToArray())
                this.CancelOne(song);
            foreach (Song song in this.fetchingSongs.ToArray())
                this.CancelOne(song);
        }

        /// <summary>
        /// Cancel the work on the given song
        /// </summary>
        /// <param name="song">The song to be cancelled</param>
        protected void CancelOne(Song song) {
            this.CancelInternal(song);
            this.waitingSongs.Remove(song);
            this.fetchingSongs.Remove(song);
            this.songStatusMap.Remove(song);
        }

        /// <summary>
        /// The given song has finished being worked on. Cleanup its 
        /// resources and start new tasks
        /// </summary>
        /// <param name="song"></param>
        protected void CleanupOne(Song song) {
            this.fetchingSongs.Remove(song);
            this.songStatusMap.Remove(song);
            this.PossibleStartNewThreads();
        }

        /// <summary>
        /// Wait until all lyrics have been fetched
        /// </summary>
        public void WaitUntilFinished() {
            while (this.IsFetching) {
                //System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
        }
        #endregion

        #region Abstract methods

        /// <summary>
        /// Remove the given song from those being operated on.
        /// </summary>
        /// <param name="song">The song to be removed</param>
        protected abstract void CancelInternal(Song song);

        /// <summary>
        /// Add the song to the list of songs for whom some information is being fetched.
        /// </summary>
        /// <param name="song">The song to be queued</param>
        /// <remarks>This should add an entry into the songStatusMap to track
        /// the fetch status of the song</remarks>
        protected abstract void QueueInternal(Song song);

        /// <summary>
        /// Start a long running thread to process the given song
        /// </summary>
        protected abstract void StartInternal(Song song);

        #endregion

        protected List<Song> waitingSongs = new List<Song>();
        protected List<Song> fetchingSongs = new List<Song>();
        protected Hashtable songStatusMap = new Hashtable();

    }
}
