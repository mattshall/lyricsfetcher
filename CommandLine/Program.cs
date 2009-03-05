using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.IO;

using LyricsFetcher;
using iTunesLib;
using WMPLib;

public class TestSong : Song
{
    public TestSong(string title, string artist, string album, string genre) :
        base(title, artist, album, genre)
    {
    }

    public override void Commit()
    {
    }

    public override void GetLyrics()
    {
    }
}

namespace LyricsFetcher.CommandLine
{
    class Program
    {
        static void TestSecondTry() {
            // Is it actually worthwhile doing the second or subsequent attempt?
            ITunesLibrary lib = new ITunesLibrary();
            //lib.MaxSongsToFetch = 1000;
            lib.LoadSongs();
            lib.WaitLoad();

            List<Song> songs = lib.Songs.FindAll(
                    delegate(Song s) {
                        LyricsStatus status = s.LyricsStatus;
                        return (status == LyricsStatus.Untried ||
                            status == LyricsStatus.Failed ||
                            status == LyricsStatus.Success);
                    }
                );

            ILyricsSource source2 = new LyricsSourceLyricWiki();
            ILyricsSource source1 = new LyricsSourceLyrdb();
            ILyricsSource source3 = new LyricsSourceLyricsFly();

            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();
            Stopwatch sw3 = new Stopwatch();
            
            int failures = 0;
            int success1 = 0;
            int success2 = 0;
            int success3 = 0;

            foreach (Song song in songs) {
                sw1.Start();
                string lyrics = source1.GetLyrics(song);
                sw1.Stop();

                if (lyrics == string.Empty) {
                    sw2.Start();
                    lyrics = source2.GetLyrics(song);
                    sw2.Stop();

                    if (lyrics == string.Empty) {
                        sw3.Start();
                        lyrics = source3.GetLyrics(song);
                        sw3.Stop();

                        if (lyrics == string.Empty)
                            failures++;
                        else
                            success3++;
                    } else
                        success2++;
                } else
                    success1++;
            }

            Console.WriteLine("1st try: successes: {0} ({1}%), time: {2} ({3} each)",
                success1, (success1 * 100 / songs.Count), sw1.Elapsed, sw1.ElapsedMilliseconds / songs.Count);
            Console.WriteLine("2st try: successes: {0} ({1}%), time: {2} ({3} each)",
                success2, (success2 * 100 / songs.Count), sw2.Elapsed, sw2.ElapsedMilliseconds / (songs.Count - success1));
            Console.WriteLine("3st try: successes: {0} ({1}%), time: {2} ({3} each)",
                success3, (success3 * 100 / songs.Count), sw3.Elapsed, sw3.ElapsedMilliseconds / (songs.Count - success1 - success2));
            Console.WriteLine("failures: {0} ({1}%)",
                failures, (failures * 100 / songs.Count));
        }

        static void TestSources() {
            ITunesLibrary lib = new ITunesLibrary();
            //lib.MaxSongsToFetch = 1000;
            lib.LoadSongs();
            lib.WaitLoad();

            List<Song> songs = lib.Songs.FindAll(
                    delegate(Song s) {
                        LyricsStatus status = s.LyricsStatus;
                        return (status == LyricsStatus.Untried ||
                            status == LyricsStatus.Failed ||
                            status == LyricsStatus.Success);
                    }
                );
            TestOneSource(songs, new LyricsSourceLyricWiki());
            TestOneSource(songs, new LyricsSourceLyrdb());
            TestOneSource(songs, new LyricsSourceLyricsFly());
        }

        private static void TestOneSource(List<Song> songs, ILyricsSource source) {
            Console.WriteLine("Testing {0}...", source.Name);
            int successes = 0;
            int failures = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (Song song in songs) {
                if (source.GetLyrics(song) == string.Empty)
                    failures++;
                else
                    successes++;
            }
            sw.Stop();
            Console.WriteLine("Successes: {0}, failure: {1}, time: {2} seconds", successes, failures, sw.Elapsed);
        }

        static void lfm_StatusEvent(object sender, FetchStatusEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Status);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("start");
            TestSecondTry();
            Console.ReadKey();

            //TestSong s = new TestSong("Desire2", "U2", "Pop", "Rock");
            //Console.WriteLine(s);
            //LyricsSourceLyricsFly source = new LyricsSourceLyricsFly();
            //string lyrics = source.GetLyrics(s);
            //Console.WriteLine(lyrics);
            //Console.ReadKey();

            //Console.WriteLine(ITunes.Instance.Version);
            //int i1, i2, i3, i4, i5, i6;
            //ITunes.Instance.AllTracks[3].GetITObjectIDs(out i1, out i2, out i3, out i4);

            //Stopwatch sw1 = new Stopwatch();
            //sw1.Start();
            //for (int i = 1; i < 1000; i++) {
            //    ITunes.Instance.GetObjectByIds(i1, i2, i3, i4);
            //}
            //sw1.Stop();
            //Console.WriteLine("GetObjectByIds={0}ms", sw1.ElapsedMilliseconds);
            //ITunes.Instance.GetPersistentIds(ITunes.Instance.AllTracks[4], out i5, out i6);

            //Stopwatch sw2 = new Stopwatch();
            //sw2.Start();
            //for (int i = 1; i < 1000; i++) {
            //    ITunes.Instance.GetTrackByPersistentIds(i5, i6);
            //}
            //sw2.Stop();
            //Console.WriteLine("GetTrackByPersistentIds={0}ms", sw2.ElapsedMilliseconds);

