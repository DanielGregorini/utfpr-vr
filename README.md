# UTFPR · Virtual & Augmented Reality

Unity projects I built for the Virtual Reality course at **UTFPR** (Universidade Tecnológica Federal do Paraná) in 2025. They range from small class exercises to the two term projects.

| # | Project | Type | Highlights |
|---|---|---|---|
| 01 | [Popcorn Shooter](01-popcorn-shooter) | Arcade 3D, class exercise | Shooting, score, physics-based traps |
| 02 | [Zombie Survival FPS](02-zombie-survival-fps) | FPS, **1st term project** | NavMesh zombies, gun and grenades, health/kill HUD |
| 03 | [AR Painting Gallery](03-ar-painting-gallery) | AR (AR Foundation / ARCore) | Plane detection, tap-to-place paintings on walls |
| 04 | [Vuforia AR Zombies](04-vuforia-ar-zombies) | AR (Vuforia) | Image targets (banknotes), virtual joystick |
| 05 | [Horror Flashlight Escape](05-horror-flashlight-escape) | First-person horror, **2nd term project** | Monster AI with line of sight, flashlight mechanic |

Each folder is a standalone Unity project with its own README.

## Opening a project

1. Install **Git LFS** before cloning, because models, textures and audio are stored with LFS:
   ```bash
   git lfs install
   git clone <repo-url>
   ```
2. In **Unity Hub**, choose *Add → Add project from disk* and select one of the numbered folders.
3. Use **Unity 6** (`6000.0.43f1`, or `6000.0.45f1` for project 04). Unity rebuilds the `Library` cache on first open, which takes a few minutes.

## Credits

The code, scenes and gameplay are my own. The 3D models, textures, skyboxes and some sounds come from the Unity Asset Store and other third parties, credited in each project's README. They are included for educational purposes only and remain under their original licenses.
