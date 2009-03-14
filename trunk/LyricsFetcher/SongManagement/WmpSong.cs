/*
 * This file holds the class implementations necessary to interact with Window Media Library.
 *
 * Author: Phillip Piper
 * Date: 2009-03-14 8:28 AM
 *
 * CHANGE LOG:
 * 2009-03-14 JPP  - Changed to use MetaDataEditor to update lyrics
 *                 - Separated from WmpLibrary.cs
 */

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

using WMPLib;

namespace LyricsFetcher
{
    /// Instances of these songs know how to load and store
    /// themselves from WindowMediaPlayer store.
    /// </summary>
    public class WmpSong : Song
    {
        /// <summary>
        /// Create a new song from the given media object
        /// </summary>
        /// <param name="iWmpMedia">The media object that represents the song</param>
        public WmpSong(IWMPMedia iWmpMedia) {
            this.Media = iWmpMedia;
            this.Title = iWmpMedia.getItemInfo(Wmp.WMTitle);
            this.Artist = iWmpMedia.getItemInfo(Wmp.WMAuthor);
            this.Album = iWmpMedia.getItemInfo(Wmp.WMAlbumTitle);
            this.Genre = iWmpMedia.getItemInfo(Wmp.WMGenre);
        }

        /// <summary>
        /// Get or set the media object that represents the song
        /// </summary>
        public IWMPMedia Media {
            get;
            set;
        }

        /// <summary>
        /// Read the lyrics of this song from the WMP library.
        /// </summary>
        public override void GetLyrics() {
            this.Lyrics = this.Media.getItemInfo(Wmp.WMLyrics);
        }

        /// <summary>
        /// Write the current state of this song back into WMP
        /// </summary>
        public override void Commit() {
            if (this.Media == null)
                return;

            if (this.Media.isReadOnlyItem(Wmp.WMTitle) == false)
                this.Media.setItemInfo(Wmp.WMTitle, this.Title);
            if (this.Media.isReadOnlyItem(Wmp.WMAuthor) == false)
                this.Media.setItemInfo(Wmp.WMAuthor, this.Artist);
            if (this.Media.isReadOnlyItem(Wmp.WMAlbumTitle) == false)
                this.Media.setItemInfo(Wmp.WMAlbumTitle, this.Album);
            if (this.Media.isReadOnlyItem(Wmp.WMGenre) == false)
                this.Media.setItemInfo(Wmp.WMGenre, this.Genre);
            if (this.Media.isReadOnlyItem(Wmp.WMLyrics) == false) {
                /*
                 * Life is never easy. We should be able to simply say:
                 * this.Media.setItemInfo(Wmp.WMLyrics, this.Lyrics);
                 * but that doesn't work. On WMP 9 and 10, it doesn't update
                 * the lyrics at all. On WMP 11, it updates them, but creates
                 * hundreds of entries -- one for each language.
                 * So we have to do something more complicated
                 */
                using (MetaDataEditor editor = new MetaDataEditor(this.Media.sourceURL)) {
                    editor.SetFieldValue(Wmp.WMLyrics, this.Lyrics);
                }
            }
        }
    }
}
