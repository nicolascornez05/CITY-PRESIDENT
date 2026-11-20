# Unity project setup

## Engine
- Unity 6.3 LTS baseline: 6000.3.0f1
- Universal Render Pipeline package 17.3.0
- Android target, landscape orientation planned for the first prototype
- Offline single-player; no server or subscription dependency

## First prototype
The first scene is `Assets/Scenes/Main.unity`. The runtime bootstrap creates a provisional 3D country map, city markers, a basic national dashboard and simulation speed controls.

## Open the project
1. Install Unity Hub.
2. Install Unity Editor 6000.3.0f1 with Android Build Support, Android SDK & NDK Tools, and OpenJDK.
3. Clone this repository and open the repository root in Unity Hub.
4. Let Unity import packages and compile scripts.
5. Open `Assets/Scenes/Main.unity` and press Play.

## Important validation
This scaffold has been written to GitHub but has not yet been imported or compiled by the Unity Editor. The first Unity import may reveal serialization or API issues that must be corrected before an APK can be considered buildable.
