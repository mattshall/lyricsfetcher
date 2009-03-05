/*
 * The file contains all the Windows Media Player specific code.
 *
 * Author: Phillip Piper
 * Date: 7/01/2008 8:40 PM
 *
 * CHANGE LOG:
 * 2008-01-07 JPP  Initial Version
 */

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using WMPLib;

namespace LyricsFetcher
{
    public sealed class Wmp
    {
		#region Instance access and constructors

        /// <summary>
        /// Return the singleton instance of this class
        /// </summary>
		public static Wmp Instance {
			get { return instance; }
		}
        private static Wmp instance = new Wmp();

        /// <summary>
        /// Is Windows Media Player actually installed on this machine?
        /// </summary>
        /// <remarks>If this returns false, no other methods should be called.</remarks>
        /// <returns></returns>
        public static bool HasWmp()
        {
            return (Wmp.Instance.Player != null);
        }

        /// <summary>
        /// You cannot create instances of Wmp. You access a singleton through
        /// "Wmp.Instance".
        /// </summary>
		private Wmp()
		{
		}

		#endregion

        public const string WMTitle = "Title";
        public const string WMAuthor = "Author";
        public const string WMDescription = "Description";
        public const string WMRating = "Rating";
        public const string WMCopyright = "Copyright";
        public const string WMAlbumTitle = "WM/AlbumTitle";
        public const string WMTrack = "WM/Track";
        public const string WMGenre = "WM/Genre";
        public const string WMYear = "WM/Year";
        public const string WMGenreID = "WM/GenreID";
        public const string WMComposer = "WM/Composer";
        public const string WMLyrics = "WM/Lyrics";
        public const string WMTrackNumber = "WM/TrackNumber";
        public const string WMAlbumArtist = "WM/AlbumArtist";

		#region Public attributes

		/// <summary>
		/// Return the window media player
		/// </summary>
		public WindowsMediaPlayer Player
		{
			get {
                // TODO: Put this in a critical section
                if (this.wmpPlayer == null) {
					try {
                        this.wmpPlayer = new WindowsMediaPlayer();
					} catch (Exception) {
						// do nothing
					}
				}
                return this.wmpPlayer;
			}
		}
        private WindowsMediaPlayer wmpPlayer;

        /// <summary>
        /// Returns a track collection of all the tracks available in the WMP library
        /// </summary>
        /// <returns>IWMPPlaylist</returns>
        public IWMPPlaylist AllTracks {
            get {
                return this.Player.mediaCollection.getByAttribute("MediaType", "audio");
            }
        }


        #endregion

        #region Event handlers

        void wmpPlayer_MediaError(object pMediaObject) {
            System.Diagnostics.Debug.WriteLine("Cannot play media file.");
        }

        void wmpPlayer_PlayStateChange(int newState) {
            System.Diagnostics.Debug.WriteLine(String.Format("PlayStateChanged: {0}", (WMPPlayState)newState));
        }

        #endregion

        #region Private variables


        #endregion

    }
}
