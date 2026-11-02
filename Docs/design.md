# Design

## Overview
Study game for OSU PHAR736 students, developed in Unity and hosted on 
Itch. A [WinForms C# app](https://github.com/kyofyufufufufufufufu/test_database1) manages database
integration so the project partner can edit study questions and material.

## Architecture
![C4 context diagram](./diagrams/context.png)
![C4 container diagram](./diagrams/container.png)
- Game Engine (Unity, WebGL): overworld, battles, quiz gameplay, runs in the student's browser.
- Admin App (C#, WinForms): edit, save, and bulk upload study questions, commits to GitHub via GitHub API.
- Question Data: [jsonTest.json](https://github.com/kyofyufufufufufufufu/test_database1/blob/main/jsonTest.json) loaded in by the admin app.
- Itch.io: hosts and serves the WebGL build.

## Interfaces
- Question set schema: JSON, defined by the [QuestionSet](https://github.com/kyofyufufufufufufufu/test_database1/blob/main/DataModels.cs) C# class, updating this requires updating both apps.
- GitHub API: the admin app fetches and commits using a token, which is read by the game. 

## Data
- JSON file in git
- Player save data

## Deployment
- Game: Unity WebGL uploaded to Itch.io manually, public access.
- Admin app: csv or xslx file uploaded manually.
- Rollack: re-upload the previous build to Itch.

## Open questions
- Who owns the Itch.io account long term?

## Deferred decisions
- Character customization: best approach (palette swap, different sprites) and how choices are saved.
- Backend: using GitHub tokens may be limiting if player volume or rate limits increase. 
