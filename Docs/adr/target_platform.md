# RFC - Target Platform (Draft)


## CS.019 - PharmacyGo_v3


| Field | Value |
| --- | --- |
| Alex Loomis | <loomisa@oregonstate.edu> |
| Date | 2026-10-24 |
| Status | <ins>Proposed</ins> / Accepted / Rejected / Superseded |
| Decision Date | (fill when decided) |
| Deciders | (who agreed to the outcome) |
| Superseded By | (link, if applicable) |



**1 - Problem and Context**

Capstone project 019 is PharmacyGo_v3, in which my team and I iterate on an existing game, “PharmacyGo!”. The game is a study tool used by OSU Pharmacy students, developed in Unity 6.0 and based on classic top-down “Pokemon” games. As the “v3” in our name implies, this is the third year that this project has been worked on by students.

A full game has already been built by the v2 team. Our goals as the v3 team are to refine the product. This includes both type 2 changes - such as graphical updates, new minigames, and quality of life changes - as well as type 1 changes that will be difficult to backtrack.

One example of a hard-to-reverse decision we will face is our target platform and delivery method of the game. One of the high-level requirements of our project that we must prioritize is that the game must work across multiple platforms. Our predecessors built the game in Unity and hosted it on Itch.io for players to access it at any time. We can either choose to continue using Itch, or switch gears and use an alternative platform.

It is important that this decision is settled now in fall term, because whether we continue to use Itch or move to a different platform will affect the rest of v3’s development, such as data handling pipelines, code architecture, and optimization for performance and visuals.

**2 - Options**

* Option 1 - Continue using Itch and make refinements
	* lowest cost, can use existing architecture
* Option 2 - Host the game on a stand-alone website
	* medium cost, can host for free using ENGR servers
* Option 3 - Adapt the game to be distributed by Steam
	* highest cost, many architecture changes

**3 - Analysis**

Option 1 
* Best option for keeping low costs, maintains existing pipeline, CI and patch support (Chang)
* Worst option for scalability and performance, limited customization
* Browser memory limitations (Trivellato)
Option 2 
* Moderate costs, requires new pipeline and overhead
* Best option for customization, user data analytics
* Can host for free using ENGR servers (OSU COE IT)
* Networking restrictions (Unity Documentation)
Option 3
* Highest cost option, requires the most refactoring and reworking of existing code, architecture, pipelines, data handling
* Best option for accessibility, performance, scalability and controller support
* Worst option for CI and patch support, requires pharmacy students to download and install the game, $100 steam publication fee (Steamworks Direct Fee Doc)
* Potential headaches and costs with MacOS support (Steamworks Platform Doc)

**4 - Recommendation and Verification Plan**

My recommendation is for our team to go with option 1 - to continue using Itch. Migration to an alternative platform represents an expensive challenge, both financially and in terms of the time we would spend rebuilding architecture and existing pipelines. Our project partner already explained to us that v2’s team spent a lot of their time re-working platform issues before eventually settling on Itch. If my team and I had more experience and more time, moving to Steam could have been the right decision. However, building off of v2’s experience, I believe that continuing to host the game on Itch is our best option.

**5 - Constraints**

Our primary constraint is the requirement of the game being cross-platform, which my proposed solution meets. Additionally, our project partner has requested live access to a database GUI from which he can edit the pharmaceutical study questions and answers at any time. Staying with Itch also meets this requirement, as team v2 already created a database API that we can build on in v3.

**6 - Delegation and Validation**

I made my recommendation without the assistance of AI tools. We manually test the game during development cycles to get a holistic view of the changes we’ve made, and to identify any bugs we need to address. Should we continue to use Itch, performing manual tests of our game hosted on Itch platform will be our key to checking whether this decision was successful.

Before making this decision, we will discuss with our project partner, explaining to him the pros and cons of each option. We will also read over our peer reviews and take their feedback into account before cementing our choice.

**Sources**

Chang, A. (2026, February 25). Itch.io vs. steam early access for indie game developers. Spiral Up Games. https://www.spiralupgames.com/post/itch-io-vs-steam-early-access-for-indie-game-developers

COE IT_Support. (2026, October 14). Your PUBLIC_HTML folder. Information Technology and Computing Support. https://it.engineering.oregonstate.edu/web/public-html

Steamworks Documentation. (2008a). Steamworks Documentation > Getting Started > Steam Direct Fee. https://partner.steamgames.com/doc/gettingstarted/appfee

Steamworks Documentation. (2008b). Steamworks Documentation > Store Presence > Applications > Platforms. Steamworks. https://partner.steamgames.com/doc/store/application/platforms

Trivellato, M. (2016, September 20). Understanding memory in Unity Webgl. Understanding memory in Unity WebGL. https://unity.com/blog/engine-platform/understanding-memory-in-unity-webgl

Unity Documentation. (2016). WebGL networking. https://docs.unity3d.com/540/Documentation/Manual/webgl-networking.html

