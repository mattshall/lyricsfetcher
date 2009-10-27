/*
 * These classes manage the ability to convert a given audio file into WAV format.
 * 
 * At the moment, the Open Fingerprint Architecture library only seems to work with wav files.
 * 
 * Author: Phillip Piper
 * Date: 2009-03-2 11:28 PM
 *
 * CHANGE LOG:
 * 2009-04-03 JPP   - The new implementation that uses genpuid does not need this file.
 * 2009-03-22 JPP   Initial Version
 * 
 * TO DO:
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;

namespace LyricsFetcher
{
    /// <summary>
    /// A Decoder converts a given file into a WAV file.
    /// </summary>
    public abstract class Decoder
    {
        /// <summary>
        /// Create a decoder that can handle the given list of extensions
        /// </summary>
        /// <param name="extensions">A string containing blank separated extensions</param>
        public Decoder(string extensions) {
            this.extensions.AddRange(extensions.ToLowerInvariant().Split(new char[] { ' ', '.' }));
        }

        /// <summary>
        /// Gets the list of extensions that this decoder can handle.
        /// </summary>
        /// <remarks>Each extension should be lowercase without dots</remarks>
        public IList<String> Extensions {
            get { return this.extensions; }
        }
        private List<String> extensions = new List<string>();

        /// <summary>
        /// Decode the given input file into the given output file.
        /// </summary>
        /// <param name="inputFile">Full path to the file to decode</param>
        /// <param name="outputFile">Full path to a possible location where the decoded WAV file could be placed. 
        /// It will not already exist and will have a ".wav" extension</param>
        /// <remarks>The input file will exist and its extension will be one of those given in Extensions.</remarks>
        /// <returns>The full path to the decoded WAV file</returns>
        public abstract string Decode(string inputFile, string tempWavFile);
    }

    /// <summary>
    /// This implements a WAV file format decoder, which essentially does nothing,
    /// since we want a WAV as our output.
    /// </summary>
    public class WavDecoder : Decoder
    {
        public WavDecoder(string extensions)
            : base(extensions) {
        }

        public override string Decode(string inputFile, string tempWavFile) {
            // The input file is already in the right format
            return inputFile;
        }
    }

    /// <summary>
    /// These codecs use an external, command line program to convert an audio file into a WAV file
    /// </summary> 
    public class CommandLineDecoder : Decoder
    {
        public CommandLineDecoder(string extensions, string exePath, string arguments)
            : base(extensions) {
            this.FileName = exePath;
            this.Arguments = arguments;
        }

        /// <summary>
        /// Gets or sets the name of the executable that does the decoding
        /// </summary>
        /// <remarks>This executable must be in the same directory as the application
        /// or somewhere on the PATH</remarks>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the command line arguments that will be passed to the executable.
        /// </summary>
        /// <remarks>The command line should have two placeholders: {0} for the input file
        /// to be converted, {1} for the full path to the output file. Both names will ALREADY
        /// be surrounded by double quotes.</remarks>
        public string Arguments { get; set; }

        /// <summary>
        /// Decode the given input file into the given output file.
        /// </summary>
        /// <param name="inputFile">Full path to the file to decode</param>
        /// <param name="outputFile">Full path to a possible location where the decoded WAV file could be placed. 
        /// It will not already exist and will have a ".wav" extension</param>
        /// <remarks>The input file will exist and its extension will be one of those given in Extensions.</remarks>
        /// <returns>The full path to the decoded WAV file</returns>
        public override string Decode(string inputFile, string tempWavFile) {
            Process p = new Process();
            p.StartInfo.FileName = this.FileName;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;

            string quotedInputFile = @"""" + inputFile + @"""";
            string quotedOutputFile = @"""" + tempWavFile + @"""";
            p.StartInfo.Arguments = String.Format(this.Arguments, quotedInputFile, quotedOutputFile);

            //Console.WriteLine("cmd: {0} {1}", p.StartInfo.FileName, p.StartInfo.Arguments);

            p.Start();
            p.PriorityClass = ProcessPriorityClass.BelowNormal;
            p.WaitForExit();

            return tempWavFile;
        }
    }

    /// <summary>
    /// The DecodeManager is a Singleton that maps a file to a Decoder that
    /// can convert that file to wav format.
    /// </summary>
    public class DecoderManager
    {
        #region Life and death

        /// <summary>
        /// Return the unique instance of this class
        /// </summary>
        public static DecoderManager Instance {
            get {
                return instance;
            }
        }
        private static DecoderManager instance = new DecoderManager();

        private DecoderManager() {
        }

        #endregion

        #region Commands

        /// <summary>
        /// Load our list of decoders
        /// </summary>
        /// <remarks>This should be called before FindDecoder</remarks>
        public void LoadDecoders() {
            this.Decoders = new List<Decoder>();
            this.Decoders.Add(new WavDecoder("wav"));
            this.Decoders.Add(new CommandLineDecoder("mp3", "lame.exe", "--decode {0} {1}"));
            this.Decoders.Add(new CommandLineDecoder("m4a aac", "faad.exe", "-o {1} {0}"));
            this.Decoders.Add(new CommandLineDecoder("ogg", "oggdec.exe", "-w {1} {0}"));
            this.Decoders.Add(new CommandLineDecoder("wma", "wmadec.exe", "-w -o {1} {0}"));
        }

        /// <summary>
        /// Can we decode the given file into a wav file?
        /// </summary>
        /// <param name="fileName">The path to the file to be deocded</param>
        /// <returns>Returns true if we can make a reasonable attempt</returns>
        public bool CanDecode(string fileName) {
            return this.FindDecoder(fileName) != null;
        }

        /// <summary>
        /// Find a decoder that can decode the given file into a wav file
        /// </summary>
        /// <param name="fileName">The path to the file to be deocded</param>
        /// <returns>The decoder that can handle the file</returns>
        public Decoder FindDecoder(string fileName) {
            // If the decoders haven't been loaded, do it now
            if (this.Decoders.Count == 0)
                this.LoadDecoders();

            // Can't decode a file that doesn't exist
            if (!File.Exists(fileName))
                return null;

            string extension = Path.GetExtension(fileName);

            // Can't handle a file without an extension
            if (extension.Length == 0)
                return null;

            extension = extension.Substring(1).ToLowerInvariant(); // remove dot from extension
            foreach (Decoder decoder in this.Decoders) {
                if (decoder.Extensions.Contains(extension))
                    return decoder;
            }

            // Unknown extension
            return null;
        }

        #endregion

        private IList<Decoder> Decoders = new List<Decoder>();
    }
}