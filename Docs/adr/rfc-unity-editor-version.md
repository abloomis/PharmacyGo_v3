# RFC: Unity Editor Version for PharmacyGO! v3

| Field | Value |
| --- | --- |
| Author | Shawn (Sangwoo) Park, Quality Owner |
| Date | 2026-10-25 (revised 2026-11-22) |
| Status | Proposed |
| Decision Date | TBD |
| Deciders | BJ (project partner), Alex Loomis, Zach Goldstein, Nathan Ngo, Shawn Park |
| Superseded By | — |

## Problem and Context

PharmacyGO! is a study game for students at the OSU College of Pharmacy who are taking PHAR 736 (Drug Action III, Autonomic Drug Action). Students move around top-down pixel-art levels, answer drug questions in battles and card minigames, and get coins and points while getting ready for their exams (see README.md). Our project partner BJ owns the question content and edits it in a separate database app, and the game loads the questions at runtime (Docs/design.md, Assets/Scripts/Questions/Database.cs). v1 and v2 were made by earlier student teams. Our team of four is making v3, which adds new levels, new minigames like Blackjack (FR-02 in Docs/requirements.md), and fixes for the old ones. The partner wants the game made in Unity, hosted on itch.io, and playable in a browser (Docs/risks.md, "Closed").

This RFC is about the Unity Editor version, which the rest of the project depends on. Right now we're on Unity 6000.0.24f1 (ProjectSettings/ProjectVersion.txt, README.md). That's what the v2 team left us. The question is whether we stay on it or move to a newer version.

This is not easy to undo. Opening the project in a newer Unity rewrites project settings, scene and asset files, and package versions. Unity's upgrade guide even says to back up the project first because the files get changed in place.

Why we need to decide now:

- Unity 6.0 LTS support ended in October 2026. Only Enterprise and Industry customers get one more year (Unity 6 releases page). We use Unity Personal, so we don't get bug fixes for 6000.0 anymore.
- There's a known security issue, CVE-2025-59489 (High, CVSS 8.4). Unity's advisory says every 6000.0 version before 6000.0.58f2 has it, so ours does too. The affected platforms are Android, Windows, Linux, and macOS. Web isn't on the list.
- We add minigames, levels, and art every sprint (Docs/requirements.md). Every new scene and script is one more thing to check after an upgrade, so the longer we wait, the more work it becomes.
- Our project ends in June 2027, and BJ might give the game to a v4 team.

If we do nothing, the game will keep working on itch.io for now. But we'd be stuck on an editor with no patches, any desktop or mobile build later on would have the security issue, and the upgrade would keep getting harder. Also, someone on the team could open the project in a newer editor by accident and push the upgraded files before we decide anything. We don't have CI (Docs/risks.md, R-3), so nothing would catch that.

## Options

### Option 0: Do nothing (stay on 6000.0.24f1)

No work now. But we keep the security issue, we get no more patches, and a later upgrade gets more expensive every sprint. This only makes sense if the game stays web-only forever, no v4 team picks it up, and we never run into an engine bug that needs a fix.

### Option 1: Patch upgrade to 6000.0.58f2 or newer

This is a small change. Only the patch number changes, so package versions should mostly stay the same. We'd still need to test every scene and minigame once. It fixes the security issue, but 6.0 still won't get new patches after October 2026. It's a good choice if we want the security fix with as little risk as possible, or as a backup if Option 2 breaks too much to fix in one sprint.

### Option 2: Upgrade to Unity 6.3 LTS

More work. URP (17.0.4 now), Input System, Cinemachine, the Leaderboards service package, and other packages will update too (Packages/manifest.json). We might have to change some scripts, and 2D lighting could look different, so we'd need a full test pass and maybe some fixing. 6.3 LTS is supported until December 2027. This is the one to pick if the game will be maintained after June 2027 or given to another team, or if we might build for anything other than web.

### What about Unity 6.6?

Unity 6.6 is newer, but it's an Update release, not LTS. Unity only supports an Update release until the next one comes out. We'd get the same upgrade risks as Option 2 with much shorter support, so I didn't list it as an option.

## Analysis

### Security

Options 1 and 2 both fix CVE-2025-59489. The 6000.0.58f2 release notes list "Scripting: Addressed CVE-2025-59489" as the fix. Unity's advisory also says there's no evidence of it being exploited, and Web/WebGL isn't one of the affected platforms. Since the partner wants a browser game (Docs/risks.md), the actual risk to students right now is low. It really only matters if we or a later team make desktop or Android builds. So I'd count security as a long-term reason not to pick Option 0, but it isn't urgent on its own.

