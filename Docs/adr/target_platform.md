# RFC - Target_Platform


## CS.019 - PharmacyGo_v3


| Field | Value |
| --- | --- |
| Alex Loomis | <loomisa@oregonstate.edu> |
| Date | 2026-10-24 |
| Status | Proposed / **Accepted** / Rejected / Superseded |
| Decision Date | 2026-11-17 |
| Deciders | Alex, Zach, Shawn, Nathan, BJ |
| Superseded By | n/a |




## **1 - Problem and Context**
Capstone project 019 is PharmacyGo_v3, in which my team and I iterate on an existing game, “PharmacyGo!”. The game is a study tool used by OSU Pharmacy students, developed in Unity 6.0 and based on classic top-down “Pokemon” games. Rather than collecting monsters and battling with them, players answer study questions, fight boss battles, and progressively unlock new areas to explore. As suggested by the “v3” in our team name, we are the third capstone group to be developing this project.

A full game has already been built and shipped by the v2 team. Our high-level goal as the v3 team is to refine the shipped product. This includes both type 2 changes - such as graphical updates, new minigames, code improvements, and quality of life changes - as well as type 1 changes that will be difficult to backtrack.

One example of a type 1, hard-to-reverse decision we must face is our target platform and delivery method of the game. One of our highest priorities is that the game must work across multiple platforms to ensure accessibility for the Pharmacy students that play the game to study. Our predecessors built the game in Unity and exported their builds using WebGL to host it on Itch.io for players to access at any time in their browsers. We can either choose to continue using WebGL builds of the game, or adapt the code base for a superior alternative.

It is important that this decision is settled now in fall term, because whether we continue to use Itch or move to a different platform will affect the rest of v3’s development, including the project’s data handling pipelines, code architecture, and optimizations for performance and visuals.


## **2 - Options**
* **Option 1**: Continue using Itch and make refinements
* lowest cost, can use existing architecture
* **Option 2**: Host the game on a stand-alone website
* medium cost, can host for free using ENGR servers
* **Option 3**: Adapt the game to be distributed by Steam
* highest cost, many architecture changes

## **3 - Analysis**

### **Option 1** 
Continuing to use Itch is the lowest-cost option. Keeping the existing target platform allows our team to use the established development pipeline, saving time and resources that can be devoted towards other improvements. Platforming on Itch is also an effective way to meet the project requirement of cross-platform accessibility, and streamlines the process of pushing new patches of the game after each development cycle (Chang).

However, continuing to use WebGL builds of the game that run in-browser can run into issues. If many students attempt to play the game all at once, there is potential for browser memory limitations to impact gameplay. Additionally, as development continues and more features are added, future builds of the game may find the WebGL medium as a bottleneck on the project's scalability, performance, and customization (Trivellato). 

**This is the best option for streamlined development cycles and player accessibility.**

### **Option 2** 
Creating an executable for the game is a medium-cost option that avoids Option 1’s drawbacks of limited scalability and performance concerns. Unity already natively supports creating builds for Windows, MacOS, and Linux environments, but new installations, packages, and overhead would be required to support the expanded development pipeline. This option also supports greater flexibility in the game’s delivery, as a stand-alone website could be created to host the executable. The OSU ENGR servers facilitate free website development, nullifying what may otherwise be a financial concern (OSU COE IT). Alternatively, the executable could be distributed through Itch.

The primary downside of this option is the additional overhead of supporting new build platforms. Due to the project requirement of cross-platform accessibility, many different types of executables would have to be created for each platform used by pharmacy students, such as Windows, MacOS, Linux, and mobile. This would slow down development time and would make the play-testing process demonstrably slower. Furthermore, in ensuring that these builds interacted properly with our secondary database holding study questions and answers, either new interfaces would have to be built, or entirely new networking functionality would have to be developed, the latter of which would likely come with performance concerns (Unity Documentation).

**This is the best option for enhanced customization without completely rehauling existing architecture.**

