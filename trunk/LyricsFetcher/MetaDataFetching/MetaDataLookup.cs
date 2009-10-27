/*
 * This class tries to calculate the title and artist of a given track from the audio
 * file alone. It does not use the ID tags embedded within the file, but uses the music
 * itself. It does this by creating an audio "fingerprint" of the music and then looking
 * up that fingerprint on the MusicDNS service.
 * 
 * Author: Phillip Piper
 * Date: 2009-03-23 10:28 PM
 *
 * CHANGE LOG:
 * 2009-04-05 JPP   - Kill the subprocesses when the lookup is cancelled
 * 2009-04-03 JPP   - Rewrote to use MusicDNS's command line program, genpuid, rather
 *                  than doing all the work myself. Apart from having less code to maintain,
 *                  their code also runs about twice as fast (10 second per song, rather than
 *                  20 seconds).
 * 2009-03-31 JPP   Handle encodings
 * 2009-03-23 JPP   Initial Version
 * 
 * TO DO:
 */

using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Xml;
using System.Text;

using System.Diagnostics;

namespace LyricsFetcher
{
    /// <summary>
    /// A MetaDataRequest tries to calculate the name and artist of a given music track
    /// using its audio fingerprint.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class uses MusicDNS's command line program, genpuid.
    /// </remarks>
    public class MetaDataLookup : BackgroundWorkerWithProgress
    {
        /// <summary>
        /// Make an empty request
        /// </summary>
        public MetaDataLookup() {
            this.MetaData = new MetaData();
            this.Status = MetaDataStatus.Waiting;
        }

        /// <summary>
        /// Make a lookup request for the given song
        /// </summary>
        public MetaDataLookup(Song song)
            : this() {
            this.MetaData.Song = song;
        }

        #region Public properties

        public MetaData MetaData { get; private set; }

        public MetaDataStatus Status {
            get { return this.MetaData.Status; }
            set { this.MetaData.Status = value; }
        }

        #endregion

        #region Commands

        protected override object DoWork(System.ComponentModel.DoWorkEventArgs e) {
            this.Status = MetaDataStatus.InProgress;
            this.Lookup((Song)e.Argument);
            return null;
        }

        /// <summary>
        /// Try to lookup the metadata of the given song.
        /// </summary>
        /// <param name="fileName">The song whose file is to be analyzsed, Must be non-null</param>
        public void Lookup(Song song) {
            this.MetaData.Song = song;
            MetaDataStatus status = this.Lookup(song.FullPath);
            this.ChangeState(status);
        }

