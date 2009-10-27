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
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.XPath;
using iTunesLib;

namespace LyricsFetcher
{
    /// <summary>
    /// The class implements a SongLibrary based on the iTunes music library
    /// </summary>
    public class ITunesLibrary : SongLibrary
    {
        protected override SongLoader ChooseSongLoader() {
            if (Properties.Settings.Default.UseFastITunesLoader)
                return new FastITunesSongLoader();
            else
                return new ITunesSongLoader();
        }

        /// <summary>
        /// Is the given song currently playing
        /// </summary>
        /// <param name="song">The song to check</param>
        /// <returns>Is the given song playing</returns>
        public override bool IsPlaying(Song song)
        {
            ITunesSong itSong = song as ITunesSong;
            if (itSong != null && itSong.Track != null)
                return ITunes.Instance.IsTrackPlaying(itSong.Track);
            else
                return false;
        }

        /// <summary>
        /// Start the given song playing
        /// </summary>
        /// <param name="song">The song to play</param>
        public override void Play(Song song)
        {
            ITunesSong itSong = song as ITunesSong;
            if (itSong != null && itSong.Track != null)
                itSong.Track.Play();
        }

        /// <summary>
        /// Stop playing any song
        /// </summary>
        public override void StopPlaying()
        {
            ITunes.Instance.Stop();
        }

        #region Event handlers

        /// <summary>
        /// Initialize all events required for this library
        /// </summary>
        public override void InitializeEvents()
        {
            ITunes.Instance.Application.OnPlayerPlayEvent += new _IiTunesEvents_OnPlayerPlayEventEventHandler(ITunesOnPlayerPlayEvent);
            ITunes.Instance.Application.OnPlayerPlayingTrackChangedEvent += new _IiTunesEvents_OnPlayerPlayingTrackChangedEventEventHandler(ITunesOnPlayerPlayingTrackChangedEvent);
            ITunes.Instance.Application.OnPlayerStopEvent += new _IiTunesEvents_OnPlayerStopEventEventHandler(ITunesOnPlayerStopEvent);
            ITunes.Instance.Application.OnQuittingEvent += new _IiTunesEvents_OnQuittingEventEventHandler(ITunesOnQuittingEvent);
        }

        /// <summary>
        /// Stop listening for events on this library
        /// </summary>
        public override void DeinitializeEvents()
        {
            ITunes.Instance.Application.OnPlayerPlayEvent -= new _IiTunesEvents_OnPlayerPlayEventEventHandler(ITunesOnPlayerPlayEvent);
            ITunes.Instance.Application.OnPlayerPlayingTrackChangedEvent -= new _IiTunesEvents_OnPlayerPlayingTrackChangedEventEventHandler(ITunesOnPlayerPlayingTrackChangedEvent);
            ITunes.Instance.Application.OnPlayerStopEvent -= new _IiTunesEvents_OnPlayerStopEventEventHandler(ITunesOnPlayerStopEvent);
            ITunes.Instance.Application.OnQuittingEvent -= new _IiTunesEvents_OnQuittingEventEventHandler(ITunesOnQuittingEvent);
        }

        void ITunesOnPlayerStopEvent(object iTrack) {
            this.OnPlayEvent(new EventArgs());
        }

        void ITunesOnPlayerPlayingTrackChangedEvent(object iTrack) {
            this.OnPlayEvent(new EventArgs());
        }

        void ITunesOnPlayerPlayEvent(object iTrack) {
            this.OnPlayEvent(new EventArgs());
        }

        void ITunesOnQuittingEvent() {
            this.OnQuitEvent(new EventArgs());
            ITunes.Instance.Release();
        }

        #endregion
    }

    /// <summary>
    /// This class implements a loader that loads tracks from the iTunes library using
    /// the iTunes COM interface.
    /// </summary>
    public class ITunesSongLoader : SongLoader
    {
        /// <summary>
        /// Do the actual work of loading the lyrics from the library
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

    /// <summary>
    /// This class implements a loader that loads tracks from the iTunes library
    /// by reading the iTunes XML liibrary file.
    /// </summary>
    public class FastITunesSongLoader : SongLoader
    {
        /// <summary>
        /// Do the actual work of loading the lyrics from the library
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
            //string xml = File.ReadAllText(ITunes.Instance.XmlPath);
            //int docTypeStart = xml.IndexOf("<!DOCTYPE");
            //const string endOfDtdMarker = "0.dtd\">";
            //int docTypeEnd = xml.IndexOf(endOfDtdMarker) + endOfDtdMarker.Length;
            //string xml2 = xml.Remove(docTypeStart, docTypeEnd - docTypeStart);

            //XPathDocument doc = new XPathDocument(new StringReader(xml2));
            XmlDocument doc2 = new XmlDocument();
            doc2.XmlResolver = null;
            doc2.Load(ITunes.Instance.XmlPath);
            XPathNavigator nav = doc2.CreateNavigator();

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

        /// <summary>
        /// Get a value from a dictionary or return a default if that key isn't there
        /// </summary>
        /// <param name="data">The dictionary</param>
        /// <param name="key">The key to fetch</param>
        /// <returns>The value of the key or an empty string</returns>
        private string GetValue(Dictionary<string, string> data, string key) {
            string value;
            if (data.TryGetValue(key, out value))
                return value;
            else
                return "";
        }
    }
}
