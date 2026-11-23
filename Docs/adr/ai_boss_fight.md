# RFC - AI Integration in Boss Fights


## CS.019 - PharmacyGo_v3

| Field | Value |
| --- | --- |
| Nathan Ngo | <ngona@oregonstate.edu> |
| Date | 2026-11-22 |
| Status | Proposed / Accepted / **Rejected** / Superseded |
| Decision Date | 2026-11-17 |
| Deciders | BJ (Project Partner) |
| Superseded By | N/A |


## Decision
Our project parter, BJ, has rejected the initial recommendation (Option 2). His reasoning is that the for the AI Rival in battles be for quick reactions, while it would be excluded from boss battles because those are meant to be slower and more complex. He hopes for our team to make boss fights more engaging and dynamic for the players instead.

The AI Rival will remain excluded from boss battles (Option 1 remains in effect). The rest of this RFC will be retained as a record of the options that were weighed and why the recommendation was turned down.

## Problem & Context
Our team's Capstone Project 019 is PharmacyGo_v3. Our team will be iterating on an existing PharmacyGo worked on by a team from the previous year. The game works as a study tool/guide for PHAR 736 students, having been built in Unity 6.0 and modeled after classic 2D top-down Pokemon games.

The v2 team added an AI "Rival" companion that follows the player wherever they go (`AiRival.cs`, a `DontDestroyOnLoad` singleton) and competes with the player to answer questions in regular battles ("wild encounters"). If the Rival selects the answer before the player, they win and gains points. One of our non-functional requirements is to improve upon the AI Rival's functionality (Docs/requirements.md).

Currently, the AI Rival does not participate in "boss" battles. `BattleSystem.cs` only begins the Rival's answer routine in wild battles, with the comment that "Al don't participate in boss battle." A boss fight costs 10 coins, asks the player a fixed number of questions, and is only winnable if every question is answered correctly. Winning unlocks the next level and grants a 10,000-point bonus reward.

The decision here is whether and how the Rival would take part in boss battles. There would be 3 hard things to reverse:
- Level-Unlock Rule: If the Rival can take questions from the player, than the condition for the player to answer all questions correctly must be updated. Students will have to learn this new rule, and the project partner must approve of it.
- Battle Architecture: `BattleSystem.cs` is a shared, heavily coroutine-based code. The v2 handoff warns us that it causes race conditions when new mechanics are added.
- Scores: The Rival's score count is saved in `PlayerPrefs`, and the player's score updates to the leaderboard with every change. Scores that have already been uploaded cannot be reversed.

## Options
- **Option 1: Change nothing. Rival stays out of boss battles.**
  - No cost
- Option 2: Extend existing scripted Rival into the boss battles.
  - Low-med cost; requires a refactor & a new boss rule
- Option 3: Adopt an AI framework (Unity Behavior or ML-Agents)
  - Med-high cost; new package dependency
- Option 4: Use a generative AI (LLM) service.
  - High ongoing cost; requires a server & risks question accuracy

## Analysis
Option 1:
- AI Rival remains absent from boss battles; zero risk to the existing boss rules, scores, and battling code.

Option 2:
- Rival waits 50-80% of the player's recent average answer time, then guesses answers it hasn't tried; simple method of dynamic difficulty adjustment (Hunicke & Chapman).
- The cost would be moving Rival logic from `BattleSystem.cs` into a small, testable class. This also means each of the Rival's routine needs an explicit stop, because disabling the script itself doesn't disable its coroutines.
- New boss rule: a question answered first by the Rival returns into the queue instead of being counted as wrong. The player still has to answer every question correctly, so the study goal is preserved.
- No new packages; already works in WebGL.

Option 3:
- Unity Behavior provides visual behavior trees for Unity 6.0. This is overkill for Rival with only two choices (when to answer & which answer); graph asset files hard to review in PRs.
- ML-Agents trains agents with reinforcement learning in Python, so the agent would only learn a timing rule we'd write by hand; docs don't confirm WebGL support.

