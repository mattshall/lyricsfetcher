using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LyricsFetcher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the library.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SplashScreen.ShowSplashScreen();
            Application.DoEvents();

            Application.Run(new Form1());
        }
    }
}
