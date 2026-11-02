# RFC: [Unity Version Upgrade]

| Field | Value |
| --- | --- |
| Author | Zach Goldstein|
| Date | 2026-10-24 |
| Status | Proposed / Accepted / Rejected / Superseded |
| Decision Date | (fill when decided) |
| Deciders | (who agreed to the outcome) |
| Superseded By | (link, if applicable) |

## Problem and Context

My team is working on Capstone Project 019, PharmacyGo_v3. Our project contribution will be to iterate on an existing game made by a capstone group from last year. Designed as a study game for Pharmacy students, the game is inspired by the original set of Pokemon games. As such, it is top-down, has a pixel art style, and uses interactions and minigames like Pokemon battles to represent the things a “student” (player) has learned. The previous year group created a fully working v2 of the game, and our job is to add more to this existing base, such as new mini-games, new levels that focus on specific systems or regions in the body, etc.

The problem for this RFC is whether or not we should update the project to a more recent version of Unity. The version we are currently using was released almost exactly two years ago, and is no longer supported and has active security warnings.

## Options

Option 1. Move to the latest stable build of Unity.
Option 2. Move to the latest LTS (long-term support) build.
Option 3. Do nothing.

### Option A: Latest Stable Build

The latest stable build of Unity would be version 6.6. This option would involve upgrading our existing project to the latest stable build of Unity, using existing migration guides released by Unity.

This would add many new developer features and performance upgrades and fixes security advisories. The benefit of this would be that it would add the latest features available to developers, but the version will not receive support for very long, as new versions are always being released, so it isn’t a great choice for games that are close to release.

The cost to this would be time and effort on the part of our team, but there is an official migration guide for updating existing projects to newer versions. Due to the large number of versions that have been released between the version currently implemented and our target, it would require several in-between upgrades to “walk up” through all of the versions until our target stable build.

One downside to this would be that the stable build will not be supported for as long as an LTS would be. 

### Option B: Latest LTS Build

Similar to Option A, this would be an update to our Unity version to version 6.3, the latest long-term support build. 

While this would not include as many features, it is a more stable version that will receive official support from Unity for much longer, and as such it is recommended for projects that are nearing release. 

The cost would be similar to Option A, but would require less work as there are fewer versions between our current version and our target version of 6.3. 

### Option C: Do Nothing

If we choose to do nothing, we would stay on our current version of Unity. The benefits of this would be mainly in time and effort, as, by definition, we don’t have to do anything. This requires no additional work or planning on our part.

One large issue is that the version we are currently using, and any version released before October of 2025, has a severe security vulnerability according to the cve.org (Common Vulnerabilities and Exposures). They rate this vulnerability at a 7.4 out of 10 on their Common Vulnerability Severity Score which falls under the “high severity” category.

## Analysis

The technical tradeoffs that matter for this decision: performance, complexity, maintainability, security, cost. Argue from evidence (benchmarks, prototypes, documentation, prior art) and cite what you relied on so a reviewer can check it. Acknowledge what you are uncertain about.

The 6.3 LTS build would have the best maintainability, and is recommended for “developers who are about to lock in production” and “new and mid-cycle productions”. I feel as though our project falls into both of these two categories. The benefits of upgrading Unity versions include huge performance upgrades, such as: highly reduced memory usage, reduced build times (up to 50% reduction), and many other new features. Additionally, while it is unlikely to affect a project like ours, this update would eliminate the severe security vulnerability mentioned in Option C. While I am not sure as of yet how these new features would affect our project development, the build time, compile time, and memory usage, as well as the security patch are reason enough, in my opinion, to move forward with the update. 

Sources:
https://unity.com/security/sept-2025-01
https://www.cve.org/CVERecord?id=CVE-2025-59489
https://unity.com/blog/unity-6-3-lts-is-now-available

## Recommendation and Verification Plan

I recommend that we go with Option B. This version will add new features and improvements, fix potentially critical security flaws with our current version, and will still have support several years from now. No verification plan will be required, aside from checking for the correct version number and ensuring that our game still works.

## Constraints

As this is an entirely internal decision, it does not affect licensing, data use, IPs or confidentiality. The only constraints on this decision would be the cost in time for the project, as well as any broken features should we decide to upgrade to a newer version.

## Delegation and Validation

I did all of the work on this RFC myself, and did not use AI tools for any part of it.

## Decision

Proposed, seeking review. 