Option 4:
- The game is a static itch.io build, so an API key would ship to every browser. OWASP flags hardcoded secrets as a common problem.
- Needs a paid, hosted server owned by someone.
- Generated study content can't be verified, and our requirements state all study information must be correct.

## Recommendation & Verification Plan
Original Recommendation (Rejected): My recommendation is to go with Option 2 - placing it behind a feature flag, we can use the requeue rule if the partner project approves of it. If not, then we can fall back to Option 1. Option 2 works best because it meets the requirements based off of v2's working code, doesn't add dependencies, and fits the browser constraint.

Decision After: We keep things as they are with Option 1. Some verification to watch out for as boss fights are reworked moving forward:
- Tests: A test to run a boss battle with the Rival logic active and checks that the Rival does not answer and `AiRivalScore` remains unchanged. A second test can check if the Rival still answers questions in wild battles.
- Until then: Any PR changes that changes and updates Rival check or `BattleSystem.cs` requires a human reviewer to playtest a boss battle with the AI Rival active.

Reversing: This is cheap to do because currently, there exists no code for the Rival within boss battles.

Next: Moving forward, the plan is to make boss fights more "engaging and dynamic" as the follow-up decision. Some options & ideas to consider alongside BJ's input/approval for new boss battle mechanics as well as the Winter RFC:
- Boss Health Bar: Instead of making players answer a fixed number of correct questions, the boss is given a health bar. Every question answered correctly deals "damage," lowering the boss's health, and answering incorrectly penalizes the Player in some way. At the very least, things could remain the same, but a visual boss health bar could help with further "engaging" players.
- Player Hearts System: Alongside the boss health bar, players could have three hearts, in which answering a question incorrectly makes them lose one heart. This mechanic would make boss fights more forgiving without necessarily making it easier for the Player. Being on a correct streak could also deal higher "damage" to the boss as well.
- Multiple Boss Phases: In the boss's Phase 1, they still give the standard multiple-choice questions, then in Phase 2 or higher, the questions could become more difficult with a shorter time limit for answering. This provides players with more adrenaline and urgency.

## Constraints
With Option 2, the rival only selects among existing answers and doesn't create or change study content. Our project partner would require Unity, itchi.io, and browser play themselves, so the solution must work in WebGL with no server. No third-party code is added and the Rival would only utilize session & local data, keeping information more confidential.

## Delegation & Validation
I made my recommendation with the aid of AI assistance (Claude) to help read through files and drafting formatting. I checked code within Unity editor and other sources myself. Future changes will need to be reviewed with our partner and peer feedbacks before committing changes.

Update:
- Updated/revised RFC around BJ's latest response.
- Corrected incorrect citation year (Hunicke & Chapman, 2004).

If AI tools are to write or assist in the Rival checks and its tests, a teammate reviews the PR and playtests a boss battle and a wild battle before merging since `BattleSystem.cs` is a shared system.

## Sources
- Hunicke, R., & Chapman, V. (2004). AI for dynamic difficulty adjustment in games. Northwestern University. https://users.cs.northwestern.edu/~hunicke/pubs/Hamlet.pdf
- OWASP Foundation. (n.d.). Secrets management cheat sheet. https://cheatsheetseries.owasp.org/cheatsheets/Secrets_Management_Cheat_Sheet.html
- Unity Technologies. (2025). ML-Agents toolkit (Release 23). GitHub. https://github.com/Unity-Technologies/ml-agents
- Unity Documentation. (n.d.-a). Behavior (com.unity.behavior). https://docs.unity3d.com/6/Documentation/Manual/com.unity.behavior.html
- Unity Documentation. (n.d.-b). Coroutines. https://docs.unity3d.com/6/Documentation/Manual/Coroutines.html
- Unity Documentation. (n.d.-c). Inference Engine. https://docs.unity3d.com/Packages/com.unity.ml-agents@4.0/manual/Inference-Engine.html
