/*
 * This file holds the class implementations necessary to interact with iTunes.
 *
 * Author: Phillip Piper
 * Date: 2009-03-14 8:28 AM
 *
 * CHANGE LOG:
 * 2009-03-14 JPP  Separated from iTunesLibrary.cs
 */

using System;
using System.Runtime.InteropServices;
using iTunesLib;

namespace LyricsFetcher
{

    /// <summary>
    /// This class implements a Song object based on iTunes track
    /// </summary>
    public class ITunesSong : Song
    {
        /// <summary>
        /// Build a Song from the information available in the iTunes xml file
        /// </summary>
        /// <param name="title"></param>
        /// <param name="artist"></param>
        /// <param name="album"></param>
        /// <param name="genre"></param>
        /// <param name="persistentId"></param>
        public ITunesSong(string title, string artist, string album, string genre, string persistentId) :
            base(title, artist, album, genre) {
            this.persistentIdHigh = Convert.ToInt32(persistentId.Substring(0, 8), 16);
            this.persistentIdLow = Convert.ToInt32(persistentId.Substring(8, 8), 16);
        }

        /// <summary>
        /// Build a Song from a given iTunes Track object
        /// </summary>
        /// <param name="track"></param>
        public ITunesSong(IITTrack track) :
            base(track.Name, track.Artist, track.Album, track.Genre) {
            track.GetITObjectIDs(out this.sourceId, out this.playlistId, out this.trackId, out this.databaseId);
        }

        /// <summary>
        /// Get the IITrack com object that is this song in the iTunes library
        /// </summary>
        public IITTrack Track {
            get {
                // If we don't have a database id, we must have persistent ids. In that case
                // we get the object from its persistent ids, and the fetch the full four ids.
                // Getting an object by its ids is about 4x faster than fetch it by persistent ids
                if (this.databaseId == -1) {
                    IITTrack track = ITunes.Instance.GetTrackByPersistentIds(this.persistentIdHigh, this.persistentIdLow);
                    if (track != null)
                        track.GetITObjectIDs(out this.sourceId, out this.playlistId, out this.trackId, out this.databaseId);
                    return track;
                } else {
                    return ITunes.Instance.GetObjectByIds(this.sourceId,
                        this.playlistId, this.trackId, this.databaseId) as IITTrack;
                }
            }
        }

        /// <summary>
        /// Read the lyrics for this song from their underlying source.
        /// </summary>
        /// <remarks>This normally has to open and read the underlying media
        /// file, so this is a slow operation.</remarks>
        public override void GetLyrics() {
            IITFileOrCDTrack fileTrack = this.Track as IITFileOrCDTrack;
            if (fileTrack != null) {
                try {
                    this.Lyrics = fileTrack.Lyrics;
                }
                catch (COMException) {
                    // If the file is corrupt, missing or just plain obstinate, this can fail.
                }
            }
        }

        /// <summary>
        /// Write any changes to this song out to the music library.
        /// </summary>
        public override void Commit() {
            IITTrack track = this.Track;
            if (track == null)
                return;

            try {
                if (this.Title != null && track.Name != this.Title)
                    track.Name = this.Title;
                if (this.Artist != null && track.Artist != this.Artist)
                    track.Artist = this.Artist;
                if (this.Album != null && track.Album != this.Album)
                    track.Album = this.Album;
                if (this.Genre != null && track.Genre != this.Genre)
                    track.Genre = this.Genre;

                IITFileOrCDTrack fileTrack = track as IITFileOrCDTrack;
                if (fileTrack != null && this.Lyrics != null)
                    fileTrack.Lyrics = this.Lyrics;
            }
            catch (COMException) {
                // There are quite a few reasons why these might fail. If the track
                // is locked, or the underlying file has been deleted/moved.
                // There is nothing we can do if this fails.
            }
        }

        // If we have read this song from the XML, these are used to temporarily identify the
        // song in the library. The first time we need the IITrack object, these will be translated
        // to the other 4-tuple of ids.
        private int persistentIdHigh;
        private int persistentIdLow;

        // This 4-tuple uniquely identifies a track within a music library. Their values are not
        // persistent.
        private int sourceId;
        private int playlistId;
        private int trackId;
        private int databaseId = -1;
    }
}
