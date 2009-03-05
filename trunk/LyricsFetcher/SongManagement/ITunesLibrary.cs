/*
 * This file holds the class implementations necessary to interact with iTunes.
 *
 * Author: Phillip Piper
 * Date: 8/01/2008 4:28 PM
 *
 * CHANGE LOG:
 * 2009-02-28 JPP  - Load KindsToIgnore from Settings
 *                 - Use of Fast loader is now configurable
 * 2009-02-15 JPP  Filter out non-song kinds in SongLoader
 * 2008-01-08 JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.XPath;
using System.IO;

using iTunesLib;

namespace LyricsFetcher
{
    /// <summary>
    /// The class implements a SongLibrary based on the iTunes music library
    /// </summary>
    public class ITunesLibrary : SongLibrary
    {
        protected override SongLoader ChooseSongLoader( ) {
            if (Properties.Settings.Default.UseFastITunesLoader)
                return new FastITunesSongLoader();
            else
                return new ITunesSongLoader();
        }

        public override bool IsPlaying(Song song) {
            ITunesSong itSong = song as ITunesSong;
            if (itSong != null && itSong.Track != null)
                return ITunes.Instance.IsTrackPlaying(itSong.Track);
            else
                return false;
        }

        public override void Play(Song song) {
            ITunesSong itSong = song as ITunesSong;
            if (itSong != null && itSong.Track != null)
                itSong.Track.Play();
        }

        public override void StopPlaying() {
            ITunes.Instance.Stop();
        }

        #region Event handlers

        public override void InitializeEvents() {
            ITunes.Instance.Application.OnPlayerPlayEvent += new _IiTunesEvents_OnPlayerPlayEventEventHandler(ITunes_OnPlayerPlayEvent);
            ITunes.Instance.Application.OnPlayerPlayingTrackChangedEvent += new _IiTunesEvents_OnPlayerPlayingTrackChangedEventEventHandler(ITunes_OnPlayerPlayingTrackChangedEvent);
            ITunes.Instance.Application.OnPlayerStopEvent += new _IiTunesEvents_OnPlayerStopEventEventHandler(ITunes_OnPlayerStopEvent);
            ITunes.Instance.Application.OnQuittingEvent += new _IiTunesEvents_OnQuittingEventEventHandler(ITunes_OnQuittingEvent);
        }

        public override void DeinitializeEvents() {
            ITunes.Instance.Application.OnPlayerPlayEvent -= new _IiTunesEvents_OnPlayerPlayEventEventHandler(ITunes_OnPlayerPlayEvent);
            ITunes.Instance.Application.OnPlayerPlayingTrackChangedEvent -= new _IiTunesEvents_OnPlayerPlayingTrackChangedEventEventHandler(ITunes_OnPlayerPlayingTrackChangedEvent);
            ITunes.Instance.Application.OnPlayerStopEvent -= new _IiTunesEvents_OnPlayerStopEventEventHandler(ITunes_OnPlayerStopEvent);
            ITunes.Instance.Application.OnQuittingEvent -= new _IiTunesEvents_OnQuittingEventEventHandler(ITunes_OnQuittingEvent);
        }

        void ITunes_OnPlayerStopEvent(object iTrack) {
            this.OnPlayEvent(new EventArgs());
        }

        void ITunes_OnPlayerPlayingTrackChangedEvent(object iTrack) {
            this.OnPlayEvent(new EventArgs());
        }

        void ITunes_OnPlayerPlayEvent(object iTrack) {
            this.OnPlayEvent(new EventArgs());
        }

        void ITunes_OnQuittingEvent()
        {
            this.OnQuitEvent(new EventArgs());
            ITunes.Instance.Release();
        }

        #endregion
    }

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
            base(title, artist, album, genre) 
        {
            this.persistentIdHigh = Convert.ToInt32(persistentId.Substring(0, 8), 16);
            this.persistentIdLow = Convert.ToInt32(persistentId.Substring(8, 8), 16);
        }

        /// <summary>
        /// Build a Song from a given iTunes Track object
        /// </summary>
        /// <param name="track"></param>
        public ITunesSong(IITTrack track) :
            base(track.Name, track.Artist, track.Album, track.Genre) 
        {
            track.GetITObjectIDs(out this.sourceId, out this.playlistId, out this.trackId, out this.databaseId);
        }

        /// <summary>
        /// Get the IITrack com object that is this song in the iTunes library
        /// </summary>
        public IITTrack Track 
        {
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
        public override void  GetLyrics()
        {
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


    public class ITunesSongLoader : SongLoader
    {
        /// <summary>
        /// Do the actual work of fetching the lyrics from the library
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Ignored</returns>
        protected override object DoWork(DoWorkEventArgs e) {
            try {
                IITTrackCollection tracks = ITunes.Instance.AllTracks;

                // How many tracks are there and how many songs should we fetch?
                int trackCount = tracks.Count;
                int maxSongs = trackCount;
                if (this.MaxSongsToFetch > 0)
                    maxSongs = Math.Min(trackCount, this.MaxSongsToFetch);

                this.ReportProgress(0, "Gettings songs...");
                for (int i = 1; i <= trackCount && this.Songs.Count < maxSongs && this.CanContinueRunning; i++) {

                    IITTrack track = tracks[i];
                    if (track.Kind == ITTrackKind.ITTrackKindFile)
                        if (!this.KindsToIgnore.Contains(track.KindAsString))
                            this.AddSong(new ITunesSong(track));

                    this.ReportProgress((i * 100) / maxSongs);
                }
            }
            catch (COMException ex) {
                // If the server died or stalled during the load, just ignore it
                if (!(((uint)ex.ErrorCode) == 0x80010007 || ((uint)ex.ErrorCode) == 0x8001010A))
                    throw ex;
            }
            return true;
        }
    }

    public class FastITunesSongLoader : SongLoader
    {
        /// <summary>
        /// Do the actual work of fetching the lyrics from the library
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Ignored</returns>
        protected override object DoWork(DoWorkEventArgs e) {

            // How many tracks are there and how many songs should we fetch?
            int maxSongs = ITunes.Instance.AllTracks.Count;
            if (this.MaxSongsToFetch > 0)
                maxSongs = Math.Min(maxSongs, this.MaxSongsToFetch);

            // Load the whole xml file into memory, and then remove the DTD.
            // We remove that so we can read the xml even when not connected to the network
            string xml = File.ReadAllText(ITunes.Instance.XmlPath);
            int docTypeStart = xml.IndexOf("<!DOCTYPE");
            const string endOfDtdMarker = "0.dtd\">";
            int docTypeEnd = xml.IndexOf(endOfDtdMarker) + endOfDtdMarker.Length;
            string xml2 = xml.Remove(docTypeStart, docTypeEnd - docTypeStart);

            XPathDocument doc = new XPathDocument(new StringReader(xml2));
            XPathNavigator nav = doc.CreateNavigator();

            // Move to plist, then to master library, than tracks, then first track
            nav.MoveToChild("plist", "");
            nav.MoveToChild("dict", "");
            nav.MoveToChild("dict", "");
            bool success = nav.MoveToChild("dict", "");
            Dictionary<string, string> data = new Dictionary<string, string>();

            // Read each song until we have enough or no more
            while (success && this.Songs.Count < maxSongs && this.CanContinueRunning) {
                success = nav.MoveToFirstChild();

                // Read each piece of information about the song
                data.Clear();
                while (success) {
                    string key = nav.Value;
                    success = nav.MoveToNext();
                    string value = nav.Value;
                    data[key] = value;
                    success = nav.MoveToNext();
                }

                // Create and add the song if it's not one we want to ignore
                if (data.Count > 0 && !this.KindsToIgnore.Contains(this.GetValue(data, "Kind"))) {
                    ITunesSong song = new ITunesSong(
                        this.GetValue(data, "Name"), 
                        this.GetValue(data, "Artist"),
                        this.GetValue(data, "Album"),
                        this.GetValue(data, "Genre"),
                        this.GetValue(data, "Persistent ID"));
                    this.AddSong(song);
                    this.ReportProgress((this.Songs.Count * 100) / maxSongs);
                }
                success = nav.MoveToParent();
                success = nav.MoveToNext("dict", "");
            }

            return true;
        }

        private string GetValue(Dictionary<string, string> data, string key) {
            string value;
            if (data.TryGetValue(key, out value))
                return value;
            else
                return "";
        }
    }
}
