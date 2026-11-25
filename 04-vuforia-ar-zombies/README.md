# 04 · Vuforia AR Zombies

Augmented reality class exercise (June 2025). Point the camera at a Brazilian banknote (R$ 2 or R$ 10) to make a knight appear on top of it. Move the knight with an on-screen joystick and attack the zombies around him.

## Gameplay

- Vuforia **image targets** (`Assets/StreamingAssets/Vuforia`) anchor the scene to the banknote.
- The knight walks with a virtual joystick and attacks any zombie he touches.
- Defeated zombies play a fall animation and disappear.

## Scripts (`Assets/Scripts`)

| Script | What it does |
|---|---|
| `PlayerController` | Joystick movement, walk/idle/attack animations, zombie hit and removal |

## Setup

1. **Vuforia license key.** The key was removed from this repo. Create a free *Basic* key at [developer.vuforia.com](https://developer.vuforia.com/), then paste it in *Assets/Resources/VuforiaConfiguration → App License Key*.
2. The Vuforia Engine package is stored locally in `Packages/com.ptc.vuforia.engine-11.2.4.tgz`. It's tracked with Git LFS, so run `git lfs pull` after cloning.
3. Build for Android or iOS, or test with a webcam in Play mode.

## Tech

- Unity **6000.0.45f1**, Universal Render Pipeline (URP)
- Vuforia Engine 11.2.4, Input System

## Third-party assets

- *Joystick Pack* by Fenerax Studios: Unity Asset Store
- Knight and zombie characters (`cha_data`, `Assets_Zombie`): Unity Asset Store
- Vuforia sample targets and Vuforia Engine: PTC
