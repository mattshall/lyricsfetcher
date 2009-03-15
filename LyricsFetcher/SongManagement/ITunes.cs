/*
 * The file contains all the iTunes specific code.
 *
 * Author: Phillip Piper
 * Date: 7/01/2008 8:40 PM
 *
 * CHANGE LOG:
 * 2009-02-26 JPP  Changed to use iTunesDetectorClass to detect the presence of iTunes
 * 2009-02-01 JPP  Simplified
 * 2008-01-07 JPP  Initial Version
 */

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using iTunesLib;
using ITDETECTORLib;

namespace LyricsFetcher
{
    /// <summary>
    /// A wrapper around the iTunes library.
    /// </summary>
    /// <remarks>
    /// <para>If ITunes.HasITunes returns false, do not call any other methods -- they will crash.</para>
    /// </remarks>
    public sealed class ITunes
    {
        #region Instance access and constructors

        /// <summary>
        /// Return the singleton instance of this class
        /// </summary>
        public static ITunes Instance {
            get { return instance; }
        }
        private static ITunes instance = new ITunes();

        /// <summary>
        /// Is iTunes actually installed on this machine?
        /// If this returns false, no other methods should be called.
        /// </summary>
        public static bool HasITunes
        {
            get {
                try {
                    iTunesDetectorClass detector = new iTunesDetectorClass();
                    return detector.IsiTunesAvailable;
                }
                catch (COMException) {
                    return false;
                }
            }
        }

        /// <summary>
        /// You cannot create instances of ITunes. You access a singleton through
        /// "ITunes.Instance".
        /// </summary>
        private ITunes()
        {
        }

        #endregion

        #region Public attributes

        /// <summary>
        /// Return the iTunes library
        /// </summary>
        public iTunesApp Application
        {
            get {
                // TODO: Put this in a critical section
                if (this.iTunesApp == null) {
                    try {
                        iTunesApp = new iTunesAppClass();
                    } catch (Exception) {
                        // do nothing
                    }
                }
                return iTunesApp;
            }
        }
        private iTunesAppClass iTunesApp;

        /// <summary>
        /// Returns a track collection of all the tracks available in the iTunes library
        /// </summary>
        /// <returns>IITTrackCollection</returns>
        public IITTrackCollection AllTracks
        {
            get {
                return this.Application.LibraryPlaylist.Tracks;
            }
        }

        /// <summary>
        /// Return true if any track is currently playing in iTunes
        /// </summary>
        public bool IsAnyTrackPlaying
        {
            get {
                return this.Application.PlayerState == ITPlayerState.ITPlayerStatePlaying;
            }
        }

        /// <summary>
        /// What version of iTunes is installed?
        /// </summary>
        public string Version
        {
            get {
                return this.Application.Version;
            }
        }

        /// <summary>
        /// Return the full path to the xml file that holds the information about the iTunes library
        /// </summary>
        public string XmlPath
        {
            get {
                return this.Application.LibraryXMLPath;
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Return the object identified by the four given ids.
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="playlistId"></param>
        /// <param name="trackId"></param>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public IITObject GetObjectByIds(int sourceId, int playlistId, int trackId, int databaseId) {
            return this.Application.GetITObjectByID(sourceId, playlistId, trackId, databaseId);
        }

        public IITTrack GetTrackByPersistentIds(int high, int low) {
            return this.Application.LibraryPlaylist.Tracks.get_ItemByPersistentID(high, low);
        }

        public void GetPersistentIds(object x, out int high, out int low)
        {
            this.Application.GetITObjectPersistentIDs(ref x, out high, out low);
        }

        /// <summary>
        /// Pause any currently playing track
        /// </summary>
        public void Pause()
        {
            this.Application.Pause();
        }

        /// <summary>
        /// Play the current song in the playlist
        /// </summary>
        public void Play()
        {
            this.Application.Play();
        }

        /// <summary>
        /// Release our iTunes related resources
        /// </summary>
        public void Release()
        {
            if (this.iTunesApp != null) {
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(this.iTunesApp);
                this.iTunesApp = null;
            }
        }

        /// <summary>
        /// StopPlaying playing any song
        /// </summary>
        public void Stop()
        {
            this.Application.Stop();
        }

        #endregion

        #region Enquiries

        /// <summary>
        /// Is the given track currently playing?
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsTrackPlaying(IITTrack track)
        {
            if (track == null || !this.IsAnyTrackPlaying)
                return false;

            IITTrack currentTrack = this.Application.CurrentTrack;
            return (currentTrack != null &&
                track.trackID == currentTrack.trackID &&
                track.TrackDatabaseID == currentTrack.TrackDatabaseID);
        }

        #endregion
    }
}
