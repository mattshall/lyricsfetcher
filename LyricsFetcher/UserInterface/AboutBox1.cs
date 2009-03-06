/*
 * This form shows the About box for the application.
 *
 * Author: Phillip Piper
 * Date: 2009-02-09 14:28
 *
 * CHANGE LOG:
 * 2009-03-06 JPP  Changed layout
 * 2009-02-09 JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace LyricsFetcher
{
    partial class AboutBox1 : Form
    {
        public AboutBox1() {
            this.InitializeComponent();
            this.Text = String.Format("About {0}", this.AssemblyProduct);
            this.labelVersion.Text = String.Format("Version {0}", this.AssemblyVersion);
        }

        #region Assembly Attribute Accessors

        public string AssemblyProduct {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyVersion {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /*
         * I don't use any of these, but it's still interesting to see how it is done.
         * 
         
        public string AssemblyTitle {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0) {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "") {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyDescription {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyCopyright {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany {
            get {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0) {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
         * 
         */

        #endregion

        #region UI Commands

        private void okButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e) {
            using (new WaitCursor()) {
                System.Diagnostics.Process.Start(e.LinkText);
            }
        }

        #endregion
    }
}