            //// Load the whole xml file into memory, and then remove the DTD.
            //// We remove that so we can read the xml even when not connected to the network
            //string xml = File.ReadAllText(ITunes.Instance.XmlPath);
            //int docTypeStart = xml.IndexOf("<!DOCTYPE");
            //int docTypeEnd = xml.IndexOf("0.dtd\">") + "0.dtd\">".Length;
            //string xml2 = xml.Remove(docTypeStart, docTypeEnd - docTypeStart);

            //XPathDocument doc = new XPathDocument(new StringReader(xml2));
            //XPathNavigator nav = doc.CreateNavigator();

            //// Move to plist
            //bool success = nav.MoveToChild("plist", "");

            //// Move to master library
            //success = nav.MoveToChild("dict", "");

            //// Move to tracks
            //success = nav.MoveToChild("dict", "");

            //// Move to first track info
            //success = nav.MoveToChild("dict", "");

            //ArrayList allData = new ArrayList();
            //while (success) {
            //    success = nav.MoveToFirstChild();
            //    Hashtable data = new Hashtable();

            //    while (success) {
            //        string key = nav.Value;
            //        success = nav.MoveToNext();
            //        string value = nav.Value;
            //        data[key] = value;
            //        success = nav.MoveToNext();
            //    }
            //    allData.Add(data);
            //    success = nav.MoveToParent();
            //    success = nav.MoveToNext("dict", "");
            //}
            //Console.WriteLine("Read {0} songs", allData.Count);
            //Console.ReadKey();

            //------------------------------------------------------------

            //string xml = File.ReadAllText(ITunes.Instance.XmlPath);
            //int docTypeStart = xml.IndexOf("<!DOCTYPE");
            //int docTypeEnd = xml.IndexOf("0.dtd\">") + "0.dtd\">".Length;
            //string xml2 = xml.Remove(docTypeStart, docTypeEnd - docTypeStart);

            //using (XmlTextReader reader = new XmlTextReader(new StringReader(xml2))) {
            //    reader.MoveToContent();
            //    reader.ReadToDescendant("dict"); // move to library
            //    reader.ReadToDescendant("dict"); // move to track list

            //    int i = 0;
            //    while (reader.Read()) {
            //            switch (reader.NodeType) {
            //                case XmlNodeType.Element:
            //                    Console.Write("<{0}", reader.Name);
            //                    while (reader.MoveToNextAttribute()) {
            //                        Console.Write(" {0}='{1}'", reader.Name, reader.Value);
            //                    }
            //                    Console.Write(">");
            //                    break;
            //                case XmlNodeType.Text:
            //                    Console.Write(reader.Value);
            //                    break;
            //                case XmlNodeType.EndElement:
            //                    Console.Write("</{0}>", reader.Name);
            //                    break;
            //            }
            //        if (i > 100)
            //            break;
            //        i++;
            //    }
            //}
            //Console.ReadKey();

            //------------------------------------------------------------

            //System.Diagnostics.Debug.WriteLine("start");
            //Stopwatch watch = new Stopwatch();
            //SongLibrary library = new WmpLibrary();
            ////SongLibrary library = new ITunesLibrary();
            ////library.MaxSongsToFetch = 1000;
            //watch.Start();
            //library.LoadSongs();
            //library.WaitLoad();
            //watch.Stop();
            //System.Diagnostics.Debug.WriteLine(String.Format("Loaded {0} songs in {1}ms",
            //    library.Songs.Count, watch.ElapsedMilliseconds));

            //------------------------------------------------------------
            
            //LyricsFetchManager lfm = new LyricsFetchManager();
            //lfm.AutoUpdateLyrics = true;
            //lfm.StatusEvent += new EventHandler<FetchStatusEventArgs>(lfm_StatusEvent);
            //lfm.Sources.Add(new LyricsSourceLyrdb());
            //lfm.Sources.Add(new LyricsSourceLyricWiki());

            //System.Diagnostics.Debug.WriteLine("--------------");

            //Song song = new TestSong("seconds", "u2", "Music of the Sun", "");
            //lfm.Queue(song);

            //lfm.Start();
            //lfm.WaitUntilFinished();

            //System.Diagnostics.Debug.WriteLine(song);
            //System.Diagnostics.Debug.Write(song.Lyrics);
            //System.Diagnostics.Debug.WriteLine("========");

            //------------------------------------------------------------
            //ILyricsSource strategy = new LyricsSourceLyricWiki();
            //ILyricsSource strategy = new LyricsSourceLyrdb();
            //TestSong song = new TestSong("Pon de Replay", "Rihanna", "Music of the Sun", "");

            //System.Diagnostics.Debug.WriteLine(strategy.GetLyrics(song));

            //------------------------------------------------------------

            //System.Diagnostics.Debug.WriteLine(Wmp.Instance.Player.versionInfo);

            //SongLibrary library = new WmpLibrary();
            //library.MaxSongsToFetch = 100;
            //foreach (Song x in library.Songs)
            //    System.Diagnostics.Debug.WriteLine(x);

            //------------------------------------------------------------

            //IWMPPlaylist playlist = Wmp.Instance.AllTracks;

            //System.Diagnostics.Debug.WriteLine(Wmp.Instance.Player.versionInfo);
            //for (int i = 0; i < playlist.count; i++) {
            //    IWMPMedia media = playlist.get_Item(i);
            //    System.Diagnostics.Debug.WriteLine(media.name);
            //}
        }
    }
}
