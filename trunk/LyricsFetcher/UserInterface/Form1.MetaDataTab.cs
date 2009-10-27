/*
 * This file handles all the functionality of the Metadata tab of the main window
 *
 * Author: Phillip Piper
 * Date: 2009-03-28 16:49
 *
 * CHANGE LOG:
 * 2009-03-28  JPP  Initial Version
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using BrightIdeasSoftware;

namespace LyricsFetcher
{
    partial class Form1
    {
        #region Initializing

        private void InitializeMetaDataTab() {
            this.InitializeMetaDataListView();
            this.InitializeMetaDataAnimator();
            this.InitializeMetaDataFetchManager();

            this.BeginInvoke(new MethodInvoker(this.SetMetaDataListContents));
        }

        private void InitializeMetaDataAnimator() {
            // Allow animations on the Fetching column
            this.animationHelper = new AnimationHelper();
            this.animationHelper.Column = this.olvMdStatus;
            this.animationHelper.SetCompositeAnimationImage(
                Properties.Resources.process_working, new Size(16, 16), 32);

            this.animationHelper.IsAnimatingGetter = delegate(object model) {
                MetaData metaData = this.GetSongMetaData((Song)model);
                return metaData != null && !metaData.IsFinished;
            };

            this.animationHelper.Start();
        }

        private void InitializeMetaDataFetchManager() {
            this.metaDataFetchManager.StatusEvent += new EventHandler<MetaDataEventArgs>(metaDataFetchManager_StatusEvent);
            this.metaDataFetchManager.Start();
        }

        private void InitializeMetaDataListView() {
            // Create a typed wrapper to remove need to perform so many casts
            this.typedOlvMetaData = new TypedObjectListView<Song>(this.olvMetaData);

            // Generate aspect getters which are typically 3-5x faster than reflection
            this.typedOlvMetaData.GenerateAspectGetters();

            this.olvMdTitle.ImageGetter = delegate(object x) { return "music"; };
            this.typedOlvMetaData.GetColumn(1).ImageGetter = delegate(Song song) {
                if (String.IsNullOrEmpty(song.Artist))
                    return -1;
                else
                    return "group";
            };

            this.olvMdLyricsStatus.AspectGetter = delegate(object x) {
                Song song = (Song)x;
                if (this.fetchManager.GetStatus(song) == LyricsFetchStatus.NotFound)
                    return song.LyricsStatusString;
                else
                    return this.fetchManager.GetStatusString(song);
            };

            this.olvMdStatus.AspectGetter = delegate(object x) {
                return this.GetStatusText(this.GetSongMetaData((Song)x));
            };

            byte[] olvState = this.Preferences.MetaDataListViewState;
            if (olvState != null)
                this.olvMetaData.RestoreState(olvState);
        }

        #endregion

        #region Accessing

        private MetaData GetSongMetaData(Song song) {
            if (song != null && this.mapMetaData.ContainsKey(song))
                return this.mapMetaData[song];
            else
                return null;
        }

        private string GetStatusText(MetaData metaData) {
            if (metaData == null)
                return String.Empty;

            switch (metaData.Status) {
                case MetaDataStatus.Success:
                    if (metaData.IsDataUnchanged)
                        return "Success - Data unchanged";
                    else
                        return "Success";
                case MetaDataStatus.Failed_ConverterFailed:
                    return "Converter failed";
                case MetaDataStatus.Failed_Lookup:
                    return "Lookup failed";
                case MetaDataStatus.Failed_MissingFile:
                    return "Failed - File missing";
                case MetaDataStatus.Failed_NoData:
                    return "No data available";
                case MetaDataStatus.Failed_UnknownFileType:
                    return "Unknown file type";
                case MetaDataStatus.InProgress_Converting:
                    return "Conversion in progress";
                case MetaDataStatus.InProgress_Fetching:
                    return "Fetch in progress";
                case MetaDataStatus.InProgress_Fingerprinting:
                    return "Fingerprinting in progress";
                default:
                    return metaData.Status.ToString();
            }
        }

        private string GetStatusTextLong(MetaData metaData) {
            if (metaData == null)
                return "No metadata lookup";

            switch (metaData.Status) {
                case MetaDataStatus.Rejected:
                    return "The metadata lookup succeeded but its results were rejected";
                case MetaDataStatus.Accepted:
                    return "The metadata lookup succeeded and its results were accepted";
                case MetaDataStatus.Waiting:
                    return "The metadata lookup is waiting to execute.";
                case MetaDataStatus.Failed:
                    return "The metadata lookup failed";
                case MetaDataStatus.Success:
                    return "The metadata lookup succeeded";
                case MetaDataStatus.Failed_ConverterFailed:
                    return "Failed. The audio decoder for this file failed to convert it to WAV format.";
                case MetaDataStatus.Failed_Lookup:
                    return "Failed. This song was not found in the online database.";
                case MetaDataStatus.Failed_MissingFile:
                    return "Failed. The media file for this song was missing or inaccessible.";
                case MetaDataStatus.Failed_NoData:
                    return "Failed. The online database contains this song, but no title or artist has been registered for it.";
                case MetaDataStatus.Failed_UnknownFileType:
                    return "Failed. The media file for this song has no decoder registered for it.";
                case MetaDataStatus.InProgress_Converting:
                    return "In progress. A copy of the media file is being converted to WAV format...";
                case MetaDataStatus.InProgress_Fetching:
                    return "In progress. Information about the song is being fetched from the online database...";
                case MetaDataStatus.InProgress_Fingerprinting:
                    return "In progress. An audio fingerprint is being calculated for this song...";
                default:
                    return metaData.Status.ToString();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Enable or disable the controls to match the state of the UI
        /// </summary>
        private void EnableControlsMetaData() {
            bool hasSongs = (this.olvMetaData.GetItemCount() > 0);
            bool hasSelection = (this.olvMetaData.SelectedIndices.Count > 0);
            bool isSingleSelection = (this.olvMetaData.SelectedIndices.Count == 1);

            this.buttonMdLookup.Enabled = hasSongs && this.isNetworkAvailable;
            this.buttonAccept.Enabled = hasSongs;
            this.buttonReject.Enabled = hasSelection;

            // The Play button is only enabled when one song is selected.
            // It can then function as either a start or stop button.
            this.buttonPlay.Enabled = isSingleSelection;
            if (isSingleSelection && this.library.IsPlaying(this.typedOlvMetaData.SelectedObject)) {
                this.buttonPlay.ImageKey = "stop";
                this.buttonPlay.Text = "Sto&p";
            } else {
                this.buttonPlay.ImageKey = "play";
                this.buttonPlay.Text = "&Play";
            }
        }

        private void SetMetaDataListContents() {
            if (this.library.IsLoading) {
                this.olvMetaData.EmptyListMsg = "Waiting for library to finish loading";
                this.olvMetaData.SetObjects(null);
            } else if (this.radioButtonShowAll.Checked)
                this.olvMetaData.SetObjects(this.library.Songs);
            else if (this.radioButtonShowMissingDetails.Checked)
                this.olvMetaData.SetObjects(this.library.SongsMissingData);
            else if (this.radioButtonShowWithoutLyrics.Checked)
                this.olvMetaData.SetObjects(this.library.SongsWithoutLyrics);
            else
                this.olvMetaData.SetObjects(null);
            this.EnableControls();
            this.UpdateStatusText();
            this.UpdateMetaDataDetails();
        }

        private void SetSongMetaData(MetaData metaData) {
            this.mapMetaData[metaData.Song] = metaData;
        }

        private void UpdateStatusTextMetaData() {
            this.toolStripStatusLabel1.Text = String.Format("{0} selected. {1} songs shown.",
                this.olvMetaData.SelectedIndices.Count, this.olvMetaData.GetItemCount());
        }

        private void UpdateMetaDataDetails() {
            Song song = this.olvMetaData.SelectedObject as Song;
            if (song != null) {
                this.labelTitle.Text = song.Title;
                this.labelArtist.Text = song.Artist;
                this.labelGenre.Text = song.Genre;
            }

            MetaData metaData = this.GetSongMetaData(song);
            this.labelStatus.Text = this.GetStatusTextLong(metaData);

            if (metaData != null && metaData.Status == MetaDataStatus.Success) {
                this.labelProposed.Show();
                this.labelTitleProposed.Text = metaData.Title;
                this.labelArtistProposed.Text = metaData.Artist;
                this.labelGenreProposed.Text = metaData.Genre;
            } else {
                this.labelProposed.Hide();
                this.labelTitleProposed.Text = String.Empty;
                this.labelArtistProposed.Text = String.Empty;
                this.labelGenreProposed.Text = String.Empty;
            }
        }

        #endregion

        #region Fetch event handlers

        void metaDataFetchManager_StatusEvent(object sender, MetaDataEventArgs e) {
            if (this.IsDisposed)
                return;

            this.BeginInvoke(new MethodInvoker(delegate() {
                this.SetSongMetaData(e.MetaData);
                this.olvMetaData.RefreshObject(e.MetaData.Song);
                this.EnableControls();
                this.UpdateStatusText();
                if (this.olvMetaData.IsSelected(e.MetaData.Song))
                    this.UpdateMetaDataDetails();
            }));
        }

        #endregion

        #region UI event handlers

        private void buttonMdLookup_Click(object sender, EventArgs e) {
            IList<Song> selected = this.typedOlvMetaData.SelectedObjects;
            if (selected.Count == 0) {
                string msg = String.Format(Properties.Resources.LookupAllMsg, this.olvMetaData.GetItemCount());
                DialogResult result = MessageBox.Show(msg, Properties.Resources.AppName, 
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                    return;

                for (int i=0; i<this.olvMetaData.GetItemCount(); i++)
                    selected.Add(this.typedOlvMetaData.GetModelObject(i));
            }

            this.metaDataFetchManager.Queue(selected);
            if (this.metaDataFetchManager.Paused)
                this.metaDataFetchManager.Start();
        }

        private void buttonAccept_Click(object sender, EventArgs e) {
            IEnumerable songs = this.olvMetaData.SelectedObjects;
            if (this.olvMetaData.SelectedObjects.Count == 0) {
                DialogResult result = MessageBox.Show(Properties.Resources.AcceptAllMsg, Properties.Resources.AppName,
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                    return;
                songs = this.olvMetaData.Objects;
            }

            // Write the changed detail into the selected objects
            using (new WaitCursor()) {
                foreach (Song song in songs) {
                    MetaData metaData = this.GetSongMetaData(song);
                    if (metaData != null && metaData.Status == MetaDataStatus.Success) {
                        try {
                            this.library.Cache.RemoveLyrics(song);
                            song.Title = metaData.Title;
                            song.Artist = metaData.Artist;
                            song.Genre = metaData.Genre;
                            song.Commit();
                            this.library.Cache.PutLyrics(song);
                            metaData.Status = MetaDataStatus.Accepted;
                            if (this.checkBoxFetchAfterAccept.Checked)
                                this.fetchManager.Queue(song);
                        }
                        catch (COMException ex) {
                            metaData.Status = MetaDataStatus.Failed;
                            this.olvMetaData.RefreshObject(song);
                            if (this.ReportCommitFailed(song, ex))
                                break;
                        }
                        this.olvMetaData.RefreshObject(song);
                        this.olvSongs.RefreshObject(song);
                    }
                }
                this.EnableControls();
                this.UpdateMetaDataDetails();
            }
        }

        private void buttonReject_Click(object sender, EventArgs e)
        {
            IList songs = this.olvMetaData.SelectedObjects;
            if (songs.Count == 0)
                return;

            using (new WaitCursor()) {
                foreach (Song song in songs) {
                    MetaData metaData = this.GetSongMetaData(song);
                    if (metaData != null && metaData.Status == MetaDataStatus.Success) 
                        metaData.Status = MetaDataStatus.Rejected;
                }
                this.olvMetaData.RefreshObjects(songs);
                this.EnableControls();
                this.UpdateMetaDataDetails();
            }
        }

        private void buttonMdPlay_Click(object sender, EventArgs e) {
            this.PlaySong(this.olvMetaData.SelectedObject as Song);
        }

        private void radioButtonShowMissingDetails_CheckedChanged(object sender, EventArgs e) {
            this.SetMetaDataListContents();
        }

        private void radioButtonShowWithoutLyrics_CheckedChanged(object sender, EventArgs e) {
            this.SetMetaDataListContents();
        }

        private void radioButtonShowAll_CheckedChanged(object sender, EventArgs e) {
            this.SetMetaDataListContents();
        }

        private void olvMetaData_SelectionChanged(object sender, EventArgs e) {
            this.UpdateMetaDataDetails();
            this.UpdateStatusText();
            this.EnableControls();
        }

        #endregion

        #region Private variables

        private MetaDataFetchManager metaDataFetchManager = new MetaDataFetchManager();
        private TypedObjectListView<Song> typedOlvMetaData;
        private Dictionary<Song, MetaData> mapMetaData = new Dictionary<Song, MetaData>();

        #endregion
    }
}
