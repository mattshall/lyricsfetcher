/*
 * LyricsSource - Represents a single mechanism for finding the lyrics of a song
 *
 * Author: Phillip Piper
 * Date: 2009-02-07 11:15 AM
 *
 * Change log:
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

using LyricsFetcher.org.lyricwiki;

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
    public class LyricsSourceLyricWiki : ILyricsSource {

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
                System.Diagnostics.Debug.WriteLine(ex);
                return String.Empty;
            }
        }
    }

    /// <summary>
    /// This source tries to fetch song lyrics from the Lyrdb site (www.lyrdb.com)
    /// </summary>
    public class LyricsSourceLyrdb : ILyricsSource {

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

        string queryUrl = String.Format(Properties.Settings.Default.LyrDbLookupUrl, s.Artist, title);

        WebClient client = new WebClient();
        string result = client.DownloadString(queryUrl);
        if (result == String.Empty)
            return String.Empty;

        foreach (string x in result.Split('\n')) {
            string id = x.Split('\\')[0];
            Uri lyricsUrl = new Uri(Properties.Settings.Default.LyrDbGetUrl + id);
            string lyrics = client.DownloadString(lyricsUrl);
            if (lyrics != String.Empty)
                return lyrics;
        }

        return String.Empty;
        }
    }

    /// <summary>
    /// This source tries to fetch song lyrics from the LyricsFly site (www.lyricsfly.com)
    /// </summary>
    public class LyricsSourceLyricsFly : ILyricsSource  {

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

            string queryUrl = String.Format(Properties.Settings.Default.LyricsFlyApiUrl,
                Properties.Settings.Default.LyricsFlyUserId, s.Artist, s.Title);

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
                            return nav.Value.Replace("[br]", System.Environment.NewLine);
                    }
                }
            } catch (XmlException) {
                // Not much we can do here
            }
            return string.Empty;
        }
    }

    /*
    /// <summary>
    /// A LyricsSource encapsulates a single technique of finding the lyrics to a given song.
    /// </summary>
    /// <remarks>
    /// These strategies revolve around creating a URL which will contain the lyrics of a song,
    /// and then scraping the lyrics from the resulting page. This task naturally falls into two parts:
    /// <list type=String.Empty>
    /// <item>generating a url where the strategy expects to find a page that contains the lyrics</item>
    /// <item>extracting and cleaning the lyrics from the page fetched by step 1</item>
    /// </list>
    /// </remarks>
    [Serializable]
    public class LyricsSourceHtmlScrapper : ILyricsSource
    {
        public LyricsSourceHtmlScrapper()
        {
        }

        public LyricsSourceHtmlScrapper(string name)
        {
            this.Name = name;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        private string name;

        public string GetLyrics(Song s)
        {
            //Uri queryUrl = new Uri(this.generator.Generate(s));

            //WebClient client = new WebClient();
            //string result = client.DownloadString(queryUrl);
            //if (result == String.Empty)
            //    return String.Empty;
            //else
            //    return this.scrapper.Extract(result);
        }
    }
     */
}
