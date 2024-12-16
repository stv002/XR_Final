# Mr. Watts: VR Electrical Circuits

## Project Description and Features

**Mr. Watts** allows users to engage with various physics-based electrical components in VR. Though functionality is currently limited, users can experience the base concept of what would ideally be an interactive puzzle game based on building and experimenting with electrical circuits.

**Key Features:**
- **Interactive Components:** Physically interact with wires, electrical components, and batteries.
- **VR Experience:** Designed for VR and XR controllers.
- **Environment:** Set in a fenced-in garage in a forest, offering immersive surroundings.

**Game Examples:**
![plot](imgs/img1.jpeg)
![plot](imgs/img2.jpeg)
![plot](imgs/img3.jpeg)

## Hardware/Software Requirements

**Hardware:**
- An OpenXR complient VR headset (it has only been tested with the Meta Quest 3)
- OpenXR complient Controllers

**Software:**
- Unity 3D (version 2022.3.45f)
- Git & Git LFS for cloning

## Installation Instructions

1. Clone the repository from GitHub with Git LFS:
```
git lfs clone https://github.com/stv002/XR_Final.git
```

## Build / Run Instructions

1. Open the cloned project in Unity.
2. Open the scene **Assets/Scenes/XR_FINAL_DEMO.unity**
3. Install the package located at: **Assets/External Assets/Magic Lightmap Switcher/Support Packages/URP/URP 14 Support.unitypackage**
4. Go to **Tools --> Magic Lightmap Switcher --> Prepare Shaders**, and click **Patch Shaders**
5. Now, once you enter play mode, the correct lightmap will be loaded.
#### Mac + Non Quest Users
1. Navigate to File --> Build Settings, and select **Android**.
2. Select your VR device as the **run device** (ex. Oculus Quest 3).
3. Click **Build**, then transfer the .apk to your device using any installation tool, or instead click **Build and Run**.
#### Windows WITH Meta Quest
Windows users can either follow the same steps as Mac, or instead of building:
1. Navigate to File --> Build Settings, and select **Windows/Mac/Linux**.
2. Select **Switch Platform**
3. Connect your VR headset to your PC via **Meta Quest Link**
4. Simply enter play mode!

## Known Issues/Limitations

- **Lighting Bleed:** Outside lighting bleeds through some edges of the garage mesh.
- **Lighting Inconsistencies:** When built to android, surrounding forest sometimes appears completely white, along with more minor inconsistencies with per-object lightmaps in the garage.
- **Circuit Updates:** Components' powered state does not update until a direct interaction with its own local sockets.
- **Wire Physics Stability:** It is extremely easy to cause eradic behavior in the wires by either over-stretching them or causing many different collisions with the segments.
- **Performance:** Further lighting optimization is needed.

## Future Development Possibilities

- Implement a nail gun tool for attaching components to surfaces.
- Address lighting and performance issues for smoother gameplay.
- Implement many new electrical components.
- Create an array of puzzles for the player to work through.
- Introduce new scenes such as a warehouse or a factory.

## Video Demonstration

Watch a demo of Mr. Watts in action: [Video URL](<https://youtu.be/cOnxxGZeZco>)

---
