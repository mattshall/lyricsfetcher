Functionality
-------------

LyricsFetcher has two major functional areas:

Song management
^^^^^^^^^^^^^^^

This functional area covers these capablities:
* Choosing which library to use
* Read that library into objects
* Writing changes back into the library

We want to be able to access more than one library. Ideally, we would like the program
to be able to access all music libraries. But there isn't a common API for accessing
music libraries. iTunes and Windows Media Player both have published APIs which can
be used to iterate the songs in the library. On the Windows platform, these two
programs cover the vast majority of users (I know some people are vocal supporters
of Foobar).

Finding lyrics
^^^^^^^^^^^^^^

Once we have some songs, how will we find lyrics for them? 

There are two basic strategies we can use:
* database lookups
* html scrapping

Database lookups require a service that provides a programmable interface. The only three lyrics services
(that I am currently aware of) that provide such an interface are LyricsWiki, LyrDb, and Firefly. 

The other approach is to find web pages that display the lyrics that you want, and then cull the lyrics from the 
html code. This strategy is far more versatile, since it can be used on many lyrics sites. However, it is 
unreliable since it is dependent on the structure of the html that the site generates. If the site choose a new
layout, or html generator, then the lyrics might not be able to be scraped in the same way.

Design principles
-----------------

Simplicity is the best design. 

If there is an obvious way to do something, there has to be a very good reason to do something in an un-obvious way.

It seems that inside every programmer is a framework architect.
