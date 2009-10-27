/*
 * This file handles all the functionality of the Lyrics tab of the main window
 *
 * Author: Phillip Piper
 * Date: 2009-03-28 16:49
 *
 * CHANGE LOG:
 * 2009-03-31  JPP  - Added MetaData button
 * 2009-03-28  JPP  Initial Version
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Web;
using System.Windows.Forms;

using BrightIdeasSoftware;

namespace LyricsFetcher
{
    partial class Form1
    {
        #region Initialization

        private void InitializeLyricsTab() {
            this.InitializeSongListView();
            this.InitializeSongDetails();
            this.InitializeSongLibrary();
            this.InitializeLyricsFetchAnimation();
            this.InitializeLyricsFetchManager();
        }

        /// <summary>
        /// Initialize the details portion of the UI
        /// </summary>
        private void InitializeSongDetails() {
            // Give each textbox a Munger that knows how to get and set the appropriate information
            this.textBoxTitle.Tag = new Munger("Title");
            this.textBoxArtist.Tag = new Munger("Artist");
            this.textBoxAlbum.Tag = new Munger("Album");
            this.textBoxGenre.Tag = new Munger("Genre");
            this.textBoxLyrics.Tag = new Munger("Lyrics");

            // Collect all the details boxes so we can handle them as a collection
            this.allDetailTextBoxes.Add(this.textBoxTitle);
            this.allDetailTextBoxes.Add(this.textBoxArtist);
            this.allDetailTextBoxes.Add(this.textBoxAlbum);
            this.allDetailTextBoxes.Add(this.textBoxGenre);
            this.allDetailTextBoxes.Add(this.textBoxLyrics);
        }
        private List<TextBox> allDetailTextBoxes = new List<TextBox>();

        private void InitializeSongListView() {
            // Create a typed wrapper to remove need to perform so many casts
            this.typedOlvSongs = new TypedObjectListView<Song>(this.olvSongs);

            // Generate aspect getters which are typically 3-5x faster than reflection
            this.typedOlvSongs.GenerateAspectGetters();

            this.olvColumnTitle.ImageGetter = delegate(object x) { return "music"; };
            this.typedOlvSongs.GetColumn(1).ImageGetter = delegate(Song song) {
                if (String.IsNullOrEmpty(song.Artist))
                    return -1;
                else
                    return "group";
            };

            // Show the current fetch status if there is one
            this.olvColumnLyricsStatus.AspectGetter = delegate(object x) {
                Song song = (Song)x;
                if (this.fetchManager.GetStatus(song) == LyricsFetchStatus.NotFound)
                    return song.LyricsStatusString;
                else
                    return this.fetchManager.GetStatusString(song);
            };

            byte[] olvState = this.Preferences.ListViewState;
            if (olvState != null)
                this.olvSongs.RestoreState(olvState);
        }

        private void InitializeLyricsFetchAnimation() {
            // Allow animations on the Lyrics Status column
            this.animationHelper = new AnimationHelper();
            this.animationHelper.Column = this.olvColumnLyricsStatus;
            this.animationHelper.SetCompositeAnimationImage(
                Properties.Resources.process_working, new Size(16, 16), 32);

            // The image should animate if it has any fetch status except notFound
            this.animationHelper.IsAnimatingGetter = delegate(object model) {
                Song song = (Song)model;
                return this.fetchManager.GetStatus(song) != LyricsFetchStatus.NotFound;
            };

            this.animationHelper.Start();
        }

        private void InitializeSongLibrary() {
            // Release any previous library
            if (this.library != null) {
                this.fetchManager.CancelAll();
                this.library.Close();
                this.library.ProgessEvent -= new EventHandler<ProgressEventArgs>(library_ProgessEvent);
                this.library.DoneEvent -= new EventHandler<ProgressEventArgs>(library_DoneEvent);
                this.library.PlayEvent -= new EventHandler<EventArgs>(library_PlayEvent);
                this.library.QuitEvent -= new EventHandler<EventArgs>(library_PlayEvent);
            }

            this.olvSongs.SetObjects(null);

            if (this.Preferences.LibraryApplication == LibraryApplication.ITunes) {
                this.library = new ITunesLibrary();
                this.Text = Properties.Resources.WindowLabelITunes;
            } else {
                this.library = new WmpLibrary();
                this.Text = Properties.Resources.WindowLabelWmp;
            }
            this.library.InitializeEvents();
            this.library.ProgessEvent += new EventHandler<ProgressEventArgs>(library_ProgessEvent);
            this.library.DoneEvent += new EventHandler<ProgressEventArgs>(library_DoneEvent);
            this.library.PlayEvent += new EventHandler<EventArgs>(library_PlayEvent);
            this.library.QuitEvent += new EventHandler<EventArgs>(library_QuitEvent);
            this.library.LoadSongs();

            this.UpdateDetails();
            this.EnableControls();
            this.UpdateStatusText();
        }

        #endregion

        #region UI Handling

        /// <summary>
        /// Enable or disable the controls to match the state of the UI
        /// </summary>
        private void EnableControlsLyrics() {
            bool hasSongs = (this.olvSongs.GetItemCount() > 0);
            bool hasSelection = (this.olvSongs.SelectedIndices.Count > 0);
            bool isSingleSelection = (this.olvSongs.SelectedIndices.Count == 1);

            // "Select..." buttons are enabled when there are songs in the listview
            this.buttonSelectMissing.Enabled = hasSongs;
            this.buttonSelectUntried.Enabled = hasSongs;
            this.buttonSelectAll.Enabled = hasSongs;
            this.buttonSelectNone.Enabled = hasSongs;

            // The Play button is only enabled when one song is selected.
            // It can then function as either a start or stop button.
            this.buttonPlay.Enabled = isSingleSelection;
            if (isSingleSelection && this.library.IsPlaying(this.olvSongs.SelectedObject as Song)) {
                this.buttonPlay.ImageKey = "stop";
                this.buttonPlay.Text = "Sto&p";
            } else {
                this.buttonPlay.ImageKey = "play";
                this.buttonPlay.Text = "&Play";
            }

            this.buttonSearch.Enabled = isSingleSelection && this.isNetworkAvailable;
            this.buttonFetch.Enabled = hasSelection && this.isNetworkAvailable;
            this.buttonMetaData.Enabled = hasSelection && this.isNetworkAvailable;
            this.buttonStop.Visible = this.library.IsLoading || this.fetchManager.IsFetching;

            // Disable commands that mess with the library while the library is loading
            this.toolStripMenuItemChooseLibrary.Enabled = !this.library.IsLoading;
            this.toolStripMenuItemReloadLibrary.Enabled = !this.library.IsLoading;
            this.toolStripMenuItemDiscardCache.Enabled = !this.library.IsLoading;
        }

        /// <summary>
        /// Update all the details with the common information from
        /// all the selected songs.
        /// </summary>
        private void UpdateDetails() {
            IList<Song> songs = this.typedOlvSongs.SelectedObjects;
            foreach (TextBox tb in this.allDetailTextBoxes) {
                tb.Enabled = songs.Count > 0;
                tb.Text = this.GetCommonValue(songs, (Munger)tb.Tag);
            }
        }

        private void UpdateStatusTextLyrics() {
            this.toolStripStatusLabel1.Text = String.Format("{0} selected. {1} in library.",
                this.olvSongs.SelectedIndices.Count, this.olvSongs.GetItemCount());

            if (this.fetchManager.CountFetching == 0 && this.fetchManager.CountWaiting == 0)
                this.labelFetchStatus.Text = "";
            else
                if (this.fetchManager.CountWaiting == 0)
                    this.labelFetchStatus.Text = String.Format("{0} fetching",
                        this.fetchManager.CountFetching);
                else
                    this.labelFetchStatus.Text = String.Format("{0} fetching\r\n{1} waiting",
                        this.fetchManager.CountFetching,
                        this.fetchManager.CountWaiting);
        }

        /// <summary>
        /// If the given munger returns the same value for all the given
        /// songs, then return that value. Otherwise, return an empty string.
        /// </summary>
        /// <param name="songs">The list of songs to be considered</param>
        /// <param name="munger">The munger which will extract the value</param>
        /// <returns>The common value or an empty string</returns>
        private string GetCommonValue(IList<Song> songs, Munger munger) {
            if (songs.Count == 0 || songs.Count > 1000)
                return "";

            string value = (string)munger.GetValue(songs[0]);
            for (int i = 1; i < songs.Count; i++) {
                if (value != (string)munger.GetValue(songs[i]))
                    return "";
            }

            return value;
        }

        /// <summary>
        /// The text in the given text box may have changed.
        /// If it has, update all selected songs with its new value.
        /// </summary>
        /// <param name="tb">The text box whose value has changed</param>
        private void CommitDetails(TextBox tb) {
            if (!tb.Modified)
                return;

            IList songs = this.olvSongs.SelectedObjects;
            if (songs.Count == 0)
                return;

            // Write the changed detail into the selected objects
            using (new WaitCursor()) {
                foreach (Song song in songs) {
                    try {
                        this.library.Cache.RemoveLyrics(song);
                        ((Munger)tb.Tag).PutValue(song, tb.Text.Trim());
                        song.Commit();
                        this.library.Cache.PutLyrics(song);
                    }
                    catch (COMException ex) {
                        if (this.ReportCommitFailed(song, ex))
                            break;
                    }
                }
                this.olvSongs.RefreshObjects(songs);
            }
            this.EnableControls();

            // Don't do this again until the user changes it another time
            tb.Modified = false;
        }

        /// <summary>
        /// Configure the text boxes of the details section to autocomplete
        /// their values. The Artist, Album and Genre text boxes will autocomplete.
        /// The Title and Lyrics will not.
        /// </summary>
        private void ConfigureAutoCompleteDetails() {
            int rowCount = this.olvSongs.GetItemCount();
            this.olvSongs.ConfigureAutoComplete(this.textBoxArtist, this.olvColumnArtist, rowCount);
            this.olvSongs.ConfigureAutoComplete(this.textBoxAlbum, this.olvColumnAlbum, rowCount);
            this.olvSongs.ConfigureAutoComplete(this.textBoxGenre, this.olvColumnGenre, rowCount);
        }

        #endregion

        #region Library and Fetch Event handlers
        // All of these events originate on non-UI threads, so we have to Invoke them

        void library_ProgessEvent(object sender, ProgressEventArgs e) {
            this.BeginInvoke(new MethodInvoker(delegate() {
                if (e.Percentage == 0) {
                    this.olvSongs.EmptyListMsg = "Initializing library...";
                    this.SetMetaDataListContents();
                } else
                    this.olvSongs.EmptyListMsg = String.Format("Loading: {0}%", e.Percentage);
            }));
        }

        void library_DoneEvent(object sender, ProgressEventArgs e) {
            this.BeginInvoke(new MethodInvoker(delegate() {
                if (e.IsCancelled)
                    this.olvSongs.EmptyListMsg = "Loading was cancelled";
                else
                    this.olvSongs.EmptyListMsg = "This library is empty";
                this.olvSongs.SetObjects(library.Songs);
                this.SetMetaDataListContents();
                this.ConfigureAutoCompleteDetails();
                this.EnableControls();
                this.UpdateStatusText();

                // Release the cache to save some resources
                this.library.Cache = null;
                GC.Collect();
            }));
        }

        void library_PlayEvent(object sender, EventArgs e) {
            this.BeginInvoke(new MethodInvoker(this.EnableControls));
        }

        void fetchManager_StatusEvent(object sender, LyricsFetchStatusEventArgs e) {
            this.BeginInvoke(new MethodInvoker(delegate() {
                this.olvSongs.RefreshObject(e.Song);
                this.EnableControls();
                this.UpdateStatusText();
                if (e.Status == LyricsFetchStatus.Done) {
                    this.library.CacheLyrics(e.Song);
                    if (this.olvSongs.IsSelected(e.Song))
                        this.UpdateDetails();
                }
            }));
        }

        void library_QuitEvent(object sender, EventArgs e) {
            this.BeginInvoke(new MethodInvoker(delegate() {
                if (this.preferences.LibraryApplication == LibraryApplication.ITunes) {
                    MessageBox.Show(this, Properties.Resources.ITunesClosedMsg, Properties.Resources.AppName);
                    this.olvSongs.SetObjects(null);
                    this.fetchManager.CancelAll();
                    this.UpdateDetails();
                    this.EnableControls();
                    this.UpdateStatusText();
                }
            }));
        }

        #endregion

        #region UI Event handlers

        private void buttonFetch_Click(object sender, EventArgs e) {
            foreach (Song song in this.olvSongs.SelectedObjects) {
                this.fetchManager.Queue(song);
            }
            this.EnableControls();
        }

        private void buttonSelectUntried_Click(object sender, EventArgs e) {
            using (new WaitCursor()) {
                this.olvSongs.SelectedObjects = this.library.UntriedSongs;
                this.EnsureSelectionVisible(this.olvSongs);
            };
        }

        private void buttonSelectMissing_Click(object sender, EventArgs e) {
            using (new WaitCursor()) {
                this.olvSongs.SelectedObjects = this.library.SongsWithoutLyrics;
                this.EnsureSelectionVisible(this.olvSongs);
            }
        }

        private void buttonSelectAll_Click(object sender, EventArgs e) {
            using (new WaitCursor()) {
                this.olvSongs.SelectAll();
            }
        }

        private void buttonSelectNone_Click(object sender, EventArgs e) {
            using (new WaitCursor()) {
                this.olvSongs.DeselectAll();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e) {
            if (this.library.IsLoading)
                this.library.CancelLoad();
            else
                if (this.fetchManager.IsFetching)
                    this.fetchManager.CancelAll();

            this.EnableControls();
        }

        private void buttonMetaData_Click(object sender, EventArgs e) {
            IList songs = this.olvSongs.SelectedObjects;
            if (songs.Count == 0)
                return;

            foreach (Song song in songs) {
                this.metaDataFetchManager.Queue(song);
            }

            // Make sure the songs involved are visible
            if (!this.radioButtonShowAll.Checked) {
                this.olvMetaData.RemoveObjects(songs);
                this.olvMetaData.AddObjects(songs);
            }
            this.olvMetaData.SelectedObjects = songs;
            this.EnsureSelectionVisible(this.olvMetaData);
            this.EnableControls();
            this.UpdateMetaDataDetails();

            // Swap to the MetaData tab, unless the Shift key is down
            if ((Control.ModifierKeys & Keys.Shift) != Keys.Shift)
                this.tabControl1.SelectedIndex = 1;
        }

        private void buttonPlay_Click(object sender, EventArgs e) {
            this.PlaySong(this.olvSongs.SelectedObject as Song);
        }

        private void buttonSearch_Click(object sender, EventArgs e) {
            Song song = this.olvSongs.SelectedObject as Song;
            if (song == null)
                return;

            string url = String.Format(Properties.Settings.Default.SearchQuery,
                HttpUtility.UrlEncode(song.Title),
                HttpUtility.UrlEncode(song.Artist));

            // Why is Process.Start() in the Diagnostics??
            System.Diagnostics.Process.Start(url);
        }

        private void olvSongs_SelectionChanged(object sender, EventArgs e) {
            this.UpdateDetails();
            this.EnableControls();
            this.UpdateStatusText();
        }

        private void textBox_Validated(object sender, EventArgs e) {
            this.CommitDetails((TextBox)sender);
        }

        #endregion

        #region Private variables

        private LyricsFetchManager fetchManager = new LyricsFetchManager();
        private AnimationHelper animationHelper;
        private TypedObjectListView<Song> typedOlvSongs;

        #endregion
    }
}