### Support and handoff

This is the biggest difference between Option 1 and Option 2. With Option 1 we're on a version that stopped getting patches in October 2026. Option 2 gets patches until December 2027, which covers the rest of our project (June 2027) and the start of any v4 work. I think this matters most for the handoff. If a v4 team gets the project on 6.0, they'd have to do the upgrade themselves, and they'd know the code less than we do.

### What might break

Option 1 is low risk because the packages stay the same. Option 2 is where I'm not sure, so I went through Unity's "Upgrade to Unity 6.3" guide and compared it with our project:

- URP Compatibility Mode was removed in 6.3. Our URP global settings already have `m_EnableRenderCompatibilityMode: 0` (Assets/UniversalRenderPipelineGlobalSettings.asset), which means we already use render graph, so this shouldn't affect us.
- I didn't find any custom `ScriptableRendererFeature` or `ScriptableRenderPass` code in Assets/, so the URP API changes in the 6.2 and 6.3 guides shouldn't hit our scripts.
- We use the 2D Renderer (Assets/Settings/Renderer2D.asset), and 11 scenes have 2D lights, including Hub and Level 2. Lighting is the most likely place for something to look different, so we should compare screenshots for these scenes.
- We're going from 6.0 straight to 6.3. Unity says to read all the upgrade guides in between (6.1, 6.2, 6.3), since a big jump can cause more problems than going one version at a time.
- 14 non-module packages in Packages/manifest.json will get resolved again. Leaderboards connects to Unity Gaming Services, so we need to test it against the real service, not just check that it compiles.

Even after all this, I can't know how many compile errors or visual changes we'll get until we actually try it. That's why I want to try it on a branch first.

### Team impact

All four of us need to be on the same editor version. The upgrade has to be a single PR, merged when nobody else has open changes in ProjectSettings/ or Packages/. Otherwise we'll get merge conflicts in files that are really hard to merge by hand. The Option 2 trial would take about one person for part of a sprint, and if it fails, main isn't affected.

I also looked for a performance comparison between 6.0 and 6.3 for a small 2D WebGL game like ours, but I couldn't find one, so performance isn't part of this decision.

Overall, we can't promise that the game stays web-only or that no one will maintain it later, so I don't think Option 0 works. Option 1 is the minimum we should do. Option 2 is more work now, but it's the only one that keeps us on a supported version through the handoff.

## Recommendation and Verification Plan

I recommend Option 2 (Unity 6.3 LTS), but tried on a separate branch first, with Option 1 as the backup. If the trial can't pass the checks below within one sprint, we'll do Option 1 instead so we at least get the security fix.

Steps:

1. Tag main before upgrading (something like `pre-unity-6.3`) so we can roll back.
2. Take a screenshot of every scene in the build settings and do one full playthrough on the current itch.io build, so we have something to compare against.
3. As Quality Owner, I'll do the upgrade on a branch called `upgrade/unity-6.3` after reading the 6.1, 6.2, and 6.3 upgrade guides.
4. Put the version, package, and settings changes in one PR, with no feature work in it.
5. A teammate who didn't do the upgrade reviews the PR and runs the checks below.
6. After merging, update the editor version in README.md and have everyone switch on the same day.

We merge only if all of these pass:

- No compile errors, and no new warnings from our own scripts in the Console.
- Every scene in ProjectSettings/EditorBuildSettings.asset goes into Play mode without new errors.
- The 11 scenes with 2D lights look the same as the baseline screenshots. If something looks different, the Designer has to sign off on it.
- Questions load from the live database, including at least one picture question (Database.cs; `DownloadImage` in MinigamePilot.cs).
- You can enter a username, and the score shows up on the leaderboard after a game.
- Old saves still work. Play the current itch.io build to a later level, then open the new build in the same browser and hit Continue. The player should be in the same level and position with the same coins and items (saveManager.cs, ProgressionState.cs). This is to catch any change in how browser storage is read.
- The WebGL build loads and plays on itch.io in Chrome and Firefox.

A normal playthrough can miss edge cases, so we'll also check each minigame:

