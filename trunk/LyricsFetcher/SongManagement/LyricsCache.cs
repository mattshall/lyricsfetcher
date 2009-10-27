/*
 * This class manages a cache of previously collected lyrics.
 *
 * These lyrics are cached from the songs in the library itself. It is
 * not a cache of lyrics fetched from the web.
 *
 * The slowest part of loading songs from the library is fetching the 
 * lyrics. Other information is stored in the library data about each track
 * but lyrics are stored in the music file itself. So to get the lyrics
 * for a given song, the library has to open the music file and parse it
 * looking for lyrics. 
 * 
 * The cache ensures that that costly process is only done once for each 
 * track.
 * 
 * Author: Phillip Piper
 * Date: 2009-02-09 4:28 PM
 *
 * CHANGE LOG:
 * 2009-03-30 JPP  - Added RemoveLyrics()
 * 2009-03-06 JPP  - Added Selector to cache
 * 2009-02-26 JPP  - Added HasLyrics().
 *                 - null and empty strings are now valid values for lyrics
 * 2009-02-09 JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LyricsFetcher
{
    /// <summary>
    /// Instances of this class manage a cache of previously seen lyrics.
    /// </summary>
    public class LyricsCache
    {
        #region Life and death

        public LyricsCache() {
            this.Selector = "";
        }

        public LyricsCache(string selector) {
            this.Selector = selector;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Get or set an extra selector that uniquely idenitifies this cache
        /// </summary>
        public string Selector {
            get;
            set;
        }

        #endregion

        #region Cache interface

        /// <summary>
        /// Update the lyrics of the given song with its matched
        /// value from the cache.
        /// </summary>
        /// <param name="song">The song whose lyrics are to be updated</param>
        /// <returns>Returns true if the lyrics were updated</returns>
        public bool UpdateLyrics(Song song) {
            if (!this.HasLyrics(song))
                return false;

            song.Lyrics = this.GetLyrics(song);
            return true;
        }

        /// <summary>
        /// Get the lyrics for the given song from the cache.
        /// </summary>
        /// <param name="song">The Song whose lyrics are to be returned</param>
        /// <returns>The lyrics or null if the lyrics aren't found.</returns>
        public string GetLyrics(Song song) {
            string lyrics;
            this.lyricsCache.TryGetValue(this.GetKey(song), out lyrics);
            return lyrics;
        }

        /// <summary>
        /// Does the cache have lyrics for the given song.
        /// </summary>
        /// <param name="song">The Song whose lyrics are to be returned</param>
        /// <returns>The lyrics or null if the lyrics aren't found.</returns>
        public bool HasLyrics(Song song) {
            return this.lyricsCache.ContainsKey(this.GetKey(song));
        }

        /// <summary>
        /// Remember the lyrics of the given song.
        /// </summary>
        /// <param name="song">The Song whose lyrics are to be remembered</param>
        public void PutLyrics(Song song) {
            this.lyricsCache[this.GetKey(song)] = song.Lyrics;
        }

        /// <summary>
        /// Remove the lyrics of the given song from the cache
        /// </summary>
        /// <param name="song">The Song whose lyrics are to be removed</param>
        public void RemoveLyrics(Song song) {
            this.lyricsCache.Remove(this.GetKey(song));
        }

        #endregion

        #region Load and Store

        /// <summary>
        /// Delete the lyrics cache
        /// </summary>
        public void Discard() {
            File.Delete(this.GetLyricsCachePath());
        }

        /// <summary>
        /// Load the lyrics cache from its normal location
        /// </summary>
        public void LoadLyricsCache() {
            this.LoadLyricsCache(this.GetLyricsCachePath());
        }

        /// <summary>
        /// Save the lyrics cache to its normal location
        /// </summary>
        public void SaveLyricsCache() {
            this.SaveLyricsCache(this.GetLyricsCachePath());
        }

        /// <summary>
        /// Load this lyrics cache from the given path.
        /// </summary>
        /// <remarks>This discards any previous cache, even if the load fails</remarks>
        /// <param name="path">The full path name to a file created by
        /// SaveLyricsCache</param>
        public void LoadLyricsCache(string path) {
            this.lyricsCache = new Dictionary<string, string>();

            // Can't do anything more if the file doesn't exist
            if (!File.Exists(path))
                return;

            BinaryFormatter deserializer = new BinaryFormatter();
            using (FileStream fs = File.OpenRead(path)) {
                try {
                    this.lyricsCache = deserializer.Deserialize(fs) as Dictionary<string, string>;
                }
                catch (System.Runtime.Serialization.SerializationException ex) {
                    System.Diagnostics.Debug.WriteLine("LoadLyricsCache failed");
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// Save this cache to the given path
        /// </summary>
        /// <remarks>Any existing saved cached will be deleted.</remarks>
        /// <param name="path">The full path name of the file cache</param>
        public void SaveLyricsCache(string path) {
            // Make the directory for the cache if it doesn't exist
            if (!File.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            // Remove any existing cache
            if (File.Exists(path))
                File.Delete(path);

            // Store the cache as a binary stream
            using (FileStream fs = File.Create(path)) {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(fs, this.lyricsCache);
            }
        }

        #endregion

        /// <summary>
        /// Calculate a key that can be used to identify the lyrics of this song
        /// </summary>
        /// <param name="song">The song whose cache key is required</param>
        /// <returns>A key</returns>
        private string GetKey(Song song) {
            return String.Format("{0}\\{1}", song.Title, song.Artist);
        }

        /// <summary>
        /// Return a full path to the cache
        /// </summary>
        /// <returns>Return a full path to the cache</returns>
        private string GetLyricsCachePath() {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = Path.Combine(path, Properties.Resources.AppName);
            path = Path.Combine(path, String.Format("LyricsCache{0}.bin", this.Selector ?? ""));
            return path;
        }

        private Dictionary<string, string> lyricsCache = new Dictionary<string, string>();
    }
}
