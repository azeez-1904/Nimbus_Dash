# Nimbus Dash

A Dragon Ball Z-themed Flappy Bird game built in Unity! Fly Goku on his Nimbus cloud (Kinto-Un) through rocky pillars and see how far you can go.

![Unity](https://img.shields.io/badge/Unity-2022.3-blue) ![License](https://img.shields.io/badge/License-MIT-green)

## Gameplay

- **Fly** through gaps between rocky stone pillars
- **Score** points for each pillar you pass
- **Survive** as the game gets harder over time
- **Beat** your high score — it saves between sessions!

### Controls

| Input | Action |
|-------|--------|
| **Space** / **Left Click** / **Touch** | Flap (fly upward) |
| **Escape** | Pause / Unpause |
| **Space** (while paused) | Resume game |

## Features

- **DBZ Theme** — Goku on his Nimbus cloud dodging rocky pillars
- **Progressive Difficulty** — pipes speed up and spawn faster as your score increases
- **Difficulty Milestones** — noticeable jumps at score 10, 25, and 50
- **Persistent High Score** — your best score is saved locally
- **Screen Shake** — camera shake on death for impact
- **Bird Rotation** — Goku tilts up when flapping and nose-dives when falling
- **Pause System** — pause anytime with Escape, resume with Space or Escape
- **Start Screen** — game waits for your first input before starting
- **Mouse & Touch Support** — play with keyboard, mouse, or touchscreen

## Project Structure

```
Assets/
├── goku_phys.cs              # Player controller (flap, rotation, input)
├── Pipe_Script.cs            # Pipe/pillar movement and cleanup
├── pipe_spawner_script.cs    # Spawns pillars with increasing difficulty
├── Middle_script.cs          # Score trigger between pillar gaps
├── Logic_Script.cs           # Game logic (score, high score, pause, game over)
├── PipeGapAdjuster.cs        # Adjusts gap between top/bottom pillars
├── BackgroundScroller.cs     # Scrolling background effect
├── pipes.prefab              # Pipe/pillar prefab
├── goku.png                  # Goku sprite
├── rocky-pillar.png          # Rocky pillar sprite
└── Cloud.png                 # Cloud sprite
```

## How to Clone and Make Your Own Version

### Prerequisites

- [Unity Hub](https://unity.com/download) installed
- **Unity 2022.3.62f1** (or compatible 2022.3.x version)

### Steps

1. **Clone the repo**
   ```bash
   git clone https://github.com/azeez-1904/Nimbus_Dash.git
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Click **Open** → navigate to the cloned folder → select it
   - If prompted about a different Unity version, choose **Continue** (any 2022.3.x should work)

3. **Open the scene**
   - In the Project window, go to `Assets/`
   - Double-click the scene file to open it

4. **Hit Play** to test!

### Making It Your Own

Here are some ideas to customize:

- **Change the character** — Replace `goku.png` with your own sprite
- **Change the pillars** — Replace `rocky-pillar.png` with any obstacle sprite
- **Adjust difficulty** — Select the Pipe Spawner in the Hierarchy and tweak the speed/spawn rate values in the Inspector
- **Change the background** — Swap the background sprite or color
- **Add sounds** — Add AudioSource components and play clips on flap, score, and death
- **Add animations** — Create sprite animations for the character
- **Change controls** — Edit `goku_phys.cs` to add your own input keys

### Building the Game

1. Go to **File → Build Settings**
2. Click **Add Open Scenes**
3. Select your platform (PC, Android, WebGL, etc.)
4. Click **Build**
5. Share the output folder with friends!

## Credits

- Built with [Unity](https://unity.com/)
- Inspired by Flappy Bird by Dong Nguyen
- DBZ characters and concepts belong to Akira Toriyama / Toei Animation

## License

This project is for educational and personal use. Feel free to clone, modify, and learn from it!
