/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 2009-02-09 10:52 PM
 * 
 * CHANGE LOG:
 * 2009-02-09  JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

using LyricsFetcher;

namespace LyricsFetcher.Tests
{
    [TestFixture]
    public class TestLyricsCache
    {
        [Test]
        public void TestEmpty()
        {
            LyricsCache cache = new LyricsCache();
            Song s1 = new TestSong("title", "artist", "album", "genre");
            Assert.IsNull(cache.GetLyrics(s1));
        }

        [Test]
        public void TestCaching()
        {
            LyricsCache cache = new LyricsCache();
            Song s1 = new TestSong("title", "artist", "album", "genre");
            s1.Lyrics = "the new lyrics";

            Song s2 = new TestSong("title2", "artist2", "album", "genre");
            s2.Lyrics = "something else2";

            Assert.IsNull(cache.GetLyrics(s1));
            Assert.IsNull(cache.GetLyrics(s2));

            cache.PutLyrics(s1);
            cache.PutLyrics(s2);

            Assert.AreEqual(s1.Lyrics, cache.GetLyrics(s1));
            Assert.AreEqual(s2.Lyrics, cache.GetLyrics(s2));
        }

        [Test]
        public void TestUpdating()
        {
            LyricsCache cache = new LyricsCache();
            Song s1 = new TestSong("title", "artist", "album", "genre");

            Assert.IsFalse(cache.UpdateLyrics(s1));
            s1.Lyrics = "the new lyrics";
            cache.PutLyrics(s1);

            Song sameTitleAndArtist = new TestSong("title", "artist", "album", "genre");
            sameTitleAndArtist.Lyrics = "something else";

            Assert.IsTrue(cache.UpdateLyrics(sameTitleAndArtist));
            Assert.AreEqual(s1.Lyrics, sameTitleAndArtist.Lyrics);
        }

        [Test]
        public void TestSaveLoad()
        {
            Song s1 = new TestSong("title", "artist", "album", "genre");
            Song s2 = new TestSong("title2", "artist2", "album", "genre");
            Song s3 = new TestSong("title3", "artist3", "album", "genre");

            s1.Lyrics = "the lyrics";
            s2.Lyrics = "something else2";
            s3.Lyrics = "another something else3";

            LyricsCache cache = new LyricsCache();

            cache.PutLyrics(s1);
            cache.PutLyrics(s2);
            cache.PutLyrics(s3);
            cache.SaveLyricsCache(@"c:\temp\tempCache.bin");

            LyricsCache cache2 = new LyricsCache();
            cache2.LoadLyricsCache(@"c:\temp\tempCache.bin");

            Song t1 = new TestSong("title", "artist", "album", "genre");
            Song t2 = new TestSong("title2", "artist2", "album", "genre");
            Song t3 = new TestSong("title3", "artist3", "album", "genre");
            Song t4 = new TestSong("title4", "artist4", "album", "genre");

            Assert.IsTrue(cache.UpdateLyrics(t1));
            Assert.IsTrue(cache.UpdateLyrics(t2));
            Assert.IsTrue(cache.UpdateLyrics(t3));
            Assert.IsFalse(cache.UpdateLyrics(t4));

            Assert.AreEqual(s1.Lyrics, t1.Lyrics);
            Assert.AreEqual(s2.Lyrics, t2.Lyrics);
            Assert.AreEqual(s3.Lyrics, t3.Lyrics);
        }
    }
}
