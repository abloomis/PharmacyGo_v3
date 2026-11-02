### **1\. Project Overview and Status**

**Problem**  
Pharmacy students struggle with engaging ways of learning new material. The most common issue is retaining a large amount of information in very little time with no clear direction on what’s important and what’s not. PhamarcyGO\! Creates a unique learning tool that gamifies traditional studying options with a fun, engaging story that motivates students to enhance their knowledge through different modules.

**Current Deployment**  
PharmacyGO\! has been deployed to itch.io. Students in PHAR 736 can now use the game to study for their tests/modules. The WinForms application for the database executable has been sent to our project partner, where they have been using it to add questions to the json database since the beginning of Spring term.

**Included:** What is and is not included in this handoff (any exclusions and why)

* Game prerequisites and installation instructions  
* Database prerequisites and installation instructions  
* Leaderboard backend access and instructions  
* Known bugs and issues  
* Reference documentation links for project design and requirements  
* Completed checklist for handoff to project partner

**Not Included:**

* Project partner credentials (database credentials shared separately and securely)

**Contact Information:**

* Annmarie Geiger \- [geigerta@oregonstate.edu](mailto:geigerta@oregonstate.edu) / [tabitha.a.geiger@gmail.com](mailto:tabitha.a.geiger@gmail.com)   
* Lucy Cheng \- [chengjuh@oregonstate.edu](mailto:chengjuh@oregonstate.edu) / [lucycheng.cs@gmail.com](mailto:lucycheng.cs@gmail.com)  
* Nick Shininger \- [shiningn@oregonstate.edu](mailto:shiningn@oregonstate.edu) / [nich.s.s.olas@gmail.com](mailto:nich.s.s.olas@gmail.com)   
* Max Baker \- [bakerm7@oregonstate.edu](mailto:bakerm7@oregonstate.edu) / [max.crimsonender@gmail.com](mailto:max.crimsonender@gmail.com)   
* Jakob Poore \- [poorej@oregonstate.edu](mailto:poorej@oregonstate.edu) / [jakobethan27@gmail.com](mailto:jakobethan27@gmail.com) 

### **2\. Setup and Getting Started**

### **Game**

**Prerequisites**

