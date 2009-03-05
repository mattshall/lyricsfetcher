/*
 * Test the LyricsFetchManager
 * 
 * Author: Phillip Piper
 * Date: 2008-02-09 12:27pm
 * 
 * CHANGE LOG:
 * 2008-02-09 JPP  Initial Version
 */

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

using LyricsFetcher;

namespace LyricsFetcher.Tests
{
    [TestFixture]
    public class TestLyricsFetchManager
    {
        Song s1 = new TestSong();
        Song s2 = new TestSong();
        Song s3 = new TestSong();

        private LyricsFetchManager CreateManager() {
            LyricsFetchManager lfm = new LyricsFetchManager();
            lfm.RegisterSource(new AlwaysFailLyricsSource());
            lfm.RegisterSource(new AlwaysSuccessLyricsSource());
            return lfm;
        }

        [Test]
        public void TestQueueAndCancel() {
            LyricsFetchManager lfm = new LyricsFetchManager();

            Assert.AreEqual(lfm.CountWaiting, 0);
            lfm.Queue(s1);
            Assert.AreEqual(lfm.CountWaiting, 1);
            lfm.Queue(s2);
            Assert.AreEqual(lfm.CountWaiting, 2);
            lfm.Cancel(s2);
            Assert.AreEqual(lfm.CountWaiting, 1);
            lfm.Cancel(s1);
            Assert.AreEqual(lfm.CountWaiting, 0);
        }

        [Test]
        public void TestQueueAndCancelAll() {
            LyricsFetchManager lfm = new LyricsFetchManager();
            Assert.AreEqual(lfm.CountWaiting, 0);
            lfm.Queue(s1);
            lfm.Queue(s2);
            lfm.CancelAll();
            Assert.AreEqual(lfm.CountWaiting, 0);
        }
        [Test]
        public void TestGetStatus() {
            LyricsFetchManager lfm = new LyricsFetchManager();

            Assert.AreEqual(FetchStatus.NotFound, lfm.GetStatus(s1));
            Assert.AreEqual(FetchStatus.NotFound, lfm.GetStatus(s2));

            lfm.Queue(s1);
            lfm.Queue(s2);
            Assert.AreEqual(FetchStatus.Waiting, lfm.GetStatus(s1));
            Assert.AreEqual(FetchStatus.Waiting, lfm.GetStatus(s2));

            lfm.CancelAll();
            Assert.AreEqual(FetchStatus.NotFound, lfm.GetStatus(s1));
            Assert.AreEqual(FetchStatus.NotFound, lfm.GetStatus(s2));
        }

        [Test]
        public void TestGetStatus2() {
            LyricsFetchManager lfm = new LyricsFetchManager();
            lfm.RegisterSource(new AlwaysFailLyricsSource());
            lfm.RegisterSource(new AlwaysSuccessLyricsSource());

            Assert.AreEqual(FetchStatus.NotFound, lfm.GetStatus(s1));
            Assert.AreEqual(FetchStatus.NotFound, lfm.GetStatus(s2));
            Assert.AreEqual(FetchStatus.NotFound, lfm.GetStatus(s3));

            lfm.Queue(s1);
            lfm.Queue(s2);
            lfm.Queue(s3);
            Assert.AreEqual(FetchStatus.Waiting, lfm.GetStatus(s1));
            Assert.AreEqual(FetchStatus.Waiting, lfm.GetStatus(s2));
            Assert.AreEqual(FetchStatus.Waiting, lfm.GetStatus(s3));

            lfm.Start();
            lfm.WaitUntilFinished();

            Assert.AreEqual(FetchStatus.NotFound, lfm.GetStatus(s1));
            Assert.AreEqual(FetchStatus.NotFound, lfm.GetStatus(s2));
            Assert.AreEqual(FetchStatus.NotFound, lfm.GetStatus(s3));
        }

        [Test]
        public void TestStatusEvents() {
            this.lfm = new LyricsFetchManager();
            this.lfm.RegisterSource(new AlwaysFailLyricsSource());
            this.lfm.RegisterSource(new AlwaysSuccessLyricsSource());

            this.lfm.StatusEvent += new EventHandler<FetchStatusEventArgs>(this.HandleStatusEvent);
            this.statusEventCount = 0;
            this.lastStatus = FetchStatus.Undefined;

            this.lfm.Queue(s1);
            this.lfm.Queue(s2);
            this.lfm.Start();
            this.lfm.WaitUntilFinished();

            // Each song should trigger: Waiting, (Fetching, SourceDone) x 2, Done
            Assert.AreEqual(12, this.statusEventCount); 
            Assert.AreEqual(FetchStatus.Done, this.lastStatus);
        }
        LyricsFetchManager lfm;
        int statusEventCount;
        FetchStatus lastStatus;

