# 05 · Horror Flashlight Escape

Second-term project (July 2025). A first-person horror game set in a dark, abandoned port. Collect 5 coins before the monster catches you. Your flashlight helps you see, but it also lets the monster spot you from much farther away.

## Gameplay

- Scenes: `MenuInicial` (main menu) → `Jogo` (game).
- Collect **5 coins** to win.
- If the monster touches you, you die and go back to the menu.
- The monster **patrols** near you. It starts chasing when:
  - it can see you (raycast, up to 15 m),
  - your flashlight is on and you're within 30 m, or
  - you're closer than 7 m.
- It loses track of you after 5 s without seeing you. Sound gets louder when it's close.

| Key | Action |
|---|---|
| `WASD` | Move |
| Mouse | Look around |
| `Space` | Jump |
| `F` | Toggle flashlight |
| `Esc` / `P` | Pause menu |

## Scripts (`Assets/Script`)

| Script | What it does |
|---|---|
| `PlayerController` | Rigidbody movement, mouse look, coins, win/death handling |
| `MonsterController` | NavMesh patrol and chase, line-of-sight check, rage and footstep sounds |
| `FlashligtController` | Turns the flashlight on and off |
| `MenuInicioController`, `MenuPauseController` | Main and pause menus |

## Tech

- Unity **6000.0.43f1**, Built-in Render Pipeline with Post Processing
- AI Navigation (NavMesh), Terrain Tools

## Third-party assets

Unity Asset Store packs used in the scene:

- *Old Sea Port*
- *Monster Scavenger* (`monster_scavenger`)
- *Flashlight*
- *Rusty Vehicles*
- *AllSky Free* skyboxes
- *Stylized Spruce Tree*, *Tree9*
- *Terrain Sample Asset Pack*: Unity Technologies
- *Pandazole Ultimate Pack*, *Piloto Studio* effects
