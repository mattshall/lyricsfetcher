/*
 * The forms allows the user to choose which media library they want.
 *
 * Author: Phillip Piper
 * Date: 2009-02-09 10:20 PM
 *
 * CHANGE LOG:
 * 2009-02-09 JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LyricsFetcher
{
    public partial class ChooseLibraryForm : Form
    {
        public ChooseLibraryForm()
        {
            InitializeComponent();
        }

        #region Public properties

        /// <summary>
        /// Which library has the user chosen?
        /// </summary>
        public LibraryApplication Library
        {
            get { return this.library; }
            set { this.library = value; }
        }
        private LibraryApplication library = LibraryApplication.Unknown;

        #endregion

        #region Event handlers

        private void radioButtonITunes_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonITunes.Checked)
                this.Library = LibraryApplication.ITunes;
        }

        private void radioButtonWmp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonWmp.Checked)
                this.Library = LibraryApplication.WindowsMediaPlayer;
        }

        private void ChooseLibraryForm_Load(object sender, EventArgs e)
        {
            // Does this machine have iTunes installed?
            if (ITunes.HasITunes) {
                if (this.Library == LibraryApplication.WindowsMediaPlayer)
                    this.radioButtonWmp.Checked = true;
                else
                    this.radioButtonITunes.Checked = true;
            } else {
                this.radioButtonITunes.Text = "iTunes is not installed";
                this.radioButtonITunes.Enabled = false;
                this.radioButtonWmp.Checked = true;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}
