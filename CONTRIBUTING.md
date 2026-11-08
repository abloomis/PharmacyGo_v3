# Contributing Guide

## Prerequisites

- [GitHub](https://github.com/) account
- [GitHub Desktop](https://desktop.github.com/)
- [Unity Hub](https://unity.com/download)
- Unity editor version **6000.0.24f1** — download from the [Unity Editor Archive](https://unity.com/releases/editor/archive) (select "All versions" to find it)

## Local Setup

1. Clone the [PharmacyGO repository](https://github.com/abloomis/PharmacyGo_v3) using GitHub Desktop
2. Open the project in Unity Hub by selecting the "add" > "Add project from disk" options, then selecting the local copy of the repository
3. Confirm the editor version is **6000.0.024f1**
5. In the Project window, navigate to `Assets/Scenes/Main Levels/` and open **Main Menu**
6. Press **Play** — if the game runs without errors, your setup is good

## Expected Contribution Workflow

### 1. Branch Naming

Keep branch names short, lowercase, and descriptive of what you're working on.

Examples:
- `feature/doctor-peek-ui`
- `fix/caption-text-display`
- `chore/cleanup-hub-scene`

### 2. Opening a Pull Request

1. Push your changes to your branch
2. Go to the [Pull Requests tab](https://github.com/abloomis/PharmacyGo_v3/pulls) and open a new PR
3. Assign relevant team members as reviewers (see [Team](#team) below)

### 3. What to Include in Your PR

- **Subject** — what you changed and why
- **How to test it** — exact steps a reviewer should follow to verify the outcome
- **Screenshots or recordings** if the change is visual

### 4. Definition of Done

- No compiler errors
- Tested as described in the PR and works as expected
- At least one team member has approved the PR

### 5. Code Review Expectations

- Leave comments on logic that isn't immediately obvious
- Flag anything that touches shared systems (Hub scene, DialogBox, BattleCanvas) — these affect multiple contributors
- Approve only if you actually tested it

## Pushing a New Build to Itch

The main branch should always hold a functional, ready-to-ship version of the game that students can play on Itch. Whenever the game is patched, make sure to update it on Itch as well. Below are the steps to do so.

1. In the Unity editor, select "File" in the top left then "Build Profiles".
2. This creates a "Build Profiles" window. On the left side, select "Web" as your platform. The first time you do this, you may need to install WebGL packages; Unity will let you know if you need them.
3. Select "Switch Platform", then select "Build" (NOT "Build and Run"). Unity will ask for a directory to hold the build files, you can use /Builds for example.
4. After several minutes, the build will complete. Open the directory where you placed the files, then drag-click to select them all, and right-click to zip them all into a compressed folder.
5. Open your Itch page for PharmacyGo and enter the page editor. In "Edit Game", scroll down to "Upload Files", then paste the newly created zip folder.
6. While on the Itch page, ensure that the "This file will be played in the browser" and the "Mobile friendly" options are toggled on. Also, ensure that the embed option is set to "Click to launch in fullscreen".
7. Now the newest build of the game should be running on your Itch page! Repeat this process for every new patch.

## Reporting Bugs / Requesting Changes

[Open an Issue](https://github.com/abloomis/PharmacyGo_v3/issues) in the repository and include:

1. A clear description of the bug or requested change
2. Which file(s) are involved
3. The condition or steps that trigger it
4. Expected vs. actual behavior

## Version 3 Development Team

| Name | Area | GitHub | Email |
|---|---|---|---|
| Alex Loomis | Level Design, Art Direction | [@abloomis](https://github.com/abloomis) | loomisa@oregonstate.edu |
| Nathan Ngo | AI Development | [@Sen-design03](https://github.com/Sen-design03) | ngona@oregonstate.edu |
| Shawn Park | Game Content Development, Minigames | [@SSPark04](https://github.com/SSPark04) | parks3@oregonstate.edu |
| Zach Goldstein | Level Design, Database Management | [@sangastra](https://github.com/sangastra) | goldstza@oregonstate.edu |

## Version 2 Development Team

| Name | Area | GitHub | Email |
|---|---|---|---|
| Annmarie Geiger | Database Management | [@kyofyufufufufufufufu](https://github.com/kyofyufufufufufufufu) | geigerta@oregonstate.edu |
| Lucy Cheng | AI Development | [@LucyCheng111](https://github.com/LucyCheng111) | chengjuh@oregonstate.edu |
| Nick Shininger | Game Content Development, Minigames | [@shiningn-osu](https://github.com/shiningn-osu) | shiningn@oregonstate.edu |
| Jakob Poore | Level Design | [@poorej](https://github.com/poorej) | poorej@oregonstate.edu |
| Max Baker | Game Mechanics Development | [@Crimson-Ender](https://github.com/Crimson-Ender) | bakerm7@oregonstate.edu |

## Version 1 Development Team
 
| Name | Role |
|---|---|
| Jinpeng Chen | Programmer |
| Alec Duval | Database Manager |
| Quinn Glenn | Menu Designer |
| Xiaoyu Luo | Scene Developer |
| Teagan Simoneau | Project Manager |
| Hoimau Tan | Index Designer |
| Erik Tornquist | Database Programmer |
| Samuel Westerham | Level Designer |

## Handoff & Support

This project was developed as a capstone at Oregon State University. For installation help, technical issues, feature requests, or general questions, submit an email to loomisa@oregonstate.edu

For bugs in the codebase, please open a [GitHub Issue](https://github.com/abloomis/PharmacyGo_v3/issues).
