/*
 * This class manages persistent user settings between application runs.
 *
 * Author: Phillip Piper
 * Date: 2009-02-10 10:28 PM
 *
 * CHANGE LOG:
 * 2009-02-15 JPP  Added ListViewState
 * 2009-02-10 JPP  Initial Version
 */

using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace LyricsFetcher
{
    /// <summary>
    /// This class manages persistent user settings between application runs.
    /// </summary>
    public class UserPreferences
    {
        #region Preferences

        /// <summary>
        /// What is the state of the main listview?
        /// </summary>
        public byte[] ListViewState
        {
            get
            {
                if (this.preferences.ContainsKey("ListViewState"))
                    return (byte[])this.preferences["ListViewState"];
                else
                    return null;
            }
            set { this.preferences["ListViewState"] = value; }
        }

        /// <summary>
        /// Where is the main window located?
        /// </summary>
        public Point MainWindowLocation
        {
            get
            {
                Point? point = this.preferences["MainWindowLocation"] as Point?;
                if (point.HasValue)
                    return (Point)point;
                else
                    return new Point(-1, -1);
            }
            set { this.preferences["MainWindowLocation"] = value; }
        }

        /// <summary>
        /// How big is the main window?
        /// </summary>
        public Size MainWindowSize
        {
            get
            {
                Size? size = this.preferences["MainWindowSize"] as Size?;
                if (size.HasValue)
                    return (Size)size;
                else
                    return new Size(-1, -1);
            }
            set { this.preferences["MainWindowSize"] = value; }
        }

        /// <summary>
        /// Is the main window maximized?
        /// </summary>
        public bool IsMainWindowMaximized
        {
            get
            {
                bool? isMaximized = this.preferences["IsMainWindowMaximized"] as bool?;
                if (isMaximized.HasValue)
                    return (bool)isMaximized;
                else
                    return false;
            }
            set { this.preferences["IsMainWindowMaximized"] = value; }
        }

        /// <summary>
        /// Which library was selected by the user?
        /// </summary>
        public LibraryApplication LibraryApplication
        {
            get
            {
                LibraryApplication? app = this.preferences["LibraryApplication"] as LibraryApplication?;
                if (app.HasValue)
                    return (LibraryApplication)app;
                else
                    return LibraryApplication.Unknown;
            }
            set { this.preferences["LibraryApplication"] = value; }
        }

        #endregion

        #region Load and Store

        /// <summary>
        /// Load the user preferences from its normal location
        /// </summary>
        public void Load()
        {
            this.Load(this.GetUserPreferencesPath());
        }

        /// <summary>
        /// Save the user preferences to its normal location
        /// </summary>
        public void Save()
        {
            this.Save(this.GetUserPreferencesPath());
        }

        /// <summary>
        /// Load the user preferences from the given path.
        /// </summary>
        /// <remarks>This discards any previous preferences, even if the load fails</remarks>
        /// <param name="path">The full path name to a file created by Save()</param>
        public void Load(string path)
        {
            this.preferences = new Hashtable();

            // Can't do anything more if the file doesn't exist
            if (!File.Exists(path))
                return;

            BinaryFormatter deserializer = new BinaryFormatter();
            using (FileStream fs = File.OpenRead(path)) {
                try {
                    this.preferences = deserializer.Deserialize(fs) as Hashtable;
                }
                catch (System.Runtime.Serialization.SerializationException ex) {
                    System.Diagnostics.Debug.WriteLine("Load preferences failed");
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }

        /// <summary>
        /// Save the preferences to the given path
        /// </summary>
        /// <remarks>Any existing saved prefs will be deleted.</remarks>
        /// <param name="path">The full path name of the prefs file</param>
        public void Save(string path)
        {
            // Make the directory for the prefs if it doesn't exist
            if (!File.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            // Remove any existing cache
            if (File.Exists(path))
                File.Delete(path);

            // Store the cache as a binary stream
            using (FileStream fs = File.Create(path)) {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(fs, this.preferences);
            }
        }

        #endregion

        private string GetUserPreferencesPath()
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = Path.Combine(path, "LyricsFetcher");
            path = Path.Combine(path, "Preferences.bin");
            return path;
        }

        private Hashtable preferences = new Hashtable();
    }
}
