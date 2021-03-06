/*
 * LyricsSource - Represents a single mechanism for finding the lyrics of a song
 *
 * Author: Phillip Piper
 * Date: 2009-02-07 11:15 AM
 *
 * Change log:
 * 2009-10-26  JPP  - Removed LyricWiki since they no longer support an API :(
 *                  - Catch more error conditions in LyrDb
 *                  - Improved cleanup of LyricsPlugin lyrics
 * 2009-03-31  JPP  - Handle encodings and strange errors in Lyrdb
 *                  - Catch more exceptions
 * 2009-03-26  JPP  - Made the backup LyrDb search less forgiving. Now looks for an exact title prefix, 
                      rather than doing a full text search (which returned some very strange matches)
                    - Changed names of LyricWiki and LyricWikiHtml
                    - Correctly handle encodings
 * 2009-03-24  JPP  - Added html version of LyricsWiki
 *                  - Removed URLs from Settings. Just use plain strings 
 * 2009-03-19  JPP  Added ability to LyrDb to lookup just by title.
 * 2009-03-03  JPP  Added LyricsFly
 * 2009-02-07  JPP  Initial version
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace LyricsFetcher {
    /// <summary>
    /// A LyricsSource represents a mechanism for finding the lyrics to a song.
    /// </summary>
    /// <remarks>
    /// Sources must be thread safe, since they will be used to fetch different songs at the same
    /// time in different threads.
    /// </remarks>
    public interface ILyricsSource {

        /// <summary>
        /// Gets the name of this source
        /// </summary>
        string Name {
            get;
        }

        /// <summary>
        /// Fetch the lyrics for the given song
        /// </summary>
        /// <param name="s">The song whose lyrics are to be fetched</param>
        /// <returns>The lyrics or an empty string if the lyrics could not be found</returns>
        string GetLyrics(Song s);
    }

    /// <summary>
    /// This source tries to fetch song lyrics from the LyricWiki site (lyricwiki.org)
    /// </summary>
    /// <remarks>In Sept 2009, LyricsWiki was forced to remove its API. So this source
    /// no longer works :( JPP 2009-10-20</remarks>
    /*
    public class LyricsSourceLyricWiki : ILyricsSource {

        /// <summary>
        /// Gets the name of this source
        /// </summary>
        public string Name {
            get {
                return "LyricWikiSoap";
            }
        }

        /// <summary>
        /// Fetch the lyrics for the given song
        /// </summary>
        /// <param name="s">The song whose lyrics are to be fetched</param>
        /// <returns>The lyrics or an empty string if the lyrics could not be found</returns>
        public string GetLyrics(Song s) {
            LyricWiki lyricWiki = new LyricWiki();
            try {
                LyricsResult lyricsResult = lyricWiki.getSong(s.Artist, s.Title);
                if (lyricsResult.lyrics == "Not found")
                    return String.Empty;

                Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
                string str = Encoding.UTF8.GetString(latin1.GetBytes(lyricsResult.lyrics));
                str = str.Replace("\r", String.Empty);
                return str.Replace("\n", System.Environment.NewLine);
            }
            catch (Exception ex) {
                // Many things can do wrong here, but there's nothing we can do it fix them
                System.Diagnostics.Debug.WriteLine("LyricWikiSoap GetLyrics failed:");
                System.Diagnostics.Debug.WriteLine(ex);
                return String.Empty;
            }
        }
    }
    */

    /// <summary>
    /// This source tries to fetch song lyrics from the Lyrdb site (www.lyrdb.com)
    /// </summary>
    public class LyricsSourceLyrdb : ILyricsSource
    {
        const string PROGRAM_NAME = "Lyricsfetcher";
        const string PROGRAM_VERSION = "0.6.1";

        /// <summary>
        /// Gets the name of this source
        /// </summary>
        public string Name {
            get {
                return "Lyrdb";
            }
        }

        /// <summary>
        /// Fetch the lyrics for the given song
        /// </summary>
        /// <param name="s">The song whose lyrics are to be fetched</param>
        /// <returns>The lyrics or an empty string if the lyrics could not be found</returns>
        public string GetLyrics(Song s) {
            // Lyrdb can't handle single or double quotes in the title (as of 12/1/2008)
            // So we just remove them
            string title = s.Title.Replace("'", "");
            title = title.Replace("\"", "");

            WebClient client = new WebClient();
            string result = String.Empty;

            try {
                // If we have both the title and the artist, we can look for a perfect match
                if (!String.IsNullOrEmpty(s.Artist)) {
                    string queryUrl = String.Format("http://webservices.lyrdb.com/lookup.php?q={0}|{1}&for=match&agent={2}/{3}",
                        s.Artist, title, PROGRAM_NAME, PROGRAM_VERSION);
                    result = client.DownloadString(queryUrl);
                }

                // If we only have the title or the perfect match failed, do a full text search using 
                // whatever information we have
                if (result == String.Empty) {
                    string queryUrl = String.Format("http://webservices.lyrdb.com/lookup.php?q={0}&for=trackname&agent={2}/{3}",
                        title, s.Artist, PROGRAM_NAME, PROGRAM_VERSION);
                    result = client.DownloadString(queryUrl);
                }
            }
            catch (Exception ex) {
                // Many things can do wrong here, but there's nothing we can do it fix them
                System.Diagnostics.Debug.WriteLine("Lyrdb GetLyrics failed:");
                System.Diagnostics.Debug.WriteLine(ex);
                return String.Empty;
            }

            // Still didn't work? Give up.
            if (result == String.Empty || result.StartsWith("error:"))
                return String.Empty;

            foreach (string x in result.Split('\n')) {
                string id = x.Split('\\')[0];
                string lyrics = client.DownloadString("http://webservices.lyrdb.com/getlyr.php?q=" + id);
                if (lyrics != String.Empty && !lyrics.StartsWith("error:")) {
                    Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
                    string str = Encoding.UTF8.GetString(latin1.GetBytes(lyrics));
                    str = str.Replace("\r", String.Empty);
                    return str.Replace("\n", System.Environment.NewLine);
                }
            }

            return String.Empty;
        }
    }

    /// <summary>
    /// This source tries to fetch song lyrics from LyricsWiki, but using
    /// the HTTP interfacee rather than the WDSL interface.
    /// </summary>
    /// <remarks><para>
    /// We generally use this source rather than the WDSL one, since
    /// this is somewhat faster: 1.0 seconds per request vs 1.4 second for 
    /// the WDSL. For 1000 songs, 0.4 seconds faster translates to about 
    /// 6.5 minutes.
    /// </para>
    /// <para>In Sept 2009, LyricsWiki was forced to remove its API. So this source
    /// no longer works :( JPP 2009-10-20</para>
    /// </remarks>
    /*
    public class LyricsSourceLyricWikiHtml : ILyricsSource
    {
        /// <summary>
        /// Gets the name of this source
        /// </summary>
        public string Name {
            get {
                return "LyricWiki";
            }
        }

        /// <summary>
        /// Fetch the lyrics for the given song
        /// </summary>
        /// <param name="s">The song whose lyrics are to be fetched</param>
        /// <returns>The lyrics or an empty string if the lyrics could not be found</returns>
        public string GetLyrics(Song s) {
            WebClient client = new WebClient();
            string queryUrl = "http://lyricwiki.org/api.php?artist=" + s.Artist + "&song=" + s.Title + "&fmt=text";
            try {
                string result = client.DownloadString(queryUrl);

                if (result == "Not found")
                    return String.Empty;

                result = result.Replace("\r", String.Empty);
                result = result.Replace("\n", System.Environment.NewLine);
                Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
                result = Encoding.UTF8.GetString(latin1.GetBytes(result));
                return result;
            }
            catch (WebException ex) {
                System.Diagnostics.Debug.WriteLine("LyricWiki GetLyrics failed:");
                System.Diagnostics.Debug.WriteLine(ex);
                return String.Empty;
            }
        }
    }
    */
    /// <summary>
    /// This source tries to fetch song lyrics from the LyricsPlugin site (www.lyricsplugin.com)
    /// </summary>
    public class LyricsSourceLyricsPlugin : ILyricsSource
    {
        /// <summary>
        /// Gets the name of this source
        /// </summary>
        public string Name {
            get {
                return "LyricsPlugin";
            }
        }

        /// <summary>
        /// Fetch the lyrics for the given song
        /// </summary>
        /// <param name="s">The song whose lyrics are to be fetched</param>
        /// <returns>The lyrics or an empty string if the lyrics could not be found</returns>
        public string GetLyrics(Song s) {
            WebClient client = new WebClient();
            string queryUrl = "http://www.lyricsplugin.com/plugin/?title=" + s.Title + "&artist=" + s.Artist;
            try {
                string result = client.DownloadString(queryUrl);

                const string lyricsMarker = @"<div id=""lyrics"">";
                int index = result.IndexOf(lyricsMarker);
                if (index >= 0) {
                    result = result.Substring(index + lyricsMarker.Length);
                    index = result.IndexOf("</div>");
                    if (index > 0) {
                        result = result.Substring(0, index);
                        result = result.Replace("<br />\n", Environment.NewLine);
                        result = result.Replace("<br />", "");
                        result = result.Replace("â€™", "'");
                        Encoding latin1 = Encoding.GetEncoding("ISO-8859-1");
                        result = Encoding.UTF8.GetString(latin1.GetBytes(result));
                        return result.Trim();
                    }
                }
            }
            catch (WebException ex) {
                System.Diagnostics.Debug.WriteLine("LyricsPlugin GetLyrics failed:");
                System.Diagnostics.Debug.WriteLine(ex);
                return String.Empty;
            }

            return String.Empty;
        }
    }

    /// <summary>
    /// This source tries to fetch song lyrics from the LyricsFly site (www.lyricsfly.com)
    /// </summary>
    /// <remarks>
    /// This class is not currently used (2009-03-14) since on the two libraries of about 3000 songs
    /// each, LyricsFly did not have *any* lyrics that LyricsWiki and LyrDB did not have.
    /// Including it as a source would have added an extra second to each lyrics fetch, but without
    /// providing any more successful hits.
    /// It was also the slowest of the lyrics services.
    /// </remarks>
    public class LyricsSourceLyricsFly : ILyricsSource  {

        const string ENCRYPTED_USER_ID = "b0`f2>290:<;>lnq)?gqxfxjxhb2\\NV@A@AVU"; //"c2cb785190773baa8-temporary.API.access";

        /// <summary>
        /// Gets or sets the userId that LyricsFly requires
        /// </summary>
        public string UserId {
            get {
                if (this.userId == null) {
                    this.userId = this.DecryptString(ENCRYPTED_USER_ID);
                }

                return this.userId;
            }
        }
        private string userId;

        private string DecryptString(string str) {
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (char c in str) {
                sb.Append((char)(c ^ i));
                i++;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the name of this source
        /// </summary>
        public string Name {
            get {
                return "LyricsFly";
            }
        }

        /// <summary>
        /// Fetch the lyrics for the given song
        /// </summary>
        /// <param name="s">The song whose lyrics are to be fetched</param>
        /// <returns>The lyrics or an empty string if the lyrics could not be found</returns>
        public string GetLyrics(Song s) {

            string queryUrl = String.Format("http://lyricsfly.com/api/api.php?i={0}&a={1}&t={2}",
                UserId, s.Artist, s.Title);

            WebClient client = new WebClient();
            string result = client.DownloadString(queryUrl);
            if (result == String.Empty)
                return String.Empty;

            /*
             * The following is taken from the LyricsFly website (http://lyricsfly.com/api/)
             *
All API calls return an XML document. The following is an example of the output:
<?xml version="1.0" encoding="utf-8"?>
-<start>
   -<sg>
      <cs>14eb6fba81</cs>
      <id>1438</id>
      <ar>U2</ar>
      <tt>Beautiful Day</tt>
      <al>All that you can't leave behind</al>
    -<tx> . . . . . Lyrics provided by lyricsfly.com [br]
   </tx>
   </sg>
</start>

Each call can return a total of 3 possible titles. Because of some album duplicates such as live performances as well as re-releases, we give you an option to pick the best result that suits your needs.

Tag description
<sg> - song
<cs> - checksum (for original URL link back construction)
<id> - song ID in the database (for original URL link back construction)
<ar> - artist name
<tt> - title of the song
<al> - album name
<tx> - lyrics text separated by [br] for line break (replace with <br>)
             *
             */

            try {
                XPathDocument doc = new XPathDocument(new StringReader(result));
                XPathNavigator nav = doc.CreateNavigator();

                if (nav.MoveToChild("start", "")) {
                    if (nav.MoveToChild("sg", "")) {
                        if (nav.MoveToChild("tx", ""))
                            return nav.Value.Replace("[br]", System.Environment.NewLine).Trim();
                    }
                }
            } catch (XmlException) {
                // Not much we can do here
            }
            return string.Empty;
        }
    }
}
