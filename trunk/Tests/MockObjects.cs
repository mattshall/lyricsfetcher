/*
 * MockObjects - Dummy implementations useful for testing
 *
 * Author: Phillip Piper
 * Date: 2009-02-07 11:15 AM
 *
 * Change log:
 * 2009-02-071  JPP  Initial version
 */
using System;

namespace LyricsFetcher.Tests
{
    /// <summary>
    /// A do-nothing Song for testing purposes
    /// </summary>
    public class TestSong : Song
    {
        public TestSong() {

        }

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

    public class AlwaysSuccessLyricsSource : ILyricsSource
    {
        public string Name
        {
            get { return "alwaysSuccess"; }
        }

        public string GetLyrics(Song s)
        {
            System.Threading.Thread.Sleep(10);
            return "this lyrics";
        }
    }

    public class AlwaysFailLyricsSource : ILyricsSource
    {
        public string Name
        {
            get { return "alwaysFail"; }
        }

        public string GetLyrics(Song s)
        {
            System.Threading.Thread.Sleep(10);
            return "";
        }
    }
}
