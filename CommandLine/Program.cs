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

using WMFSDKWrapper;

public class TestSong : Song
{
    public TestSong(string title, string artist, string album, string genre) :
        base(title, artist, album, genre) {
    }

    public override string FullPath {
        get { return String.Empty; }
    }

    public override void Commit() {
    }

    public override void GetLyrics() {
    }
}

namespace LyricsFetcher.CommandLine
{
    class Program
    {
       

        static void TestWMPLyrics() {
            WindowsMediaPlayer wmpPlayer = new WindowsMediaPlayer();
            IWMPPlaylist playlist = wmpPlayer.mediaCollection.getByAttribute("MediaType", "audio");
            IWMPMedia media = playlist.get_Item(100);
            Console.WriteLine(media.name);
            Console.WriteLine(media.getItemInfo("Lyrics"));
            //media.setItemInfo("Lyrics", "");
        }

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

            ILyricsSource source2 = new LyricsSourceLyricsPlugin();
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
            lib.MaxSongsToFetch = 150;
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
            songs = songs.GetRange(0, 10);

            //TestOneSource(songs, new LyricsSourceLyricWiki());
            //TestOneSource(songs, new LyricsSourceLyricWikiHtml());
            TestOneSource(songs, new LyricsSourceLyricsPlugin());
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
            Console.WriteLine("Successes: {0}, failure: {1}, time: {2} seconds ({3} per song)", 
                successes, failures, sw.Elapsed, sw.ElapsedMilliseconds / songs.Count);
        }