        /// <summary>
        /// Try to lookup the metadata of the given file.
        /// </summary>
        /// <param name="fileName">Full path to the audio file</param>
        public MetaDataStatus Lookup(string fileName) {
            if (!File.Exists(fileName))
                return MetaDataStatus.Failed_MissingFile;

            // Use genpuid to get the metadata for the file
            this.ChangeState(MetaDataStatus.InProgress_Fetching);
            string contents = this.FetchMetaData(fileName);

            if (this.IsCancelled)
                return MetaDataStatus.Cancelled;

            /*
             * Example result from MusicDNS:
             * 
<genpuid songs="1" xmlns:mip="http://musicip.com/ns/mip-1.0#">
  <track file="C:\Documents and Settings\Admin_2\My Documents\My Music\iTunes\iTunes Music\Alanis Morissette\So-Called Chaos\01 Eight Easy Steps.mp3" puid="372ee492-7a6c-cc79-b85e-279c785c1de0">
    <title>Eight Easy Steps</title>
    <artist>
      <name>Alanis Morissette</name>
    </artist>
    <puid-list>
      <puid id="372ee492-7a6c-cc79-b85e-279c785c1de0"/>
    </puid-list>
    <mip:first-release-date>2004</mip:first-release-date>
    <mip:genre-list>
      <mip:genre>
        <name>Rock/Pop</name>
      </mip:genre>
    </mip:genre-list>
  </track>
</genpuid>             
             */

            // Did we get an actual result or a failed attempt?
            int indexOfTrack = contents.IndexOf("<track");
            if (indexOfTrack < 0)
                return MetaDataStatus.Failed_Lookup;

            // Chop out the namespace declaration to make the XML simplier to process.
            // Remove possible references to a namespace we are about to remove.
            contents = contents.Substring(indexOfTrack);
            contents = contents.Replace("<mip:", "<");
            contents = contents.Replace("</mip:", "</");
            contents = @"<genpuid>" + contents;

            // Try to read the details from the response.
            try {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(contents);

                XmlNode xmlNode = doc.SelectSingleNode("genpuid/track/puid-list/puid");
                if (xmlNode != null) {
                    XmlAttribute attr = xmlNode.Attributes["id"];
                    if (attr != null) {
                        this.MetaData.Puid = attr.Value;
                        xmlNode = doc.SelectSingleNode("genpuid/track/title");
                        if (xmlNode != null)
                            this.MetaData.Title = this.DecodeString(xmlNode.InnerText);
                        xmlNode = doc.SelectSingleNode("genpuid/track/artist/name");
                        if (xmlNode != null)
                            this.MetaData.Artist = this.DecodeString(xmlNode.InnerText);
                        xmlNode = doc.SelectSingleNode("genpuid/track/genre-list/genre/name");
                        if (xmlNode != null)
                            this.MetaData.Genre = this.DecodeString(xmlNode.InnerText);
                    }
                }
            }
            catch (XmlException) {
                // Not much we can do here
            }

            if (this.IsCancelled)
                return MetaDataStatus.Cancelled;
            if (String.IsNullOrEmpty(this.MetaData.Title) && String.IsNullOrEmpty(this.MetaData.Artist))
                return MetaDataStatus.Failed_NoData;
            return MetaDataStatus.Success;
        }

        #endregion

        #region Events

        public event EventHandler<MetaDataEventArgs> StatusEvent;

        protected virtual void OnStatusEvent(MetaDataEventArgs arg) {
            if (this.StatusEvent != null)
                this.StatusEvent(this, arg);
        }

        #endregion

        #region Implementation

        public override void Cancel() {
            base.Cancel();
            if (this.cmdLineProcess != null && !this.cmdLineProcess.HasExited)
                this.cmdLineProcess.Kill();
        }

        private string FetchMetaData(string fileName) {
            this.cmdLineProcess = new Process();
            cmdLineProcess.StartInfo.FileName = "genpuid";
            cmdLineProcess.StartInfo.RedirectStandardOutput = true;
            cmdLineProcess.StartInfo.UseShellExecute = false;
            cmdLineProcess.StartInfo.CreateNoWindow = true;

            string arguments = @"{0} -xml -rmd=2 ""{1}""";
            cmdLineProcess.StartInfo.Arguments = String.Format(arguments, CLIENT_ID, fileName);
            //Console.WriteLine("cmd: {0} {1}", p.StartInfo.FileName, p.StartInfo.Arguments);

            cmdLineProcess.Start();
            cmdLineProcess.PriorityClass = ProcessPriorityClass.BelowNormal;
            string result = cmdLineProcess.StandardOutput.ReadToEnd(); // have to read before waiting!
            cmdLineProcess.WaitForExit();
            this.cmdLineProcess = null;

            //Console.WriteLine("result={0}", result);
            return result;
        }
        private Process cmdLineProcess;

        private string DecodeString(string value) {
            Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
            return Encoding.UTF8.GetString(latin1.GetBytes(value));
        }

        internal void ChangeState(MetaDataStatus status) {
            this.Status = status;
            this.OnStatusEvent(new MetaDataEventArgs(this.MetaData));
        }

        #endregion

        #region Private variables

        private const string USER_AGENT_NAME = "LyricsFetcher";
        private const string CLIENT_ID = "dd3ee9d179467124b3f735e06d4ef966";
        private const string CLIENT_VERSION = "0.5.1";

        #endregion
    }
}
