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

using iTunesLib;

namespace LyricsFetcher.Tests
{
	[TestFixture]
	public class TestITunesSong
	{
		[Test]
		public void TestCreateFromTrack()
		{
			IITTrack track = ITunes.Instance.AllTracks[2];
			Assert.IsNotNull(track);

            Song s = new ITunesSong(track);
			
			Assert.AreEqual(s.Title, track.Name);
			Assert.AreEqual(s.Artist, track.Artist);
			Assert.AreEqual(s.Album, track.Album);
			Assert.AreEqual(s.Genre, track.Genre);
		}

        [Test]
        public void TestCommit()
        {
            IITTrack track = ITunes.Instance.AllTracks[2];
            Assert.IsNotNull(track);

            Song original = new ITunesSong(track);
            string originalToString = original.ToString();

            Song s = new ITunesSong(track);

            s.Title = "Song title";
            s.Album = "Song album";
            s.Artist = "Song artist";
            s.Genre = "Song genre";

            Assert.AreNotEqual(s.Title, track.Name);
            Assert.AreNotEqual(s.Artist, track.Artist);
            Assert.AreNotEqual(s.Album, track.Album);
            Assert.AreNotEqual(s.Genre, track.Genre);

            s.Commit();

            Assert.AreEqual(s.Title, track.Name);
            Assert.AreEqual(s.Artist, track.Artist);
            Assert.AreEqual(s.Album, track.Album);
            Assert.AreEqual(s.Genre, track.Genre);

            original.Commit();
            Assert.AreEqual(originalToString, (new ITunesSong(track)).ToString());
        }
		
		[Test]
		public void TestGenresToIgnore()
		{
			Song.GenresToIgnore = new List<String>(new string[] {"Classical", "Sermon", "Podcast"});
			Assert.IsTrue(Song.IsGenreIgnored("Sermon"));
			Assert.IsFalse(Song.IsGenreIgnored("Jazz"));
		}
		
		[Test]
		public void TestLyricsStatusGenreToIgnore()
		{
            Song s1 = new ITunesSong(ITunes.Instance.AllTracks[1]);
            Song s2 = new ITunesSong(ITunes.Instance.AllTracks[2]);

			Song.GenresToIgnore = new List<String>(new string[] {"Classical", "Sermon", "Podcast"});

            s1.Lyrics = "";
            s2.Lyrics = "";

            s1.Genre = "Classical";
			s2.Genre = "Podcast";
			
			Assert.AreEqual(LyricsStatus.GenreIgnored, s1.LyricsStatus);
			Assert.AreEqual(LyricsStatus.GenreIgnored, s2.LyricsStatus);
			
			// Having lyrics takes precedence
			s1.Lyrics = "some lyrics";
			Assert.AreEqual(LyricsStatus.Success, s1.LyricsStatus);
		}
		
		[Test]
		public void TestLyricsStatusSuccessOrFailure()
		{
            Song s = new ITunesSong(ITunes.Instance.AllTracks[2]);
			
			s.Lyrics = "";
			Assert.AreEqual(LyricsStatus.Untried, s.LyricsStatus);
			
			s.Lyrics = "some lyrics";
			Assert.AreEqual(LyricsStatus.Success, s.LyricsStatus);
			
			s.Lyrics = "Failed!";
			Assert.AreEqual(LyricsStatus.Failed, s.LyricsStatus);
		}
		
		[Test]
		public void TestLyricsStatusDataMissing()
		{
			Song.GenresToIgnore = new List<String>(new string[] {"Classical", "Sermon", "Podcast"});
			
            Song s1 = new ITunesSong(ITunes.Instance.AllTracks[2]);
            Song s2 = new ITunesSong(ITunes.Instance.AllTracks[2]);
            Song s3 = new ITunesSong(ITunes.Instance.AllTracks[2]);
            Song s4 = new ITunesSong(ITunes.Instance.AllTracks[2]);

            s1.Lyrics = "";
            s2.Lyrics = "";
            s3.Lyrics = "";
            s4.Lyrics = "";

            s1.Artist = "";
            s2.Title = "";
            s3.Title = "Track 9";
            s4.Title = "Faixa 9";

			Assert.AreEqual(LyricsStatus.DataMissing, s1.LyricsStatus);
			Assert.AreEqual(LyricsStatus.DataMissing, s2.LyricsStatus);
			Assert.AreEqual(LyricsStatus.DataMissing, s3.LyricsStatus);
			Assert.AreEqual(LyricsStatus.DataMissing, s4.LyricsStatus);
			
			// Having lyrics takes precedence
			s1.Lyrics = "some lyrics";
			Assert.AreEqual(LyricsStatus.Success, s1.LyricsStatus);
		}
	}
}
