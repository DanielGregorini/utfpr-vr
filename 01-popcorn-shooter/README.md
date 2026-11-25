# 01 · Popcorn Shooter

A small arcade arena game made in class (April 2025). You play a popcorn character who shoots enemies, scores points and dodges traps that drop cubes, launch balls and fire spikes around the arena.

## Gameplay

- Start from the main menu (`MenuIniciar`) and play in `SampleScene`.
- Each enemy you hit adds a point and drops an ice cream somewhere in the arena.
- Touching a trap or an enemy kills you and sends you back to the menu.

| Key | Action |
|---|---|
| `W` / `S` | Move forward / back |
| `A` / `D` | Turn |
| `Space` | Shoot |
| `Esc` / `P` | Pause menu (restart / main menu) |

## Scripts (`Assets/Script`)

| Script | What it does |
|---|---|
| `PopcornController` | Tank-style movement, shooting, and death on contact with traps or enemies |
| `BulletController` | Bullet hit detection, score counter, spawns an ice cream (`Resources/iceCream`) |
| `TrapTriggerController` | Drops 7 cubes from the sky when the player steps on it |
| `trapBallController` | Launches 10 cubes horizontally with a strong impulse |
| `trapTriggerEspinhosController` | Fires spikes in random directions around the trap |
| `MenuPauseScript` | Pause / resume / restart / back to menu |
| `StartGame`, `quitGame` | Main menu buttons |

## Tech

- Unity **6000.0.43f1**, Universal Render Pipeline (URP)
- Cinemachine, Input System, TextMesh Pro

## Third-party assets

- *Supercyan Character Pack Free Sample*: Unity Asset Store
- *Starter Assets* (`Assets/UnityTechnologies`): Unity Technologies, Unity Companion License
