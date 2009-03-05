/*
 * [File purpose]
 * Author: Phillip Piper
 * Date: 10/01/2008 11:04 PM
 * 
 * CHANGE LOG:
 * when who what
 * 10/01/2008 JPP  Initial Version
 */

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace LyricsFetcher.Tests
{
    [TestFixture]
    public class TestLyricsSource
    {
        [Test]
        public void TestLyricWikiStrategy()
        {
            Dictionary<Song, string> songs = this.CreateWikiSongs();

            LyricsSourceLyricWiki strategy = new LyricsSourceLyricWiki();

            foreach (Song s in songs.Keys) {
                AssertEqualIgnoringLineEndings(songs[s], strategy.GetLyrics(s));
            }
        }

        public void AssertEqualIgnoringLineEndings(string s1, string s2)
        {
            Assert.AreEqual(s1.Replace("\r", "").Trim(), s1.Replace("\r", "").Trim());
        }

        [Test]
        public void TestLyricLyrdbStrategy()
        {
            Dictionary<Song, string> songs = this.CreateLyrdbSongs();

            LyricsSourceLyrdb strategy = new LyricsSourceLyrdb();

            foreach (Song s in songs.Keys) {
                AssertEqualIgnoringLineEndings(songs[s], strategy.GetLyrics(s));
            }
        }

        /*
        [Test]
        public void TestScrapperStrategyUrlGeneration()
        {
            Dictionary<Song, string> songs = new Dictionary<Song, string>();

            songs[new TestSong("Pon de Replay", "Rihanna", "Music of the Sun", "")] = "x.com/lyrics/R/Rihanna/Pon-de-Replay.html";
            songs[new TestSong("Dead, and Dumb!", "SomeGuy", "The Album", "")] = "x.com/lyrics/S/SomeGuy/Dead-and-Dumb.html";
            songs[new TestSong("Numb (should remove brackets)", "U2", "Discotheque", "")] = "x.com/lyrics/U/U2/Numb.html";
            songs[new TestSong("this will be changed", "XXX", "Discotheque", "")] = "x.com/lyrics/X/XXX/this-to-this.html";

            UrlGenerator generator = new UrlGenerator();
            generator.UrlPattern = "x.com/lyrics/{3}/{1}/{0}.html";
            generator.CharactersToRemove = ",.!";
            generator.CharactersToReplace = "' ";
            generator.ReplaceCharactersWith = "-";
            generator.AddReplacement(@"\(.*\)", "", true);
            generator.AddReplacement("will be changed", "to this", false);

            foreach (Song s in songs.Keys) {
                Assert.AreEqual(songs[s], generator.Generate(s));
            }
        }

        [Test]
        public void TestHtmlScrapperSerializationSingle()
        {
            LyricsSourceHtmlScrapper lfs = new LyricsSourceHtmlScrapper("TestScrapper");
            lfs.generator.UrlPattern = "http://www.testscrapper.com/{0}/{1}";
            lfs.generator.AddReplacement("this1", "that1", false);
            lfs.generator.AddReplacement("this2", "that2", false);
            lfs.generator.CharactersToRemove = "<>'\"";
            lfs.generator.CharactersToReplace = " ?";
            lfs.generator.ReplaceCharactersWith = "-";
            lfs.scrapper.ChopPattern = "MyChopPattern";
            lfs.scrapper.LyricsPattern = "LyricsPattern";
            lfs.scrapper.IsChopPatternRegex = true;

            Type[] extraTypes = new Type[1];
            extraTypes[0] = typeof(UrlGenerator.Replacement);

            StreamWriter sw = new StreamWriter(@"c:\temp\t.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(LyricsSourceHtmlScrapper), extraTypes);
            serializer.Serialize(sw, lfs);
            sw.Close();

            StreamReader sr = new StreamReader(@"c:\temp\t.xml");
            XmlSerializer deserializer = new XmlSerializer(typeof(LyricsSourceHtmlScrapper), extraTypes);
            LyricsSourceHtmlScrapper lfs2 = deserializer.Deserialize(sr) as LyricsSourceHtmlScrapper;
            sr.Close();

            this.AssertStrategiesAreEqual(lfs, lfs2);
        }

        public void AssertStrategiesAreEqual(LyricsSourceHtmlScrapper lfs1, LyricsSourceHtmlScrapper lfs2)
        {
            Assert.IsNotNull(lfs1);
            Assert.IsNotNull(lfs2);

            Assert.AreEqual(lfs1.Name, lfs2.Name);

            Assert.AreEqual(lfs1.generator.CharactersToRemove, lfs2.generator.CharactersToRemove);
            Assert.AreEqual(lfs1.generator.CharactersToReplace, lfs2.generator.CharactersToReplace);
            Assert.AreEqual(lfs1.generator.ReplaceCharactersWith, lfs2.generator.ReplaceCharactersWith);
            Assert.AreEqual(lfs1.generator.replacements, lfs2.generator.replacements);

            Assert.AreEqual(lfs1.scrapper.LyricsPattern, lfs2.scrapper.LyricsPattern);
            Assert.AreEqual(lfs1.scrapper.ChopPattern, lfs2.scrapper.ChopPattern);
            Assert.AreEqual(lfs1.scrapper.IsChopPatternRegex, lfs2.scrapper.IsChopPatternRegex);
        }

        [Test]
        public void TestHtmlScrapperSerializationArrayList()
        {
            LyricsSourceHtmlScrapper lfs = new LyricsSourceHtmlScrapper();
            lfs.generator.UrlPattern = "http://www.testscrapper.com1/{0}/{1}";
            lfs.generator.AddReplacement("this1", "that1", true);
            lfs.generator.AddReplacement("this2", "that2", false);
            lfs.generator.CharactersToRemove = "<>'\"";
            lfs.generator.CharactersToReplace = " ?";
            lfs.generator.ReplaceCharactersWith = "-";
            lfs.scrapper.ChopPattern = "MyChopPattern";
            lfs.scrapper.LyricsPattern = "LyricsPattern";
            lfs.scrapper.IsChopPatternRegex = true;

            LyricsSourceHtmlScrapper lfs2 = new LyricsSourceHtmlScrapper();
            lfs2.generator.UrlPattern = "http://www.testscrapper.com2/{0}/{1}";
            lfs2.generator.AddReplacement("2this1", "2that1", false);
            lfs2.generator.AddReplacement("2this2", "2that2", true);
            lfs2.generator.CharactersToRemove = "2<>'\"";
            lfs2.generator.CharactersToReplace = "2 ?";
            lfs2.generator.ReplaceCharactersWith = "2-";
            lfs2.scrapper.ChopPattern = "MyChopPattern2";
            lfs2.scrapper.LyricsPattern = "LyricsPattern2";
            lfs2.scrapper.IsChopPatternRegex = false;

            ArrayList list = new ArrayList();
            list.Add(lfs);
            list.Add(lfs2);

            Type[] extraTypes = new Type[2];
            extraTypes[0] = typeof(LyricsSourceHtmlScrapper);
            extraTypes[1] = typeof(UrlGenerator.Replacement);

            StreamWriter sw = new StreamWriter(@"c:\temp\t.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayList), extraTypes);
            serializer.Serialize(sw, list);
            sw.Close();

            StreamReader sr = new StreamReader(@"c:\temp\t.xml");
            XmlSerializer deserializer = new XmlSerializer(typeof(ArrayList), extraTypes);
            ArrayList list2 = deserializer.Deserialize(sr) as ArrayList;
            sr.Close();

            Assert.AreEqual(list.Count, list2.Count);
            for (int i = 0; i < list.Count; i++)
                this.AssertStrategiesAreEqual(list[i] as LyricsSourceHtmlScrapper, list2[i] as LyricsSourceHtmlScrapper);
        }
        */
        public Dictionary<Song, string> CreateWikiSongs()
        {
            Dictionary<Song, string> songs = new Dictionary<Song, string>();

            songs[new TestSong("Pon de Replay wrong title", "Rihanna", "Music of the Sun", "Hip Hop")] = "";
            songs[new TestSong("Pon de Replay", "Rihanna wrong artist", "Music of the Sun", "Hip Hop")] = "";
            songs[new TestSong("Pon de Replay", "Rihanna", "Music of the Sun", "Hip Hop")] = @"Come mister DJ song pon de replay
Come mister DJ won't you turn the music up
All the gal pon the dance floor wantin' some more what
Come mister DJ won't you turn the music up [REPEAT]

It goes one by one even two by two
Everybody on the floor let me show you how we do
Lets go dip it low then you bring it up slow
Wind it up one time wind it back once more

Run, run, run, run
Everybody move run
Let me see you move and
Rock it 'til the groove's done
Shake it 'til the moon becomes the sun
Everybody in the club give me a run
If you ready to move say it
One time for your mind say it
Well I'm ready for ya
Come let me show ya
You want to groove I'm 'a show you how to move
Come come
Come mister DJ song pon de replay
Come mister DJ won't you turn the music up
All the gal pon the dance floor wantin' some more what
Come mister DJ won't you turn the music up [REPEAT]

Hey mister
Please mister DJ
Tell me if you hear me
Turn the music up [REPEAT]

It goes one by one even two by two
Everybody in the club gon' be rockin' when I'm through
Let the bass from the speakers run through ya sneakers
Move both ya feet and run to the beat

Run, run, run, run
Everybody move run
Let me see you move and
Rock it 'til the grooves done
Shake it 'til the moon becomes the sun
Everybody in the club give me a run
If you ready to move say it
One time for your mind say it
Well I'm ready for ya
Come let me show ya
You want to groove I'm 'a show you how to move
Come come
Come mister DJ song pon de replay
Come mister DJ won't you turn the music up
All the gal pon the dance floor wantin' some more what
Come mister DJ won't you turn the music up [REPEAT]
Hey mister
Please mister DJ
Tell me if you hear me
Turn the music up [REPEAT]

Ok, everybody get down if you feel me
Come and put your hands up to the ceiling";

            songs[new TestSong("Hips Don't Lie", "Shakira", "Oral Fixation (Vol. 2)", "Hip Hop")] = @"(feat. Wyclef Jean)

Ladies up in here tonight
No fighting, no fighting
We got the refugees up in here
No fighting, no fighting

Shakira, Shakira

I never really knew that she could dance like this
She makes a man wants to speak Spanish
Como se llama,si, bonita,si, mi casa
Shakira, Shakira

Oh baby when you talk like that
You make a woman go mad
So be wise and keep on
Reading the signs of my body

And I'm on tonight 
You know my hips don't lie
And I'm starting to feel it's right
All the attraction, the tension
Don't you see baby, this is perfection

Hey Girl, I can see your body moving 
And it's driving me crazy
And I didn't have the slightest idea
Until I saw you dancing

And when you walk up on the dance floor
Nobody cannot ignore the way you move your body, girl
And everything so unexpected - the way you right and left it
So you can keep on taking it

I never really knew that she could dance like this
She makes a man want to speak Spanish
Como se llama, bonita, mi casa,
Shakira, Shakira

Oh baby when you talk like that
You make a woman go mad
So be wise and keep on
Reading the signs of my body

And I'm on tonight
You know my hips don't lie
And I am starting to feel you boy
Come on lets go, real slow
Don't you see baby asi es perfecto

Oh I know I am on tonight my hips don't lie
And I am starting to feel it's right
All the attraction, the tension
Don't you see baby, this is perfection
Shakira, Shakira

Oh boy, I can see your body moving
Half animal, half man
I don't, don't really know what I'm doing
But you seem to have a plan 
My will and self restraint 
Have come to fail now, fail now
See, I am doing what I can, but I can't so you know
That's a bit too hard to explain

Baila en la calle de noche
Baila en la calle de día

Baila en la calle de noche
Baila en la calle de día

I never really knew that she could dance like this
She makes a man want to speak Spanish
Como se llama, bonita, mi casa,
Shakira, Shakira

Oh baby when you talk like that
You know you got me hypnotized
So be wise and keep on
Reading the signs of my body

Senorita, feel the conga, let me see you move like you come from Colombia

Mira en Barranquilla se baila así, say it!
Mira en Barranquilla se baila así

Yeah
She's so sexy every man's fantasy a refugee like me back with the Fugees from a 3rd world country
I go back like when 'pac carried crates for Humpty Humpty
I need a whole club dizzy
Why the CIA wanna watch us?
Colombians and Haitians
I ain't guilty, it's a musical transaction 
No more do we snatch ropes 
Refugees run the seas 'cause we own our own boats 

I'm on tonight, my hips don't lie
And I'm starting to feel you boy
Come on let's go, real slow
Baby, like this is perfecto

Oh, you know I am on tonight and my hips don't lie
And I am starting to feel it's right
The attraction, the tension
Baby, like this is perfection

No fighting
No fighting";

            songs[new TestSong("That don't impress me much", "Shania Twain", "Come on over", "Modern Country")] = @"aha-yeah

I've known a few guys who thought they were pretty smart
But you've got being right down to an art
You think you're a genius, you drive me up the wall
You're a regular original, a ""know it all""

Oh wooh, you think you're special
Oh wooh, you think you're something else

Ok, so you're a rocket scientist...

That don't impress me much!
So you got the brains, but have you got the touch?
(Now) Don't get me wrong, yeah I think you're alright
But that won't keep me warm in the middle of the night
That don't impress me much!

aha-yeah

I never knew a guy who carried a mirror in his pocket
And a comb up his sleeve, just in case
And all that extra hold gel in your hair ought'a lock it
'Cause Heaven prevent, it should fall outta place

Ohwooh, you think you're special
Ohwooh, you think you're something else

Ok, so you're Brad Pitt...


That don't impress me much!
So you got the looks but have you got the touch?
(Now) Don't get me wrong, yeah I think you're alright
But that won't keep me warm in the middle of the night
That don't impress me much!

You're one of those guys who likes to shine's machine
You make me take off my shoes before you let me get in
I can't believe you kiss your car good night
C'mon baby tell me, you must be joking, right??

Ohwooh, you think you're something special
Ohwooh, you think you're something else

Ok, so you got a car...

That don't impress me much!
So you got the moves, but have you got the touch?
Don't get me wrong, yeah I think you're alright
But that won't keep me warm in the middle of the night
[..]
But that won't keep me warm on the long, cold lonely night
That don't impress me much!

Ok, so what do you think... you're Elvis or something...
That don't impress me much!
[repeat]";

            return songs;
        }

        public Dictionary<Song, string> CreateLyrdbSongs()
        {
            Dictionary<Song, string> songs = new Dictionary<Song, string>();

            songs[new TestSong("Pon de Replay wrong title", "Rihanna", "Music of the Sun", "Hip Hop")] = "";
            songs[new TestSong("Pon de Replay", "Rihanna wrong artist", "Music of the Sun", "Hip Hop")] = "";
            songs[new TestSong("Pon de Replay", "Rihanna", "Music of the Sun", "Hip Hop")] = @"<i>[Hook x2:]</i>
Come Mr. DJ song pon de replay 
Come Mr. DJ won't you turn the music up 
All the gyal pon the dancefloor wantin some more what 
Come Mr. DJ won't you turn the music up 

<i>[Verse:]</i>
it goes 1 by 1 even 2 by 2 
everybody on the floor let me show you how we do 
lets go dip it low then you bring it up slow 
wind it up 1 time wind it back once more 

<i>[Pre-Hook:]</i>
Run, Run, Run, Run 
Everybody move run 
Lemme see you move and 
Rock it til the grooves done 
Shake it til the moon becomes the sun (Sun) 
Everybody in the club give me a run (Run) 
If you ready to move say it (Yeah Yeah) 
One time for your mind say it (Yeah Yeah) 
Well i'm ready for ya 
Come let me show ya 
You want to groove im'a show you how to move 
Come come 

<i>[Hook x2:]</i>
Come Mr. DJ song pon de replay 
Come Mr. DJ won't you turn the music up 
All the gyal pon the dancefloor wantin some more what 
Come Mr. DJ won't you turn the music up 

<i>[B-Sec x2:]</i>
Hey Mr. 
Please Mr. DJ
Tell me if you hear me 
Turn the music up 

<i>[Verse 2:]</i>
It goes 1 by 1 even 2 by 2 
Everybody in the club gon be rockin when i'm through 
Let the bass from the speakers run through ya sneakers 
Move both ya feet and run to the beat 

<i>[Pre-Hook:]</i>
Run, Run, Run, Run 
Everybody move run 
Lemme see you move and 
Rock it til the grooves done 
Shake it til the moon becomes the sun (Sun) 
Everybody in the club give me a run (Run) 
If you ready to move say it (Yeah Yeah) 
One time for your mind say it (Yeah Yeah) 
Well i'm ready for ya 
Come let me show ya 
You want to groove im'a show you how to move 
Come come 

<i>[Hook x2:]</i>
Come Mr. DJ song pon de replay 
Come Mr. DJ won't you turn the music up 
All the gyal pon the dancefloor wantin some more what 
Come Mr. DJ won't you turn the music up 

<i>[B-Sec x2:]</i>
Hey Mr. 
Please Mr. DJ
Tell me if you hear me 
Turn the music up

<i>[x4]</i>
Okay everybody get down if you feel me
Put your hands up to the ceiling

<i>[Hook x2:]</i>
Come Mr. DJ song pon de replay 
Come Mr. DJ won't you turn the music up 
All the gyal pon the dancefloor wantin some more what 
Come Mr. DJ won't you turn the music up


<i>[Thanks to norcalshorty@Hotmail.com for these lyrics]</i>
<i>[Thanks to jamacianqt07@blackplanet.com, hotty2824_78@hotmail.com for correcting these lyrics]</i>";

            // Apostrophes in the title cause Lyrdb to choke
            songs[new TestSong("Hips Don't Lie", "Shakira", "Oral Fixation (Vol. 2)", "Hip Hop")] = "";
            songs[new TestSong("Beautiful Liar", "Shakira", "Oral Fixation (Vol. 2)", "Hip Hop")] = @"Ay
Ay
Ay, Nobody likes being played

Oh Beyoncé, Beyoncé
Oh Shakira, Shakira

He said, I'm worth it, his one desire
I Know things about him that you wouldn’t want to read about
He kissed me, his one and only
Yes Beautiful Liar
Tell me how you tolerate the things you just found out about
You never know
Why are we the ones who suffer
Have to let go
He won't be the one to cry

(Chorus)
Ay Let’s not kill the Karma
Ay Let’s not start a fight
Ay It’s not worth the drama
For a Beautiful Liar
Oh can't we laugh about it
Oh it’s not worth our time
Oh we can live without him
Just a Beautiful Liar

I trusted him
But when I followed you
I saw you together
I didn’t know about you then till I saw you with him when
I walked in on your love scene
Slow dancing
You stole everything how can you say I did you wrong

We'll never know
When the pain and heartbreak's over
I have to let go
The innocence is gone

(Chorus)
Ay Let’s not kill the Karma
Ay Let’s not start a fight
Ay It’s not worth the drama
For a Beautiful Liar
Oh can't we laugh about it
Oh it’s not worth our time
Oh we can live without him
Just a Beautiful Liar

Tell me how to forgive you
When its me who’s the ashamed
And I wish I could free you
Of the hurt and the pain
But the answer is simple
He's the one to blame

Ay Beyoncé, Beyoncé
Ay Shakira, Shakira
Oh Beyoncé, Beyoncé
Oh Shakira, Shakira

(Chorus)
Ay Let’s not kill the Karma
Ay Let’s not start a fight
Ay It’s not worth the drama
For a Beautiful Liar
Oh can't we laugh about it
Oh it’s not worth our time
Oh we can live without him
Just a Beautiful Liar";

            return songs;
        }
    }
}
