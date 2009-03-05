/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 8/01/2008 11:27 PM
 * 
 * CHANGE LOG:
 * when who what
 * 8/01/2008 JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace LyricsFetcher.Tests
{
	[TestFixture]
    public class TestITunesLibrary
	{
		const int MAX_SONGS_TO_FETCH = 100;

        public virtual SongLibrary GetLibrary()
        {
            return new ITunesLibrary();
        }

		[Test]
		public void TestInitialState()
		{
			SongLibrary db = this.GetLibrary();
			Assert.IsFalse(db.HasSongs);
			Assert.AreEqual(0, db.MaxSongsToFetch);
		}
		
		[Test]
		public void TestBasicFetchAll()
		{
			SongLibrary db = this.GetLibrary();
			db.MaxSongsToFetch = MAX_SONGS_TO_FETCH;
			db.LoadSongs();
            db.WaitLoad();

			Assert.IsTrue(db.HasSongs);
			Assert.AreEqual(db.Songs.Count, db.MaxSongsToFetch);
		}
		
		[Test]
		public void TestFetchEvents()
		{
			SongLibrary db = this.GetLibrary();
            db.ProgessEvent += new EventHandler<ProgressEventArgs>(db_LoadingProgessEvent1);
            db.DoneEvent += new EventHandler<ProgressEventArgs>(db_LoadingDoneEvent1);

            this.doneEventCalled = false;
            for (int i=0; i<=100; i++) 
            	this.progressPercentages[i] = i;
            
            db.MaxSongsToFetch = Math.Max(100, MAX_SONGS_TO_FETCH);
            db.LoadSongs();
            db.WaitLoad();

            Assert.IsTrue(this.doneEventCalled);
            
            // We should have received one progress event for every value of percentage
            Assert.IsEmpty(this.progressPercentages.Keys);
		}
        bool doneEventCalled;
        Dictionary<int, int> progressPercentages = new Dictionary<int, int>();
        
        void db_LoadingDoneEvent1(object sender, ProgressEventArgs args)
        {
            this.doneEventCalled = true;
        }

        void db_LoadingProgessEvent1(object sender, ProgressEventArgs args)
        {
            this.progressPercentages.Remove(args.Percentage);
        }
		
		[Test]
		public void TestFetchCancelling()
		{
			SongLibrary db = this.GetLibrary();
            db.DoneEvent += new EventHandler<ProgressEventArgs>(db_LoadingDoneEvent2);
            this.doneEventCalled = false;

			db.MaxSongsToFetch = MAX_SONGS_TO_FETCH;
			db.LoadSongs();
			db.CancelLoad();
			
            Assert.IsTrue(doneEventCalled);
		}
		
		[Test]
		public void TestFetchCancelling2()
		{
			// This test isn't very reliable since it relies on timing. 
			// It is supposed to test that cancelling a fetch during a progress event does in fact cancel the fetch.
			// But progress events are triggered asynchronously, so the fetching may actually finish successfully
			// before the progress events are processed. 
			// We can "hide" this by making sure we fetch "enough" songs so that the progress events have a chance 
			// to cancel the fetching. On my current machine, 200 is usually enough.
			// JPP 2008/01/14
			
			SongLibrary db = this.GetLibrary();
            db.ProgessEvent += new EventHandler<ProgressEventArgs>(db_LoadingProgessEvent2);
            db.DoneEvent += new EventHandler<ProgressEventArgs>(db_LoadingDoneEvent2);

            this.doneEventCalled = false;

            db.MaxSongsToFetch = Math.Max(200, MAX_SONGS_TO_FETCH);
			db.LoadSongs();
            db.WaitLoad();

            Assert.IsTrue(doneEventCalled);
		}

        void db_LoadingDoneEvent2(object sender, ProgressEventArgs args)
        {
            this.doneEventCalled = true;
        }

        void db_LoadingProgessEvent2(object sender, ProgressEventArgs args)
        {
            args.IsCancelled = (args.Percentage >= 10);
        }
		
		[Test]
		public void TestFetchSongsWithoutLyrics()
		{
			SongLibrary db = this.GetLibrary();
			db.MaxSongsToFetch = MAX_SONGS_TO_FETCH;
            db.LoadSongs();
            db.WaitLoad();

			Assert.IsTrue(db.SongsWithoutLyrics.TrueForAll(
				delegate(Song s) {
					return (String.IsNullOrEmpty(s.Lyrics) || 
                        s.Lyrics.StartsWith("Failed") ||
                        s.Lyrics.StartsWith("[[LyricsFetcher failed"));
			    }
			));
		}
		
		[Test]
		public void TestFetchUntriedSongs()
		{
			SongLibrary db = this.GetLibrary();
			db.MaxSongsToFetch = MAX_SONGS_TO_FETCH;
            db.LoadSongs();
            db.WaitLoad();

			Assert.IsTrue(db.UntriedSongs.TrueForAll(
				delegate(Song s) {
					return (String.IsNullOrEmpty(s.Lyrics));
			    }
			));
		}
	}

    [TestFixture]
    public class TestWmpLibrary : TestITunesLibrary
    {
        public override SongLibrary GetLibrary()
        {
            return new WmpLibrary();
        }
    }
}