### **Option 3**
Adapting the game for a Steam release is a very ambitious option that begets tremendous code refactoring and creation of new development pipelines. However, the benefits are similarly tremendous. Steam as a distribution platform allows for incredible scalability, comes with native controller support, and provides a huge boost to the game’s discoverability. Releasing PharmacyGo on Steam would elevate it from a student project to a legitimate indie game.

Though the upsides are strong, the costs bring concerns of staying within the project's intended scope. Continuous integration and patch support are made much more difficult on Steam than on Itch. Though the game would be more discoverable to random users, the target audience of OSU pharmacy students would find themselves not only downloading an executable of the game, but also downloading Steam, dealing a significant blow to the game’s accessibility. Furthermore, pushing a game to Steam comes with a $100 publication fee, with potentially more costs incurred for MacOS support (Steamworks Documentation).

**This is the best option for performance, scalability, and becoming a polished product.**

## **4 - Recommendation and Verification Plan**
My recommendation is for our team to go with **option 1** - to continue using WebGL builds to keep the game playable in-browser. Migration to an alternative platform represents an expensive challenge; sometimes expensive financially, but primarily in terms of the time we would spend rebuilding architecture and existing pipelines. Our project partner warned us that v2’s team spent a lot of their time re-working platform issues before eventually settling on Itch. If my team and I had more experience and more time, upgrading platforms could have been the right decision. However, building off of v2’s experience, I believe that continuing to host the game on Itch is our best option. 

Our primary goals as the v3 team are to build new features, enhance existing gameplay, and bring much-needed polish to the game we inherited from the v2 team. Option 1 grants us the most time and freedom to accomplish these goals, therefore I recommend it above the others.


## **5 - Constraints**
Our primary constraint is the requirement of the game being cross-platform, which my proposed solution meets. Additionally, our project partner has requested live access to a database GUI from which he can edit the pharmaceutical study questions and answers at any time. Staying with Itch also meets this requirement, as team v2 already created a database API that we can build on in v3.

## **6 - Delegation and Validation**
I made my recommendation without the assistance of AI tools. We manually test the game during development cycles to get a holistic view of the changes we’ve made, and to identify any bugs we need to address. Should we continue to use Itch, performing manual tests of our game hosted on Itch platform will be our key to checking whether this decision was successful.

Before making this decision, we will discuss with our project partner, explaining to him the pros and cons of each option. We will also read over our peer reviews and take their feedback into account before cementing our choice.

After discussing with our project partner, we have confirmed that option1 is our most appealing option. Therefore, we will continue to use WebGL builds hosted on Itch as our target platform for PharmacyGo version 3.

## **Revision Log**
| Source | Feedback | Decision | Where or Why |
| --- | --- | --- | --- |
| Reviewer 1 | Option 2 - creating a standalone executable - did not mention that executables could be hosted on Itch | Accepted and revised my draft | In the Analysis section covering option 2 | 
| Reviewer 2 | Indenting and formatting would boost the document’s readability and professionalism | Accepted and revised my draft | No indents were added because I felt they were unnecessary, but I did improve my markdown formatting across the entire document to make the section hierarchy more clear | 



## **Sources**
Chang, A. (2026, February 25). Itch.io vs. steam early access for indie game developers. Spiral Up Games. https://www.spiralupgames.com/post/itch-io-vs-steam-early-access-for-indie-game-developers

COE IT_Support. (2026, October 14). Your PUBLIC_HTML folder. Information Technology and Computing Support. https://it.engineering.oregonstate.edu/web/public-html

Steamworks Documentation. (2008a). Steamworks Documentation > Getting Started > Steam Direct Fee. https://partner.steamgames.com/doc/gettingstarted/appfee

Steamworks Documentation. (2008b). Steamworks Documentation > Store Presence > Applications > Platforms. Steamworks. https://partner.steamgames.com/doc/store/application/platforms

Trivellato, M. (2016, September 20). Understanding memory in Unity Webgl. Understanding memory in Unity WebGL. https://unity.com/blog/engine-platform/understanding-memory-in-unity-webgl

Unity Documentation. (2016). WebGL networking. https://docs.unity3d.com/540/Documentation/Manual/webgl-networking.html