* [GitHub](https://github.com/) account  
* [GitHub Desktop](https://desktop.github.com/)  
* [Unity Hub](https://unity.com/download)  
* Unity editor version **6000.0.024f1** — download from the [Unity Editor Archive](https://unity.com/releases/editor/archive) (select "All versions" to find it)

**Setup & Installation**

1. Clone the [PharmacyGO repository](https://github.com/LucyCheng111/PharmacyGO) using GitHub Desktop  
2. Open the project in Unity Hub and confirm the editor version is **6000.0.024f1**  
3. In the Project window, navigate to `Assets/Scenes/Main Levels/` and open **Main Menu**  
4. Press **Play** — if the game runs without errors, your setup is good

**Project Structure**

Assets/

├── Scenes/

│   └── Main Levels/        \# All playable scenes including Main Menu

├── Scripts/                \# Game logic and UI controllers

├── Prefabs/                \# Reusable Unity objects

├── Resources/              \# Loaded assets (questions, images, etc.)

└── UI/                     \# UI elements and layouts

**How to Run**  
Press Play in the Unity Editor to run locally in the Main Menu scene, or visit

[https\://shiningn-osu.itch.io/pharmacy-go-v2](https://shiningn-osu.itch.io/pharmacy-go-v2) to play the deployed version in-browser.

**How to Deploy**

1. Create an account for itch at [https\://itch.io/register](https://itch.io/register)  
2. Build the game under File \> build profiles \> Select “WebGL”  
3. In Unity, build the game, (press File\>build and run)- it should create a set of files in the selected directory which includes “index.html”, if this file does not exist, and contains a file such as .exe, .app, etc, your build profiles are not configured to be WebGL. Make sure this is the case, as otherwise the game will not run on mobile devices after being posted on itch.  
4. Zip all files created under build into a zip file  
5. In Itch.io, create a new project  
6. Fill in any required information (Title, url, etc)  
7. Under Upload a file, select your zipped directory unity built, and select “this game will be played in the browser”.  
8. Save the game information at the bottom. After saving, under “visibility and access”, select “published”

**Common Pitfalls**

Wrong Unity version: the project will show errors or fail to open if the editor version is not exactly 6000.0.024f1. Always verify in Unity Hub before opening.

### **Requested Information**

**Mystery Levels**

Although these describe and give a base for these features, any implementation will require coding experience to include these in future developments. It cannot be made automatically through the database directly.

**Link to the murder mystery template:** 

[https\://docs.google.com/document/d/1FlV\_AH7Xo5oFrEyuZ3TF6oJSrrO-COpwt1728YOwo6E/edit?usp=sharing](https://docs.google.com/document/d/1FlV_AH7Xo5oFrEyuZ3TF6oJSrrO-COpwt1728YOwo6E/edit?usp=sharing)

The template lays out the information for what is needed to be able to add all necessary components in the murder mystery.

The template requires :

* A list of questions and responses for the patient, if present.  
* List of options, with the correct option to be labelled, for the detective/ doctor  
* Information on all evidence objects in the scene, along with a name/ description  
* Name of the case  
* Which module it is for  
* A brief description of what the scene should look like

For future developers, in Unity, to add a new MM, create a new grid object under the “Cases” object. The rows are each module, with 1 on the bottom and 4 on the top. On the right, patient doctor cases can be placed moving rightward. Murder mystery cases can be placed moving leftward. 

Copying the existing objects, aside from the grid, is an easy way to add a new case, as all of the information can be changed, and new evidence objects can be duplicated and edited as needed. 

Make sure to:

* Add dialog to each evidence object  
* Add dialog to the patient, if present, including the list of questions and responses  
* Add dialog to detective/ doctor (only referred to as detective in the scripts, for consistency)  
* Assign correct index for detective/doctor  
* *ADD ALL NECESSARY INFORMATION INTO THE MURDER MYSTERY CONTROLLER OBJECT IN THE SCENE*  
  * All variable names are descriptive, and comments in the code can explain any confusion   
* As all templates have an existing example, no mechanic exists that has not already been shown.

**Minigames**

Each minigame is created separately, and has little intercommunication.

However, all minigames access the minigame pilot (MinigamePilot.cs) to access the database, to avoid overfetching. All scripts are attached to the main canvas object in the hub scene \> Minigames

To add a new minigame, it is important to add this value to the MinigameType enum in minigamepilot, and the necessary information for starting inside NPCMinigamePlayer, and NPCController

From there, referencing existing minigames can show their consistency in how they are structured, but this is not a necessary guideline. Their only entrypoint is sourced in NPCMinigameController, and so have all necessary methods accessed from there.

### **Database**

**Prerequisites**

* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with the **.NET Desktop Development** workload, or VS Code with the C\# Dev Kit  
* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
* **Newtonsoft.Json 13.0.4** restored automatically via NuGet when the project is built  
* A **GitHub Personal Access Token (PAT)** with `repo` permissions required for the app to fetch and save `jsonTest.json`

**Setup & Installation**

1. Clone this repository:

git clone https\://github.com/kyofyufufufufufufufu/test\_database1.git

cd test\_database1

2. Open `WinFormsApp1.csproj` in Visual Studio 2022  
3. Visual Studio will automatically restore NuGet packages on first build or run manually:

dotnet restore

4. Add your PAT token when prompted by the app  
5. Press **Run** (or `F5`) to launch the app

### **Leaderboard**

**Prerequisites**

* A [Unity cloud services account](https://cloud.unity.com)  
* An invitation by the project owner for access

**Setup:**

* Go to [https\://cloud.unity.com](https://cloud.unity.com) and sign in with your Unity account  
* Once invited to the Pharmacy Go 2 project on Unity Cloud, you should be able access the project through the “dashboard.”  
* You can access the leaderboard backend by clicking on “Services” and then “Leaderboards” will be listed under active services  
* You can a

**How to Run**

Launch the app via Visual Studio (F5) or run the compiled executable directly. The app will prompt for your GitHub PAT on launch, paste the token and the app will fetch and sync jsonTest.json automatically.

**How to Deploy**

The database app is a standalone WinForms executable, no server deployment needed. Deliver the compiled .exe to the project partner directly. 

To build:

1. Open WinFormsApp1.sln in Visual Studio 2022 and go to the Solution Explorer window  
2. Right click WinFormsApp1 and select Publish  
3. Once the publish window is open, select Show All Settings.  
4. Select self-contained in Deployment mode.  
5. Choose Folder as the publish Target location.  
6. Save then click Publish.  
7. Deliver the .exe file and its required .dll files for the application, no installation is required.

**Common Pitfalls**

Missing PAT permissions: the GitHub Personal Access Token must have repo scope. Tokens with read-only or limited scope will cause fetch/save to fail.

Missing DLL files: if the executable fails to launch or throws a "could not load file or assembly" error, ensure the entire published folder is delivered to the project partner not just the .exe file. All .dll files in the folder are required for the application to run.The .exe cannot be moved or run in isolation from the other files in the publish output.

### **Leaderboard**

**Prerequisites**

* A [Unity cloud services account](https://cloud.unity.com)  
* An invitation by the project owner for access

**Setup:**

* Go to [https\://cloud.unity.com](https://cloud.unity.com) and sign in with your Unity account  
* Once invited to the Pharmacy Go 2 project on Unity Cloud, you should be able access the project through the “dashboard.”  
* You can access the leaderboard backend by clicking on “Services” and then “Leaderboards” will be listed under active services  
* You can add new members to the project under the “members” tab.

**How to Deploy:**

The leaderboard is persistent, being hosted on Unity Cloud Services, no deployment is necessary. Additionally, the backend is accessible through a web browser. The leaderboard backend has some basic management options, like controlling who has access to the backend, changing the content of the leaderboard, and CRUD operations for leaderboard entries. 

### **3\. Known Issues, Limitations, and Future Work**

An honest, prioritized accounting of the project’s current state:

* **Known bugs:** link to open issue tracker (or list them if no tracker); include severity  
* **Limitations:** features that are incomplete, descoped, or working but fragile  
* **Technical debt:**   
  * UI in some mobile platforms, when accessed through itch.io, render incorrectly in some ways- namely HUD elements rendering partly offscreen, minigame UI displaying offset, and some background objects displaying oddly.  
    * None of these issues are ever gamebreaking, softlocking, or the like, however, it can look confusing at times.  
  * Level 4 has not fully been implemented at the time of writing, however in the next couple of days will be incorporated into the final build.  
  * Some minigame bugs are present, including display of questions whose correct answer is “all of the above”  
    * This bug was previously fixed, but a merge incorrectly caused the patch to be removed, this will also be fixed in the next couple of days  
* **Future work:** prioritized backlog of features or improvements not yet implemented  
* Any credentials, API keys, environment secrets, or access permissions that need to be transferred

**Known Bugs**

Issues Link: [https\://github.com/LucyCheng111/PharmacyGO/issues](https://github.com/LucyCheng111/PharmacyGO/issues) 

**Limitations f**eatures that are incomplete, descoped, or working but fragile

filler

**Technical Debt**

Question loading logic in Resources/ is tightly coupled to the JSON structure, so refactoring is recommended before adding new question types or additional features applied to the database.

An overreliance on coroutines: The previous group relied too much on using Unity coroutines for various tasks, which can lead to race conditions if proper care is not taken when implementing new features or changes to existing gameplay mechanics. Certain features, like the player movement mechanics were refactored to not need to use coroutines. However, other features, in particular the battle system, are extremely reliant on coroutines beyond what is actually necessary for them to function properly. Refactoring a mechanic as large and complex as that was too much of a time sink to seriously be considered as a project goal, it would have diverted time and resources away from more immediately actionable changes and features the project partner wanted implemented, as well as created unnecessary blockers for other things being developed simultaneously.

**Future Work**

* Organ levels designed for relevant physiology and related to questions.  
* Minigame connection to database flags.  
* Additional murder mysteries and patient diagnoses.

**Credentials**

* GitHub repository access: transfer ownership or add partner as collaborator at [https\://github.com/LucyCheng111/PharmacyGO](https://github.com/LucyCheng111/PharmacyGO)  
* itch.io project page: transfer or share credentials with project partner  
* GitHub PAT for database app: generate a new token under the partner's account using the same repo permissions described in Section 2 

### **4\. Reference Documents**

[Requirements Document](https://docs.google.com/document/d/1XB-Z4XEROeTA22O0gNyEjfbXvg2kKjEMJOxM8qURcPE/edit?usp=sharing)

[Technical Design](https://docs.google.com/document/d/1RT-VoSktUyYlpnuwjnIYHkk7ekQeplpxTPUgSo5FKJs/edit?usp=sharing)

### **5\. Handoff Checklist and Partner Verification**

Complete this checklist and include it in your document. Check each item that is complete. For any unchecked item, include a brief note explaining why and what follow-up is needed.

* \[x \] Repository access transferred to project partner (or archived)  
* \[x\] All credentials, API keys, and secrets shared securely (not committed to the repo)  
* \[x\] Deployment environment access transferred or documented  
* \[x\] All open issues triaged and documented  
* \[x\] README is current and accurate  
* \[x\] Final requirements document and technical design document linked or attached  
* \[x\] Partner (or substitute) has confirmed receipt in writing

**Project Partner Confirmation**

![][image1]

[image1]: <data:image/png;base64,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>