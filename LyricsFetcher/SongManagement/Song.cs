/*
 * A Song represents a single track within a media library.
 *
 * Author: Phillip Piper
 * Date: 8/01/2008 4:28 PM
 *
 * CHANGE LOG:
 * 2009-02-15 JPP  Removed Kind as a visible property
 * 2008-01-07 JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace LyricsFetcher
{
    /// <summary>
    /// This enum represents the status of lyrics for a song.
    /// </summary>
    public enum LyricsStatus {
        Success,
        Failed,
        GenreIgnored,
        DataMissing,
        Untried
    }

    /// <summary>
    /// A Song represents a single track within a media library.
    /// </summary>
    abstract public class Song
    {
        #region Constructors

        public Song()
        {
        }

        public Song(string title, string artist, string album, string genre)
        {
            this.Title = title;
            this.Artist = artist;
            this.Album = album;
            this.Genre = genre;
        }

        #endregion

        #region Public properties

        public string Album;
        public string Artist;
        public string Genre;
        public string Lyrics;
        public string Title;

        /// <summary>
        /// Return an enum indicating whether the lyrics of this song have been fetched
        /// </summary>
        public LyricsStatus LyricsStatus {
            get {
                if (!String.IsNullOrEmpty(this.Lyrics)) {
                    if (this.Lyrics.StartsWith("Failed") || this.Lyrics.StartsWith("[[LyricsFetcher failed"))
                        return LyricsStatus.Failed;
                    else
                        return LyricsStatus.Success;
                }
                if (Song.IsGenreIgnored(this.Genre))
                    return LyricsStatus.GenreIgnored;

                if (String.IsNullOrEmpty(this.Title) || String.IsNullOrEmpty(this.Artist))
                    return LyricsStatus.DataMissing;

                if (this.Title.StartsWith("Track ") || this.Title.StartsWith("Faixa "))
                    return LyricsStatus.DataMissing;

                return LyricsStatus.Untried;
            }
        }

        /// <summary>
        /// Return a string indicating the status of the lyrics of this song
        /// </summary>
        public string LyricsStatusString {
            get {
                switch (this.LyricsStatus) {
                    case LyricsStatus.Success:          return "Success";
                    case LyricsStatus.Failed:           return "Failed";
                    case LyricsStatus.GenreIgnored:     return "Genre ignored";
                    case LyricsStatus.DataMissing:      return "Missing Data";
                    case LyricsStatus.Untried:          return "Untried";
                }
                return "Unknown";
            }
        }

        #endregion

        #region Converters

        public override string ToString() {
            return string.Format("[Song Title={0} Artist={1} Album={2}]", this.Title, this.Artist, this.Album);
        }

        #endregion

        #region Commands

        abstract public void Commit();

        /// <summary>
        /// Songs should not load lyrics from their source until this method is called.
        /// Specifically, lyrics should not be loaded in the constructor.
        /// </summary>
        abstract public void GetLyrics();

        #endregion

        #region Static DB type methods

        /// <summary>
        /// Each string in this list represents a genre for which lyrics should
        /// never be fetched.
        /// </summary>
        static public List<string> GenresToIgnore = new List<String>();

        static public bool IsGenreIgnored(string genre)
        {
            return Song.GenresToIgnore.Contains(genre);
        }

        #endregion
    }
}