- Card Matching: finish a game. Close the window during the opponent's turn, open it again, and restart, since this broke before (issue #23 comments in CardMatchingMinigame.cs). Also make sure picture cards load.
- Slapjack: win once and lose once. Close the window during the opponent's turn and restart (issue #4 in SlapjackMinigame.cs), then check that the card counters are still right.
- Word Bank: get one right and one wrong, and check that there are more word choices at higher difficulty (WordBankMinigame.cs).
- Blackjack: hit until you bust, and also stand and let the dealer finish. Answer right (pick 1 of 2 cards) and wrong (get a random card). Then restart in the middle of a hand.

After merging, we'll put an "upgrade" label on any bug that could be from the upgrade for the next two sprints. If one of those bugs blocks a sprint goal and we can't fix it in two days, we revert to the tag and go with Option 1. If we get to the end of fall term with no open "upgrade" bugs and the itch.io build passes the checklist, I'd say the decision worked.

## Constraints

### Licensing

Unity Personal is free for organizations with under $200K in revenue and funding over the last 12 months. But Unity's FAQ also says that if you use Unity to provide services to a client, you're only eligible if the client is under that limit too. Our client is the OSU College of Pharmacy, so we need the instructors or BJ to tell us which plan we should use (Personal, or a Student or education plan). The rule is the same for 6.0 and 6.3, so it doesn't change which option is best, but we should confirm it before the upgrade PR gets merged. The upgrade PR also shouldn't add any new third-party packages.

### Data

The leaderboard sends usernames and scores to Unity Gaming Services. The Unity Cloud project is linked to an organization we don't control (`organizationId` in ProjectSettings/ProjectSettings.asset; Docs/risks.md, R-6). The upgrade can't relink the project to a different Cloud project. We also don't have access to the dashboard, so the only way to test the leaderboard is from inside the game, which is why it's one of the merge checks. Saves are kept in the player's browser, and the save check above covers that.

### IP and partner content

The game is "All rights reserved" with the College of Pharmacy (README.md). The upgrade only changes engine, package, and settings versions, so it doesn't change who owns anything. The question content belongs to BJ. During testing we only check that questions load and show up. We won't copy any question data out of the game or share it outside the team.

## Delegation and Validation

I used an AI assistant to scan our public repo (ProjectVersion.txt, Packages/manifest.json, URP settings, scene files, and minigame scripts), to find Unity's advisory, support dates, upgrade guides, and licensing pages, and to suggest structure and wording for this revision. The AI did not change the repo.

This was reasonable because every claim points to a file in the repo or a public Unity page, so I could open it and check. The parts that would cost the most to get wrong, the upgrade itself and the merge decision, are not handed to AI.

Where I checked myself:

- The version in ProjectVersion.txt, because the whole RFC depends on it.
- The security advisory, to confirm 6000.0.58f2 is the fix and Web is not affected, because this sets how urgent the decision is.
- The support dates on Unity's release page, because "support has ended" is the main reason for the decision.
- The licensing page, because licensing is hard to fix after release.
- The upgrade itself will be done by a person on a branch, with every check above. It rewrites project files and is hard to undo, so the tag on main is our rollback.

## Decision

Proposed. The team has not made a final decision yet.

## References

- Unity. Security Update Advisory (CVE-2025-59489). https://unity.com/security/sept-2025-01
- Unity. 6000.0.58f2 release notes. https://unity.com/releases/editor/whats-new/6000.0.58f2
- Unity. Unity 6 releases (support dates). https://unity.com/releases/unity-6
- Unity. Unity releases (LTS and Update release support). https://unity.com/releases
- Unity Manual. Upgrade your Unity project. https://docs.unity3d.com/Manual/upgrade-project.html
- Unity Manual. Upgrade to Unity 6.3. https://docs.unity.com/en-us/engine/6000.3/manual/upgrade-guides/upgrade-guide-unity63
- Unity Manual. Upgrade Unity (guides for 6.1, 6.2, 6.3). https://docs.unity.com/en-us/engine/6000.3/manual/upgrade-guides
- Unity. Unity Personal. https://unity.com/products/unity-personal
- Unity. Plans and Pricing (FAQ). https://unity.com/products
- PharmacyGo_v3 repo: README.md, ProjectSettings/ProjectVersion.txt, ProjectSettings/ProjectSettings.asset, ProjectSettings/EditorBuildSettings.asset, Packages/manifest.json, Assets/UniversalRenderPipelineGlobalSettings.asset, Assets/Settings/Renderer2D.asset, Assets/Scripts/saveManager.cs, Assets/Scripts/ProgressionState.cs, Assets/Scripts/Questions/Database.cs, Assets/Scripts/Player/Minigames/, Docs/requirements.md, Docs/design.md, Docs/risks.md