        static void lfm_StatusEvent(object sender, LyricsFetchStatusEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Status);
        }

        static void TestMetaDataEditor() {
            string file = @"C:\Documents and Settings\Admin_2\My Documents\My Music\iTunes\iTunes Music\After the fire\Der Kommissar\01 Der Kommissar.wma";
            using (MetaDataEditor mde = new MetaDataEditor(file)) {
                //mde.SetFieldValue("WM/Lyrics", "These are some more of my favourite things");
                //mde.SetFieldValue("WM/Genre", "80's rock");
                Console.WriteLine(mde.GetFieldValue("WM/Year"));
                Console.WriteLine(mde.GetFieldValue("WM/Lyrics"));
                Console.WriteLine(mde.GetFieldValue("Genre"));
                Console.WriteLine(mde.GetFieldValue("WM/Publisher"));
                Console.WriteLine(mde.GetFieldValue("unknown"));
            }
        }

        static void TestMetaDataEditor2() {
            WindowsMediaPlayer wmpPlayer = new WindowsMediaPlayer();
            IWMPPlaylist playlist = wmpPlayer.mediaCollection.getByAttribute("MediaType", "audio");
            IWMPMedia media = playlist.get_Item(100);
            Console.WriteLine(media.name);
            Console.WriteLine(media.getItemInfo("WM/Genre"));
            Console.WriteLine("-");

            using (MetaDataEditor mde = new MetaDataEditor(media.sourceURL)) {
                //mde.SetFieldValue("WM/Lyrics", "MORE LIKE THIS things");
                //mde.SetFieldValue("WM/Genre", "80's rock");
                Console.WriteLine(mde.GetFieldValue("WM/Year"));
                Console.WriteLine(mde.GetFieldValue("WM/Lyrics"));
                Console.WriteLine(mde.GetFieldValue("WM/Genre"));
                Console.WriteLine(mde.GetFieldValue("WM/Publisher"));
                Console.WriteLine(mde.GetFieldValue("unknown"));
            }
        }

        static void TestLyricsSource() {
            //ILyricsSource strategy = new LyricsSourceLyricWiki();
            //ILyricsSource strategy = new LyricsSourceLyricWikiHtml();
            //ILyricsSource strategy = new LyricsSourceLyrdb();
            //ILyricsSource strategy = new LyricsSourceLyricsPlugin();
            ILyricsSource strategy = new LyricsSourceLyricsFly();

            //TestSong song = new TestSong("Lips don't hie", "Shakira", "Music of the Sun", "");
            //TestSong song = new TestSong("Hips don't lie", "Shakira", "Music of the Sun", "");
            //TestSong song = new TestSong("Pon de replay", "Rihanna", "Music of the Sun", "");
            //TestSong song = new TestSong("I Love to Move in Here", "Moby", "Last Night", "");
            TestSong song = new TestSong("Year 3000", "Busted", "", "");

            Console.WriteLine(song);
            Console.WriteLine(strategy.GetLyrics(song));
        }

        static void TestMetaDataLookup() {
            string file = @"C:\Documents and Settings\Admin_2\My Documents\My Music\iTunes\iTunes Music\After the fire\Der Kommissar\01 Der Kommissar.mp3";
            //string file = @"C:\Documents and Settings\Admin_2\My Documents\My Music\iTunes\iTunes Music\Alanis Morissette\So-Called Chaos\01 Eight Easy Steps.mp3";
            //string file = @"C:\Documents and Settings\Admin_2\My Documents\My Music\iTunes\iTunes Music\Unknown Artist\Unknown Album\16 Pump UP the jam.mp3";
            //string file = @"C:\Documents and Settings\Admin_2\My Documents\My Music\iTunes\iTunes Music\Ziqo\Unknown Album\01 Vamos Embora.mp3";
            
            MetaDataLookup req = new MetaDataLookup();
            Console.WriteLine("looking up '{0}'...", file);
            req.Lookup(file);
            Console.WriteLine("puid={0}", req.MetaData.Puid);
            Console.WriteLine("title={0}", req.MetaData.Title);
            Console.WriteLine("artist={0}", req.MetaData.Artist);
            Console.WriteLine("genre={0}", req.MetaData.Genre);
        }

        static void TestManyMetaDataLookup() {
            ITunesLibrary lib = new ITunesLibrary();
            lib.MaxSongsToFetch = 50;
            lib.LoadSongs();
            lib.WaitLoad();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            try {
                foreach (Song song in lib.Songs) {
                    MetaDataLookup req = new MetaDataLookup();
                    //MetaDataLookupOld req = new MetaDataLookupOld();
                    req.Lookup(song);
                    if (req.Status == MetaDataStatus.Success) {
                        if (song.Title == req.MetaData.Title && song.Artist == req.MetaData.Artist)
                            System.Diagnostics.Debug.WriteLine(String.Format("same: {0}", song));
                        else
                            System.Diagnostics.Debug.WriteLine(String.Format("different: {0}. title={1}, artist={2}", song, req.MetaData.Title, req.MetaData.Artist));
                    } else {
                        System.Diagnostics.Debug.WriteLine(String.Format("failed: {0}", song));
                    }
                }
            }
            finally {
                sw.Stop();
                System.Diagnostics.Debug.WriteLine(String.Format("Tried {0} song in {1} ({2} secs per song)", lib.Songs.Count, sw.Elapsed, (sw.ElapsedMilliseconds / lib.Songs.Count) / 1000));
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("start");

            TestSources();

            //TestManyMetaDataLookup();

            //Console.WriteLine("start");
            //string value = GetFieldByName(@"C:\Documents and Settings\Admin_2\My Documents\My Music\iTunes\iTunes Music\After the fire\Der Kommissar\01 Der Kommissar.mp3", "WM/Lyrics");
            //Console.WriteLine(value);
            //Console.WriteLine(GetFieldByName(@"C:\Documents and Settings\Admin_2\My Documents\My Music\iTunes\iTunes Music\After the fire\Der Kommissar\01 Der Kommissar.mp3", "WM/Genre"));
            //Console.WriteLine(GetFieldByName(@"C:\Documents and Settings\Admin_2\My Documents\My Music\iTunes\iTunes Music\After the fire\Der Kommissar\01 Der Kommissar.mp3", "WM/Publisher"));
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

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
