# 03 · AR Painting Gallery

Augmented reality class exercise (June 2025). The app detects floors and walls through the phone camera. When you tap a detected surface, it places a random framed picture there, aligned to that surface.

## How it works

1. **AR Foundation** detects horizontal and vertical planes.
2. When you tap the screen, it raycasts against the detected planes.
3. A random prefab from the `paints` list is placed at the hit pose, aligned to the plane.
4. Plane detection then turns off and the plane visuals are hidden.

## Scripts (`Assets/Scripts`)

| Script | What it does |
|---|---|
| `PlanePlacer` | Plane detection setup, touch raycast and painting placement |

## Running it

- Target: **Android** with ARCore support. Switch the platform in *File → Build Profiles*.
- Build and run on a device. AR doesn't work inside the Editor Game view.

## Tech

- Unity **6000.0.43f1**, Universal Render Pipeline (URP)
- AR Foundation 6.0.6, Google ARCore XR Plugin 6.0.6

## Third-party assets

- *Painting Collection* models (`Assets/Paintings`): Unity Asset Store