        void HandleStatusEvent(object sender, FetchStatusEventArgs args) {
            this.statusEventCount += 1;

            // SourceDone events count as Fetching status
            if (args.Status == FetchStatus.SourceDone)
                Assert.AreEqual(FetchStatus.Fetching, this.lfm.GetStatus(args.Song));
            else
                Assert.AreEqual(args.Status, this.lfm.GetStatus(args.Song));
            this.lastStatus = args.Status;
        }

        [Test]
        public void TestDoneStatusEvents() {
            LyricsFetchManager lfm = new LyricsFetchManager();
            lfm.RegisterSource(new AlwaysFailLyricsSource());
            lfm.RegisterSource(new AlwaysSuccessLyricsSource());
            lfm.StatusEvent += new EventHandler<FetchStatusEventArgs>(this.HandleDoneStatusEvent);
            doneEventCount = 0;

            lfm.Start();
            lfm.Queue(s1);
            lfm.Queue(s2);
            lfm.WaitUntilFinished();

            Assert.AreEqual(2, doneEventCount);
        }
        int doneEventCount;

        void HandleDoneStatusEvent(object sender, FetchStatusEventArgs args) {
            if (args.Status == FetchStatus.Done)
                doneEventCount += 1;
        }

        [Test]
        public void TestCancelledStatusEvents() {
            LyricsFetchManager lfm = new LyricsFetchManager();
            lfm.RegisterSource(new AlwaysFailLyricsSource());
            lfm.RegisterSource(new AlwaysSuccessLyricsSource());
            lfm.StatusEvent += new EventHandler<FetchStatusEventArgs>(this.HandleCancelledStatusEvent);

            cancelledEventCount = 0;

            lfm.Queue(s1);
            lfm.Queue(s2);
            lfm.CancelAll();

            Assert.AreEqual(cancelledEventCount, 2);
        }
        int cancelledEventCount;

        void HandleCancelledStatusEvent(object sender, FetchStatusEventArgs args) {
            if (args.Status == FetchStatus.Cancelled)
                cancelledEventCount += 1;
        }

        [Test]
        public void TestRealLyricsFetch() {
            LyricsFetchManager lfm = new LyricsFetchManager();
            lfm.StatusEvent += new EventHandler<FetchStatusEventArgs>(this.HandleDoneStatusEvent2);
            lfm.RegisterSource(new LyricsSourceLyrdb());
            lfm.RegisterSource(new LyricsSourceLyricWiki());

            List<Song> songs = this.GetSongs();

            foreach (Song s in songs)
                Assert.IsNull(s.Lyrics);

            lfm.Queue(songs);

            lfm.Start();
            lfm.WaitUntilFinished();

            foreach (Song s in songs)
                Assert.AreNotEqual("", s.Lyrics);
        }

        List<Song> GetSongs() {
            List<Song> songs = new List<Song>();
            songs.Add(new TestSong("Pon de Replay", "Rihanna", "Music of the Sun", "Hip Hop"));
            songs.Add(new TestSong("Numb", "U2", "Discotheque", "Pop"));
            songs.Add(new TestSong("Hips don't lie", "Shakira", "Oral Fixation (Vol 2)", "Latina"));
            songs.Add(new TestSong("That don't impress me much", "Shania Twain", "Come on over", "Modern Country"));
            songs.Add(new TestSong("Our lips are sealed", "The Go-Go's", "Beauty and the Beat", "Pop"));
            songs.Add(new TestSong("Rock This Party", "Bob Sinclar", "Unknown", "Dance"));
            return songs;
        }

        void HandleDoneStatusEvent2(object sender, FetchStatusEventArgs args) {
            if (args.Status == FetchStatus.Done) {
                if (args.Lyrics == "")
                    args.Song.Lyrics = "Failed";
                else
                    args.Song.Lyrics = args.Lyrics;
            }
        }

        [Test]
        public void TestAutoUpdateLyrics() {
            LyricsFetchManager lfm = new LyricsFetchManager();
            lfm.AutoUpdateLyrics = true;
            lfm.RegisterSource(new LyricsSourceLyrdb());
            lfm.RegisterSource(new LyricsSourceLyricWiki());

            List<Song> songs = this.GetSongs();

            foreach (Song s in songs)
                Assert.IsNull(s.Lyrics);

            Song song1 = new TestSong("Test Title 1", "Some Artist Name", "Beauty and the Beat", "Pop");
            song1.Lyrics = "Original Lyrics";
            songs.Add(song1);

            lfm.Queue(songs);

            lfm.Start();
            lfm.WaitUntilFinished();

            foreach (Song s in songs)
                Assert.AreNotEqual("", s.Lyrics);

            // This song cannot be found and its lyrics should not be changed
            Assert.AreEqual("Original Lyrics", song1.Lyrics);
        }
    }
}
