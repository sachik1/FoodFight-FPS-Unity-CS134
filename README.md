# 🍽️ Kitchen FPS – First Person Shooter Game (Unity)

## Project Overview

Kitchen FPS is a first-person shooter game set in a stylized, oversized kitchen environment. The player navigates the space from a miniature perspective, where everyday kitchen objects become large-scale obstacles and interactive elements. The objective is to explore the environment and shoot targets (in the form of M&Ms) placed throughout the scene.

<img width="1468" height="717" alt="image" src="https://github.com/user-attachments/assets/29b531e7-cff1-4ce2-95b5-a9ab931ad0fe" />

<img width="1469" height="729" alt="image" src="https://github.com/user-attachments/assets/90959d71-c533-412f-a66d-8ea10bb25ccb" />

---

## Core Features

* First-person movement and camera system
* Shooting mechanic with sound effects
* Target-based interaction system
* Fully designed kitchen environment
* Realistic lighting and visual effects for immersion

---

# Technical Requirements Implementation

## Audio

The project demonstrates advanced use of Unity’s audio system:

* **3D Spatial Audio**: Environmental sounds (e.g., halo sound effect around target) change based on player distance using spatial blending and rolloff settings
* **Sound Effects**: Gunshot and interaction sounds provide gameplay feedback
* **Background Music**: Background music to make the game more fun and light
---

## VFX (Visual Effects)

Visual effects were implemented using Unity’s Particle System:

* **Water Simulation**: Particle system used to simulate flowing faucet water
* **Dynamic Visual Feedback**: Effects respond to environmental interactions
* **Shooting Effects**: Smoke bursts when player shoots
* Particle properties such as lifetime, velocity, and emission were tuned to create realistic motion

---

## UI (User Interface)

The game includes a structured user interface system:

* **HUD (Heads-Up Display)**: Displays gameplay information (e.g., time, crosshair)
* **Menu System**: Organized UI elements using Unity Canvas
* **Feedback Elements**: UI responds to player actions (e.g., button click, shooting interaction)
* Layout and hierarchy are structured for clarity and usability

---

## Animations

The project incorporates animation systems:

* **Triggered Animations**: Primarily occur in the game when the gun is shot (animation of that) + animation of crosshair
* Animation transitions enhance realism and responsiveness

---

## Shaders & Materials

Advanced material usage enhances visual quality:

* **Custom Materials**: Used for objects like glowing targets, stylized props, overall enviornment
* **Emission Effects**: Materials with emission properties create glowing visuals
* Material tuning (smoothness, metallic, color) improves realism and style
* **Imported Materials and assets**: to increase realism and create obstacles (fruit, food, etc.)

---

## Lighting

* **Real-Time Lighting**: Used for dynamic interaction (e.g., pendant lights, soft-kitchen lighting)
* **Shadows**: Soft shadows used to enhance depth
* **Post-Processing**:
  * Bloom for glow effects
  * Exposure for visual clarity
* Lighting is used to guide player attention and highlight gameplay areas
  
---

## Design Concept

The project explores scale and immersion by placing the player in a miniature role within a realistic kitchen environment. Lighting, audio, and environmental design work together to create a believable and engaging gameplay experience.

---

## Technologies Used

* Unity (Universal Render Pipeline – URP)
* C# scripting
* Unity Particle System
* Unity Audio System (3D spatial sound)

---

## Project Structure

```text
Assets/
   Scripts/
   Scenes/
   Materials/
   Prefabs/

Lighting/
   Pendant lights
   Under-cabinet lights
   Reflection & light probes

Gameplay/
   Player
   Camera
   Targets
   GameManager

Environment/
   Kitchen assets

UI/
   Canvas
   HUD elements
```

---

## Challenges & Learning Outcomes

* Implementing realistic multi-source lighting in a 3D environment
* Designing spatial audio systems tied to player movement
* Integrating multiple systems (UI, audio, lighting, VFX) cohesively
* Managing Unity Version Control system

---

## Future Improvements

* Expand UI with full menu navigation during game
* Improve animations and object interactivity
* Add more dynamic gameplay elements and levels
* Add more props and realisitc kitchen elements (create a messier kitchen for harder navigation)

---

## Contributors

* Sachi Kelkar
* Owen Hall

---

## How to Run

1. Open the project in Unity Hub
2. Load the main scene (called SampleScene)
3. Press **Play** to start
4. Use WASD or arrows to move player, and mousepad to rotate perspective
5. Press to shoot

---
