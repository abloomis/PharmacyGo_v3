# Risks

**Project goal (from our 10/11 meeting with BJ):** make PharmacyGO! v3 a better study tool for PHAR 736 students by adding new levels and improving the minigames, while keeping it cross-platform (built in Unity, hosted on itch.io, playable in the browser) and making sure BJ can still edit the questions through the database GUI.

This is our only risk register. We rank each risk by how likely it is to happen and how bad it would be if it did (high / medium / low). The highest risks are at the top. We go over the top few at every sprint planning.

Owners are based on our charter roles (PM: Zach, AI Coordinator: Nathan, Quality Owner: Shawn, Designer/Docs: Alex). When roles rotate in week 8, we will update the owners here.

| # | Risk | Type | Likelihood | Impact | Owner |
|---|---|---|---|---|---|
| R-1 | We don't have access to the itch.io account | Business | High | High | Alex |
| R-2 | Questions load from a v2 member's personal GitHub repo | Technical | Medium | High | Zach |
| R-3 | No automated tests or CI | Technical | High | Medium | Shawn |
| R-4 | Some picture questions are broken | Technical | High | Medium | Shawn |
| R-5 | Unity version is old and the upgrade isn't decided | Technical | Medium | Medium | Zach |
| R-6 | Leaderboard is tied to a Unity account we don't own | Technical | Medium | Medium | Nathan |

---

## R-1: We don't have access to the itch.io account
The v2 game is on itch.io under the v2 team's account (shiningn-osu). Nobody on our team has access to it, and we don't have our own account yet. We need somewhere to put our build by week 10 (Repo Checkpoint 2), so this is a big deal.

- **Why high/high:** we have no access right now, and deploying is a graded milestone.
- **What we'll do:** ask BJ and the v2 team to give us access or transfer the page. If that doesn't work, we'll make a team account and a new page.
- **Warning sign:** still no access by Nov 15.
- **Asked:** not yet. We plan to ask at the Nov 3 meeting with BJ.

## R-2: Questions load from a v2 member's personal GitHub repo
The game gets all its questions from kyofyufufufufufufufu/test_database1 through the GitHub API, without logging in (see Assets/Scripts/Questions/Database.cs). GitHub only allows 60 of these requests per hour per IP address. If a lot of students play on the same network (like on campus before an exam), the questions might not load. Also, if that person deletes the repo or makes it private, the game stops working.

- **Why medium/high:** we don't know if this has happened to players yet, but it could happen if a class plays at the same time before an exam, and with no questions there is no game.
- **What we'll do:** move the data somewhere the team and BJ control, or keep a backup copy of the questions in the build. We won't change the data itself, since that's BJ's job.
- **Warning sign:** "Database Error" in the console, or players say the quizzes are empty.
- **Asked:** not yet. We plan to bring it up at the Nov 3 meeting.

## R-3: No automated tests or CI
The Unity Test Framework package is already pulled in as a dependency, but there are no test scripts or test assemblies in the project, and there are no GitHub Actions workflows. Right now, a broken script or scene could get merged and nobody would notice until later.

- **Why high/medium:** four of us are changing shared code and scenes, and bugs found late take longer to fix.
- **What we'll do:** set up a test assembly, write at least one test that fails when something breaks, and set up a workflow that runs on every PR. Until then, a teammate tests the build before approving a PR (from our Workshop 3 safety net plan).
- **Warning sign:** something on main stops compiling or a scene breaks after a merge.

## R-4: Some picture questions are broken
At our 10/27 meeting with BJ, it came up that some pictures from v2 weren't imported into the database correctly. We can't fix the data ourselves because that's BJ's area.

- **Why high/medium:** we already know it's happening, but it only affects questions with pictures.
- **What we'll do:** play through the picture questions, make a list of the broken ones, and send it to BJ.
- **Warning sign:** a question shows a missing or wrong image.

## R-5: Unity version is old and the upgrade isn't decided
We're on Unity 6000.0.24f1. Zach's RFC (Docs/adr/unityVersionUpdate) says this version is affected by a security issue (CVE-2025-59489) and suggests moving to 6.3 LTS. This was our top risk in Sprint Notes 1. The longer we wait, the more code there is that could break during an upgrade.

- **Why medium/medium:** the decision is still open, and upgrading could break packages or assets.
- **What we'll do:** email the RFC to BJ before the Nov 3 meeting and decide after his feedback. If we upgrade, do it on a separate branch and test every scene before merging.
- **Warning sign:** no decision by Nov 15.

## R-6: Leaderboard is tied to a Unity account we don't own
The leaderboard uses Unity Services, and the project is linked to a Unity Cloud project under the organization crimsonender99 (in ProjectSettings), which isn't one of our accounts. Cloning the repo doesn't give us access to that dashboard, so we can't change any settings, and if the owner removes it the leaderboard will stop working.

- **Why medium/medium:** the owner isn't on our team, but the main game still works without the leaderboard.
- **What we'll do:** ask for access through BJ or the v2 team, or link the project to our own Unity organization.
- **Warning sign:** "Services Init Failed" in the console, or we need to change a leaderboard setting.
- **Asked:** not yet. We plan to ask at the Nov 3 meeting.

---

## Closed

- **Agreeing on where the game runs:** at the 10/11 meeting, BJ said the game must be made in Unity, hosted on itch.io, and playable in the browser. Getting access to the account is tracked in R-1.
