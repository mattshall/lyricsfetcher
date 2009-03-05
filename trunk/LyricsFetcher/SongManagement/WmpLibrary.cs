/*
 * This file holds the class implementations necessary to interact with Window Media Library.
 *
 * Author: Phillip Piper
 * Date: 2009-02-06 4:28 PM
 *
 * CHANGE LOG:
 * 2009-02-06 JPP  Initial Version
 */

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

using WMPLib;

namespace LyricsFetcher
{
    /// <summary>
    /// This song library knows how to manipulate a WindowMediaPlayer
    /// database. 
    /// </summary>
    public class WmpLibrary : SongLibrary
    {
        protected override SongLoader ChooseSongLoader() {
            return new WmpSongLoader();
        }

        public override void Close() {
            // WMP doesn't have a separate interface, so if we close the library, we 
            // have to stop playing any current songs. 
            this.StopPlaying();
            base.Close();
        }

        public override bool IsPlaying(Song song) {
            WmpSong wmpSong = song as WmpSong;
            if (wmpSong == null)
                return false;

            if (Wmp.Instance.Player.playState != WMPPlayState.wmppsPlaying)
                return false;

            return Wmp.Instance.Player.currentMedia.get_isIdentical(wmpSong.media);
        }

        public override void Play(Song song) {
            WmpSong wmpSong = song as WmpSong;
            if (wmpSong != null) {
                Wmp.Instance.Player.currentPlaylist.appendItem(wmpSong.media);
                Wmp.Instance.Player.controls.playItem(wmpSong.media);
            }
        }

        public override void StopPlaying() {
            Wmp.Instance.Player.controls.stop();
        }

        #region Event handlers

        public override void InitializeEvents() {
            Wmp.Instance.Player.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
        }

        public override void DeinitializeEvents() {
            Wmp.Instance.Player.PlayStateChange -= new _WMPOCXEvents_PlayStateChangeEventHandler(Player_PlayStateChange);
        }

        void Player_PlayStateChange(int NewState) {
            this.OnPlayEvent(new EventArgs());
        }

        #endregion
    }

    /// <summary>
    /// Instances of these songs know how to load and store
    /// themselves from WindowMediaPlayer store.
    /// </summary>
    public class WmpSong : Song
    {
        public WmpSong(IWMPMedia iWmpMedia) {
            this.media = iWmpMedia;
            this.Title = this.media.getItemInfo(Wmp.WMTitle);
            this.Artist = this.media.getItemInfo(Wmp.WMAuthor);
            this.Album = this.media.getItemInfo(Wmp.WMAlbumTitle);
            this.Genre = this.media.getItemInfo(Wmp.WMGenre);
        }

        public override void Commit() {
            if (this.media == null)
                return;

            this.media.setItemInfo(Wmp.WMTitle, this.Title);
            this.media.setItemInfo(Wmp.WMAuthor, this.Artist);
            this.media.setItemInfo(Wmp.WMAlbumTitle, this.Album);
            this.media.setItemInfo(Wmp.WMGenre, this.Genre);
            this.media.setItemInfo(Wmp.WMLyrics, this.Lyrics);
        }

        public override void GetLyrics() {
            this.Lyrics = this.media.getItemInfo(Wmp.WMLyrics);
        }

        internal IWMPMedia media;
    }


    public class WmpSongLoader : SongLoader
    {
        protected override object DoWork(DoWorkEventArgs e) {
            try {
                IWMPPlaylist tracks = Wmp.Instance.AllTracks;

                // How many tracks are there and how many songs should we fetch?
                int trackCount = tracks.count;
                int maxSongs = trackCount;
                if (this.MaxSongsToFetch > 0)
                    maxSongs = Math.Min(trackCount, this.MaxSongsToFetch);

                this.ReportProgress(0, "Gettings songs...");
                for (int i = 0; i < trackCount && this.Songs.Count < maxSongs && this.CanContinueRunning; i++) {

                    IWMPMedia track = tracks.get_Item(i);
                    this.AddSong(new WmpSong(track));

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
}
