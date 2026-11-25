# 02 · Zombie Survival FPS

First-term project (June 2025). A first-person shooter where zombies keep spawning and chase you across a terrain. Shoot them, throw grenades, and survive as long as you can.

## Gameplay

- Scenes: `MenuIniciar` (main menu) → `JogoZombie` (game).
- Zombies spawn every second and track you with a **NavMesh agent**.
- Each zombie hit costs you 20 HP (with a 1 s cooldown). At 0 HP you go back to the menu.
- The HUD shows your health and how many zombies you've killed.

| Key | Action |
|---|---|
| `WASD` | Move |
| Mouse | Look around |
| `Space` | Jump |
| Left click | Shoot |
| `G` | Throw grenade (explodes after 3 s with a red flash) |
| `Esc` / `P` | Pause menu |

## Scripts (`Assets/Script`)

| Script | What it does |
|---|---|
| `PlayerController` | `CharacterController` movement, mouse look, health, damage cooldown |
| `GunController` | Bullet and grenade spawning |
| `GrenadeController` | Delayed explosion: grows its collider, plays audio, red light flash |
| `ZombieController` | NavMesh chase, death animation, updates the kill counter |
| `SpawnZombieController` | Spawns a zombie prefab on a fixed interval |
| `KillContController` | Kill counter shown with TextMesh Pro |
| `MenuController`, `MenuPauseController` | Main and pause menus |

## Tech

- Unity **6000.0.43f1**, Universal Render Pipeline (URP)
- AI Navigation (NavMesh), Terrain Tools, TextMesh Pro

## Third-party assets

Unity Asset Store packs used in the scene:

- *Terrain Sample Asset Pack* (`TerrainSampleAssets`): Unity Technologies
- *4K 3D Weapons Mega Pack*
- *Zombie*
- *Polygon Trees* (`polygonTrees`)
- *Infinite Dawn* skybox

The menu music (`Assets/Audio`) belongs to its original rights holders and is used here for educational purposes only.
