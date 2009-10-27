/*
 * The main windw for the LyricsFetcher application
 *
 * Author: Phillip Piper
 * Date: 2009-01-20 21:08
 *
 * CHANGE LOG:
 * 2009-03-31  JPP  - Added EnsureSelectionVisible() and ReportCommitFailed() utilities
 * 2009-03-28  JPP  - Fissioned into three files: this one just for form events,
 *                    and one file for each tab.
 * 2009-03-25  JPP  - When the user changes the lyrics field manually, make sure
 *                    the lyrics cache is updated
 * 2009-03-21  JPP  - Allow the lyrics sources to be changed via the Settings
 *                  - Catch errors when updating metadata
 * 2009-02-15  JPP  - Added multi-object details (both showing and updating values)
 *                  - Added autocomplete to details
 *                  - Added Genre and removed Kind from visible properties
 * 2009-02-13  JPP  Added checks for network availability
 * 2009-01-20  JPP  Initial Version
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Web;
using System.Windows.Forms;

namespace LyricsFetcher
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        #region Public properties

        /// <summary>
        /// What settings has the user chosen?
        /// </summary>
        public UserPreferences Preferences {
            get { return this.preferences; }
        }
        private UserPreferences preferences = new UserPreferences();

        #endregion

        #region Initializing/Terminating

        /// <summary>
        /// Initialize the network availability checking
        /// </summary>
        private void InitializeNetworkAvailability() {
            // Listen for network availability events
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(
                delegate(object sender, NetworkAvailabilityEventArgs e) {
                    this.BeginInvoke(new MethodInvoker(delegate() {
                        this.CheckNetworkStatus();
                        this.EnableControls();
                    }));
                }
            );
            // Display the current state
            this.CheckNetworkStatus();
        }


        private void InitializeLyricsFetchManager() {
            // Make a list of all known sources
            List<ILyricsSource> allSources = new List<ILyricsSource>();
            allSources.Add(new LyricsSourceLyrdb());
            allSources.Add(new LyricsSourceLyricsPlugin());
            //allSources.Add(new LyricsSourceLyricsFly());

            // Register the sources named in the settings
            foreach (String sourceName in Properties.Settings.Default.LyricsSources) {
                foreach (ILyricsSource source in allSources) {
                    if (String.Compare(source.Name, sourceName.Trim(), true) == 0) {
                        this.fetchManager.RegisterSource(source);
                    }
                }
            }

            // If nothing was named in the settings, use the default sources
            if (this.fetchManager.Sources.Count == 0) {
                this.fetchManager.RegisterSource(new LyricsSourceLyrdb());
                this.fetchManager.RegisterSource(new LyricsSourceLyricsPlugin());
            }

            // Do the other lyrics fetcher initialization
            this.fetchManager.StatusEvent += new EventHandler<LyricsFetchStatusEventArgs>(fetchManager_StatusEvent);
            this.fetchManager.AutoUpdateLyrics = true;
            this.fetchManager.Start();
        }

        private void LoadUserPreferences() {
            this.Preferences.Load();

            // If no library is chosen, default to iTunes
            if (this.Preferences.LibraryApplication == LibraryApplication.Unknown)
                this.Preferences.LibraryApplication = LibraryApplication.ITunes;

            // If iTunes has been chosen, but it's not installed, switch to media player
            // (this also covers case where iTunes has been uninstalled since LyricsFetcher last ran)
            if (this.Preferences.LibraryApplication == LibraryApplication.ITunes && !ITunes.HasITunes)
                this.Preferences.LibraryApplication = LibraryApplication.WindowsMediaPlayer;

            Size size = this.Preferences.MainWindowSize;
            if (size.Height > 0)
                this.Size = size;

            // Make sure the stored location is still reasonably visible
            Point location = this.Preferences.MainWindowLocation;
            bool isLocationValid = false;
            foreach (Screen screen in Screen.AllScreens) {
                Rectangle rect = screen.Bounds;
                rect.Height -= 32;
                rect.Width -= 32;
                if (rect.Contains(location)) {
                    isLocationValid = true;
                    break;
                }
            }
            if (isLocationValid)
                this.Location = location;
            else
                this.StartPosition = FormStartPosition.CenterScreen;

            if (this.Preferences.IsMainWindowMaximized)
                this.WindowState = FormWindowState.Maximized;

            // Load genres to ignore from settings
            foreach (string genre in Properties.Settings.Default.IgnoredGenres)
                Song.GenresToIgnore.Add(genre);
        }


        /// <summary>
        /// Write any user changable setting to persistent storage
        /// </summary>
        private void SaveUserPreferences() {
            this.Preferences.ListViewState = this.olvSongs.SaveState();
            this.Preferences.MetaDataListViewState = this.olvMetaData.SaveState();
            this.Preferences.IsMainWindowMaximized = (this.WindowState == FormWindowState.Maximized);

            // If the window is maximized, we need to keep the restore bounds rather than the actual
            // window dimensions -- since they are, funnily enough, maximized
            if (this.Preferences.IsMainWindowMaximized) {
                this.Preferences.MainWindowLocation = this.RestoreBounds.Location;
                this.Preferences.MainWindowSize = this.RestoreBounds.Size;
            } else {
                this.Preferences.MainWindowLocation = this.Location;
                this.Preferences.MainWindowSize = this.Size;
            }
            this.Preferences.Save();
        }

        #endregion

        #region UI commands

        /// <summary>
        /// Update the UI to display the network availability
        /// </summary>
        private void CheckNetworkStatus() {
            // Calculating this value is expensive so we cache it
            this.isNetworkAvailable = NetworkInterface.GetIsNetworkAvailable();
            if (this.isNetworkAvailable) {
                this.toolStripStatusLabel2.Image = null;
                this.toolStripStatusLabel2.Text = "";
            } else {
                this.toolStripStatusLabel2.Image = Properties.Resources.burn16;
                this.toolStripStatusLabel2.Text = "No network available";
            }
        }
        private bool isNetworkAvailable;


        /// <summary>
        /// Enable or disable the controls to match the state of the UI
        /// </summary>
        private void EnableControls() {
            if (this.tabControl1.SelectedIndex == 0)
                this.EnableControlsLyrics();
            else
                this.EnableControlsMetaData();
        }

        private void UpdateStatusText() {
            if (this.tabControl1.SelectedIndex == 0)
                this.UpdateStatusTextLyrics();
            else
                this.UpdateStatusTextMetaData();
        }

        #endregion

        #region Utilities

        private void PlaySong(Song song) {
            if (song == null)
                return;

            if (this.library.IsPlaying(song))
                this.library.StopPlaying();
            else {
                try {
                    this.library.Play(song);
                }
                catch (COMException ex) {
                    string msg = String.Format(Properties.Resources.SongFailedToPlayMsg, song.FullPath, ex.Message);
                    MessageBox.Show(this, msg, Properties.Resources.AppName,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void EnsureSelectionVisible(BrightIdeasSoftware.ObjectListView listView) {
            if (listView.SelectedIndices.Count > 0)
                listView.TopItemIndex = listView.SelectedIndices[0];
        }

        private bool ReportCommitFailed(Song song, COMException ex) {
            string msg = String.Format(Properties.Resources.SongFailedToUpdateMsgITunes, song.Title, song.FullPath, ex.Message);
            if (this.Preferences.LibraryApplication == LibraryApplication.WindowsMediaPlayer)
                msg = String.Format(Properties.Resources.SongFailedToUpdateMsgWmp, song.Title, song.FullPath, ex.Message);
            DialogResult result = MessageBox.Show(this, msg, Properties.Resources.AppName,
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            return result == DialogResult.Cancel;
        }

        #endregion

        #region UI Event handlers

        private void Form1_Load(object sender, EventArgs e) {
            this.InitializeNetworkAvailability();
            this.LoadUserPreferences();
            this.InitializeLyricsTab();
            this.InitializeMetaDataTab();
            this.EnableControls();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.library != null)
                this.library.Close();

            if (this.fetchManager != null)
                this.fetchManager.CancelAll();

            if (this.metaDataFetchManager != null)
                this.metaDataFetchManager.CancelAll();

            this.SaveUserPreferences();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            System.Diagnostics.Debug.WriteLine("closed");
        }

        private void Form1_Layout(object sender, LayoutEventArgs e) {
            SplashScreen.CloseForm();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            this.EnableControls();
            this.UpdateStatusText();
        }

        #endregion

        #region Menu Commands

        private void chooseLibraryToolStripMenuItem_Click(object sender, EventArgs e) {
            ChooseLibraryForm form = new ChooseLibraryForm();
            form.Library = this.Preferences.LibraryApplication;
            if (form.ShowDialog(this) == DialogResult.OK) {
                if (this.Preferences.LibraryApplication != form.Library) {
                    this.Preferences.LibraryApplication = form.Library;
                    this.InitializeSongLibrary();
                }
            }
        }

        private void toolStripMenuItemReloadLibrary_Click(object sender, EventArgs e) {
            this.InitializeSongLibrary();
        }

        private void toolStripMenuItemDiscardCache_Click(object sender, EventArgs e) {
            DialogResult result = MessageBox.Show(this,
                Properties.Resources.DiscardCacheMsg, Properties.Resources.AppName, MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            if (result == DialogResult.OK) {
                this.library.DiscardCache();
                MessageBox.Show(this, Properties.Resources.CacheDiscardedMsg,
                    Properties.Resources.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void aboutLyricsFetcherToolStripMenuItem_Click(object sender, EventArgs e) {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog(this);
        }

        #endregion

        #region Private variables

        private SongLibrary library;

        #endregion
    }
}
