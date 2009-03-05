/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 7/01/2008 9:35 PM
 * 
 * CHANGE LOG:
 * when who what
 * 7/01/2008 JPP  Initial Version
 */

using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace LyricsFetcher.Tests
{
	[TestFixture]
	public class TestITunesHelper
	{
		[Test]
		public void TestITunesExists()
		{
			Assert.IsTrue(ITunes.HasITunes);
		}
		
		[Test]
		public void TestPlayStop()
		{
            ITunes.Instance.Stop();
            Assert.IsFalse(ITunes.Instance.IsAnyTrackPlaying);

            ITunes.Instance.Play();
            Assert.IsTrue(ITunes.Instance.IsAnyTrackPlaying);

            ITunes.Instance.Stop();
		}
		
		[Test]
		public void TestIsPlayingTrack()
		{
            iTunesLib.IITTrack track = ITunes.Instance.AllTracks[2];
			Assert.IsNotNull(track);
			
			track.Play();
            Assert.IsTrue(ITunes.Instance.IsTrackPlaying(track));

            ITunes.Instance.Stop();
            Assert.IsFalse(ITunes.Instance.IsTrackPlaying(track));
		}
	}
}
