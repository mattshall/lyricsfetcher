/*
 * LyricsFetcher - Handle the fetching of lyrics for a single song
 *
 * Author: Phillip Piper
 * Date: 2009-02-07 11:15 AM
 *
 * Change log:
 * 2009-02-071  JPP  Initial version
 */

using System;
using System.Collections.Generic;

namespace LyricsFetcher
{
    /// <summary>
    /// What is the status of the fetch operation?
    /// </summary>
    public enum FetchStatus
    {
        Undefined = 0,
        NotFound,
        Waiting,
        Fetching,
        SourceDone,
        Done,
        Cancelled
    }

    /// <summary>
    /// The information available in a FetchStatusEvent
    /// </summary>
    public class FetchStatusEventArgs : EventArgs
    {
        /// <summary>
        /// What is the fetch status of this song?
        /// </summary>
        public FetchStatus Status = FetchStatus.Undefined;
        
        /// <summary>
        /// What song is the event for?
        /// </summary>
        public Song Song;
        
        /// <summary>
        /// What lyrics source was involved?
        /// </summary>
        public ILyricsSource LyricsSource;

        /// <summary>
        /// Were the lyrics actually found?
        /// </summary>
        /// <remarks>
        /// This is only valid when Status == FetchStatus.SourceDone
        /// </remarks>
        public bool LyricsFound;
        
        /// <summary>
        /// How many milliseconds elapsed in searching the given source
        /// </summary>
        public int ElapsedTime;

        /// <summary>
        /// What were the lyrics
        /// </summary>
        /// <remarks>
        /// This is only valid when Status == FetchStatus.SourceDone
        /// </remarks>
        public string Lyrics;
    }

    /// <summary>
    /// A LyricsFetcher handles a single attempt to fetch the lyrics of a given
    /// song. It is designed to be run as a background task, using FetchSongLyrics
    /// as the thread entry point. Progress, success and failure are all reported
    /// through the StatusEvent event.
    /// </summary>
    public class LyricsFetcher
    {
        /// <summary>
        /// Get or set where this fetcher look to find its lyrics?
        /// </summary>
        public IList<ILyricsSource> Sources
        {
            get { return this.sources; }
            set { this.sources = value; }
        }
        
        private IList<ILyricsSource> sources = new List<ILyricsSource>();

        /// <summary>
        /// This the main thread entry point
        /// </summary>
        /// <param name="song"></param>
        public void FetchSongLyrics(object param)
        {
            Song song = (Song)param;

            this.StartFetch(song);
            string lyrics = this.DoFetch(song);
            this.FinishFetch(lyrics);
        }

        private void StartFetch(Song song)
        {
            this.args.Song = song;
        }

        private string DoFetch(Song song)
        {
            string lyrics = String.Empty;

            foreach (ILyricsSource source in this.Sources) {
                this.StartSource(source);

                int startTickCount = Environment.TickCount;
                lyrics = source.GetLyrics(song);
                this.FinishSource(source, lyrics, Environment.TickCount - startTickCount);

                if (lyrics != String.Empty)
                    return lyrics;
            }

            return String.Empty;
        }

        private void StartSource(ILyricsSource source)
        {
            this.args.Status = FetchStatus.Fetching;
            this.args.LyricsSource = source;
            this.OnStatusEvent(args);
        }

        private void FinishSource(ILyricsSource source, string lyrics, int elapsedTime)
        {
            this.args.Status = FetchStatus.SourceDone;
            this.args.LyricsSource = source;
            this.args.Lyrics = lyrics;
            this.args.ElapsedTime = elapsedTime;
            this.OnStatusEvent(args);
        }

        private void FinishFetch(string lyrics)
        {
            this.args.Status = FetchStatus.Done;
            this.args.Lyrics = lyrics;
            this.OnStatusEvent(args);
        }

        #region Events

        /// <summary>
        /// Signal that the fetch status may have changed
        /// </summary>
        public event EventHandler<FetchStatusEventArgs> StatusEvent;

        protected virtual void OnStatusEvent(FetchStatusEventArgs args)
        {
            if (this.StatusEvent != null)
                this.StatusEvent(this, args);
        }

        #endregion

        #region Private variables

        private FetchStatusEventArgs args = new FetchStatusEventArgs();

        #endregion
    }
}
