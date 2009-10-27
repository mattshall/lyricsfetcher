/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 7/01/2008 8:52 PM
 * 
 * CHANGE LOG:
 * when who what
 * 7/01/2008 JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

using LyricsFetcher;

namespace LyricsFetcher.Tests
{
    [TestFixture]
    public class TestLyricsFetcher
    {
        [Test]
        public void TestLyricsFetch()
        {
            LyricsFetcher lf = new LyricsFetcher();
            lf.Sources.Add(new LyricsSourceLyrdb());
            lf.Sources.Add(new LyricsSourceLyricsFly());

            lf.StatusEvent += new EventHandler<LyricsFetchStatusEventArgs>(lf_StatusEvent);

            List<Song> songs = this.GetSongs();

            foreach (Song s in songs)
                Assert.IsNull(s.Lyrics);

            foreach (Song s in songs)
                lf.FetchSongLyrics(s);

            foreach (Song s in songs)
                Assert.AreNotEqual("", s.Lyrics);
        }

        void lf_StatusEvent(object sender, LyricsFetchStatusEventArgs e)
        {
            if (e.Status == LyricsFetchStatus.Done) {
                if (e.Lyrics == "")
                    e.Song.Lyrics = "Failed";
                else
                    e.Song.Lyrics = e.Lyrics;
            }
        }

        List<Song> GetSongs()
        {
            List<Song> songs = new List<Song>();
            songs.Add(new TestSong("Pon de Replay", "Rihanna", "Music of the Sun", "Hip Hop"));
            songs.Add(new TestSong("Numb", "U2", "Discotheque", "Pop"));
            songs.Add(new TestSong("Hips don't lie", "Shakira", "Oral Fixation (Vol 2)", "Latina"));
            songs.Add(new TestSong("That don't impress me much", "Shania Twain", "Come on over", "Modern Country"));
            songs.Add(new TestSong("Our lips are sealed", "The Go-Go's", "Beauty and the Beat", "Pop"));
            songs.Add(new TestSong("Rock This Party", "Bob Sinclar", "Unknown", "Dance"));
            return songs;
        }
    }
}
