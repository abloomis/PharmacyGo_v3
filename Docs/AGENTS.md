# AGENTS.md

Instructions for AI coding agents working on PharmacyGO! v3, a 2D Unity study game for OSU College of Pharmacy students. This file points to the project docs instead of repeating them. Read the doc for your task before changing code. Any other agent file (e.g., `CLAUDE.md`) should point here.

## Where to look
- `README.md`: setup and quickstart (Unity version, opening the project, the starting scene).
- `CONTRIBUTING.md`: branch naming, PR contents, definition of done, review rules.
- `Docs/requirements.md`: what the game must do, acceptance criteria, **constraints**, and non-goals.
- `Docs/design.md`: architecture, interfaces, data (how questions and medications load), deployment, and deferred decisions.
- `Docs/risks.md`: known risks and debt. Check it before touching question loading, the leaderboard, or the Unity version.
- `Docs/adr/`: decisions in progress (Unity version upgrade, target platform). Follow an accepted ADR over this file.

## Working in this repo
- Use the Unity editor version in `ProjectSettings/ProjectVersion.txt`. Don't change it unless the Unity upgrade ADR is accepted and the task is the upgrade.
- You usually can't run the Unity editor. Keep changes compiling, and end with exact Play-mode test steps (scene and actions) for a human.
- Build and test commands: see `README.md` and the CI workflow once it exists (tracked in `Docs/risks.md`).
- Put new scripts in the matching `Assets/Scripts/<System>/` folder. Keep one `MonoBehaviour` per file, with the file name matching the class name.

## Unity pitfalls
- Never delete, regenerate, or hand-edit `.meta` files. When you move or rename an asset, move its `.meta` with it.
- Don't hand-edit `.unity`, `.prefab`, or `.asset` YAML. Describe the Inspector change for a human instead.
- Renaming a `MonoBehaviour` or its serialized fields breaks scene and prefab references. Use `[FormerlySerializedAs]` and flag class renames.
- Don't touch `Library/`, `Temp/`, `Logs/`, `UserSettings/`, `Builds/`, or the third-party folders `Assets/Joystick Pack/` and `Assets/TextMesh Pro/`.

## Ask a human first
- Changes to shared systems: `Hub.unity`, `DialogBox`, `BattleSystem`, `GameControl`, `LevelManager`, and the score and coin managers.
- Changes to the question data format or source (`Assets/Scripts/Questions/`); see `Docs/design.md`.
- Package or `ProjectSettings/` changes, leaderboard or Unity Services settings, or deleting scenes, prefabs, or art.
- Any drug, medication, or question content. It comes from the College of Pharmacy partner; never invent or edit it.
- Never commit secrets or credentials.
