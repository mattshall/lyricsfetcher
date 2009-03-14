/*
 * This class shows a splash screen while the reat of the application is loading.
 * 
 * This is a cut down version of Tom Clements very nice splash screen code.
 * His original is here: http://www.codeproject.com/KB/cs/prettygoodsplashscreen.aspx
 * 
 * CHANGE LOG
 * 2009-03-06   JPP  Initial version
 */

using System;
using System.Threading;
using System.Windows.Forms;

namespace LyricsFetcher
{
    /// <summary>
    /// Summary description for SplashScreen.
    /// </summary>
    public class SplashScreen : System.Windows.Forms.Form
    {
        // Threading
        static SplashScreen ms_frmSplash = null;
        static Thread ms_oThread = null;

        // Fade in and out.
        private double m_dblOpacityIncrement = 0.05;
        private double m_dblOpacityDecrement = 0.08;

        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Constructor
        /// </summary>
        public SplashScreen() {
            InitializeComponent();
            this.Opacity = 0.0;
            timer1.Start();
            this.ClientSize = this.BackgroundImage.Size;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 15;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SplashScreen
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(401, 171);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            this.ResumeLayout(false);

        }
        #endregion

        // ************* Static Methods *************** //

        // A static method to create the thread and 
        // launch the SplashScreen.
        static public void ShowSplashScreen() {
            // Make sure it's only launched once.
            if (ms_frmSplash != null)
                return;
            ms_oThread = new Thread(new ThreadStart(SplashScreen.ShowForm));
            ms_oThread.Name = "SplashScreenThread";
            ms_oThread.IsBackground = true;
            ms_oThread.Start();
        }

        // A private entry point for the thread.
        static private void ShowForm() {
            ms_frmSplash = new SplashScreen();
            Application.Run(ms_frmSplash);
        }

        // A static method to close the SplashScreen
        static public void CloseForm() {
            if (ms_frmSplash != null && ms_frmSplash.IsDisposed == false) {
                ms_frmSplash.Invoke(new MethodInvoker(delegate {
                    ms_frmSplash.Opacity = 1.0;
                    ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;
                }));
            }
            ms_frmSplash = null;
        }

        //********* Event Handlers ************

        // Tick Event handler for the Timer control.  Handle fade in and fade out.  Also
        // handle the smoothed progress bar.
        private void timer1_Tick(object sender, System.EventArgs e) {
            // Prevent opacity from becoming more than 1 or less than 0
            this.Opacity = Math.Min(1.0, Math.Max(0.0, this.Opacity + m_dblOpacityIncrement));
            if (this.Opacity <= 0.0) {
                this.timer1.Stop();
                //this.Close();
                Application.ExitThread();
            }
        }

        // Close the form if they double click on it.
        private void SplashScreen_DoubleClick(object sender, System.EventArgs e) {
            SplashScreen.CloseForm();
        }
    }
}
