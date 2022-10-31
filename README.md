**The University of Melbourne**
# COMP30019 – Graphics and Interaction
## Teamwork plan/summary
<!-- [[StartTeamworkPlan]] PLEASE LEAVE THIS LINE UNTOUCHED -->
<!-- Fill this section by Milestone 1 (see specification for details) -->
### Protocols and Plans
* Sprints will be held weekly, with the first starting on the 16th of September.
* Team :ok_hand: will be using GitHub's issues to create tickets for features, bugs, and other content required for this project.
* Tasks are to be delegated during the weekly meetings being held on Fridays, 4:00PM to 5:00PM.
* The previous week's work should be completed by the time the sprint is done (the following Friday), any bugs/blockers withstanding.
* Every feature should be developed in their own branch, with pull requests requiring reviews and approval from fellow team members.

Team communication is done via the team's discord server.

### Team Member Responsibilities

After brainstorming the most important aspects to start off our game, we have settled on two major categories which can be further broken down: Feels Good and Plays Good.

Feels Good is broken down into UI/UX, Custom Shaders, and Gameplay Mechanics/Ideas.

Plays Good is broken down into Character Movement and Level Generation.

Unassigned work is to be delegated and/or expanded upon every sprint. Each team member can help the other, but the work defined in the two categories are the team member's main responsibility.

**Benjamin Yi - Plays Good**
**Issues Worked On:**
* Object placement and controls
* Endless mode and procedural generation
* Character movement fine-tuning
* Fog of war shader with Sean

**Elizabeth Wong - Plays Good**
**Issues Worked On:** #4, #5, #12, #23, #28, #53, #62, #64, #69
* Character movement
* Advanced character movement: wall jump + wall slide
* Character camera and fine-tuning
* Timer and additional timer features
* Cel Shader with Caitlin

**Caitlin Grant - Feels Good**
**Issues Worked On:** #3, #6, #7, #12, #15, #33, #37, #38, #66, #67, #68
* UI - Main Menu, Sub Menus, Pause Menu, etc.
* Implement background music
* Implement kill box
* Editing of gameplay video (footage coming from team members)
* Cel Shader with Liz and integration of the fog of war shader
* Bloom and Glow shader

**Sean Maher - Feels Good**
**Issues Worked On:**
* Level Generation
* Level design for levels 1, 2, and 3
* Design of different blocks that appears in the levels (ice. mud, spikes)
* Helped with implementation of the kill box and linking of levels to the main menu
* Fog of war shader with Ben
* Implementation of particle effects for the Goal object

**Group Efforts**
A lot of collaboration has been done between the team members, with the use of VsCode's Live Share being integral to the development of the game. The entire team participated in how the game should look before recording the scenes used for the gameplay video and each member also participated in recruiting people to test out the game.

<!-- [[EndTeamworkPlan]] PLEASE LEAVE THIS LINE UNTOUCHED -->
## Final report
### Game Summary
_Super Hop_ is an exciting first-person 3D strategic running game. The aim of this game is to survive each timed level by making it to a goal point at the end of each map. In a race against time, you must use your allocated amount of blocks wisely to pave a path towards the end goal, whilst collecting coins to continue forging your way forwards and keep your timer from ticking to zero. Hop your way to victory with complex movement mechanics such as wall sliding and wall jumping! But beware, not all tiles you step on are as equal as others - one wrong block could send you plummeting to death. Currently playable in two modes: levelled and endless.
---
### Table of contents
* [Game Summary](#game-summary)
* [Technologies](#technologies)
* [How To Play](#how-to-play)
* [Gameplay Design](#gameplay-design)
	* [Design Decisions](#design-decisions)
	* [Procedural Generation](#procedural-generation)
* [Graphics](#graphics)
	* [Shader #1](#cel-shader)
	* [Shader #2](#glow-shader)
	* [Particle Effects](#particle-effects)
* [Evaluation and Feedback](#evaluation-and-feedback)
	* [Demographic](#demographic)
	* [Feedback Received](#feedback-received)
	* [Changes Made](#changes-made)
* [References/External Sources](#references-and-external-sources)
---
### Technologies

Project is created with:

* Unity 2022.1.9f1

### How To Play
There are two aims of this game as there are two modes. The levelled mode - levels 1 to 3 - serve as both instructive and playable levels where the goal is to safely get to the other side within the time limit by building blocks and jumping. In regards to movement mechanics there are two distinct features which enhance the player experience: wall jumping and wall-sliding.

On the other hand, the goal of endless mode is to simply stay alive for as long as possible while travelling forwards until the timer (located at the top of the screen) ends. Coins in both modes act as a currency to place blocks where 1 coin gives players the ability to place 1 block. Once the amount of coins stashed has been exhausted, the player will no longer be able to place blocks and more coins will need to be collected in order to continue using the build ability. Additionally, coins are not a valuable resource for players to exchange for building blocks, but also add 5 seconds of time per coin collected to your ticking timer.

In regards to ‘losing’ within a level, there are two ways where a player can fail to complete the game: falling off a platform and the timer reaching zero. Within levelled mode, a player has infinitely many attempts within that level to respawn and attempt to win before the timer ends. Conversely during endless mode, there is no way to ‘win’ as there is no goal state, however a player’s score will be calculated and displayed once a player dies within endless mode.

---
### Gameplay Design

#### Design Decisions

  

#### Procedural Generation

Procedural generation is used to create endless mode by generating random platforms, obstacles, and coins. The endless mode level is generated 20 platform lengths at a time, hiding unloaded sections behind an opaque wall and loading it when the player reaches 200 units within the wall, unloading the wall in the process.

To generate a section of the endless level, a Perlin noise generator is queried at regular intervals to create a continuous terrain of platforms. Then platforms are randomly deleted to create a corridor of spare platforms. This results in what appears to be random placements of platforms, but with close platforms at similar heights. The advantage of this over completely randomly placed platforms is it prevents close platforms having large vertical distances, which is difficult and frustrating in gameplay, while still allowing platforms far apart to be vertically separated - leading to emergent challenging gameplay that still feels fair and predictable.

Secondly, the platforms are mutated into ice and mud blocks according to a separate, 1D Perlin noise generator. This Perlin noise generator is queried along the depth of the level, and transforms the platforms into ice and mud blocks depending on what range the Perlin noise generator returns. For example, platforms are transformed into ice blocks if a value between 0.0 and 0.3 is generated. The advantage of this over random transformations is that “biomes” of ice and mud can be found naturally in the endless mode, where the player will alternate between sections of differing environments. This avoids having single blocks of ice and mud interspersed with each other, which not only provides little game play value, but also does not fit with the game’s visual themes.

Finally, spikes and coins are added at random. The starting area is guaranteed to be a safe zone to begin in, and checkpoints areas consisting of safe platforms appear at fixed intervals to give a sense of progression to the player.

The four major steps (full terrain, deleting platforms, mutating environment, adding coins) are shown in the following image:

<p align="center">
  <img src="Gifs/ProceduralGeneration.gif" width="300">
</p>
[https://imgur.com/a/8uVcGLi](https://imgur.com/a/8uVcGLi)

---
### Graphics
To preface all the assets used, the original idea for Super Hop involved a simple, clean look. This was furthered when choosing the perspective the player would be in. The decision of using first-person perspective made several sources of inspiration apparent - the VR game Beat Saber and movies such as Tron and Blade Runner with their synthwave, outrun aesthetic synergized well with our goals.

<p align="center">
  <img src="Assets/Assets - menu/Background1920x1080.png" width="300">
</p>

Very little assets are taken from the assets store in Unity such as the ramp, column, and car which can be placed down. We had created the spikes in Blender for the spike trap prefab due to the lack of a cone object in Unity. Most of the objects used come from Unity out of the box.

When discussing if we should use more assets from the unity store, we concluded that the emphasis should be on the shader applied to the object, and that it would not be productive to look for premade assets which fit the aesthetic we are hoping to achieve. The decisions made are the results of tradeoffs and balances, such as the amount of attention provided to a limited set of models as opposed to quickly working on a large variety.

Given the way levels are generated in Super Hop, all of the rendered objects in a level, with the exception of the player, are generated from prefabs. This can introduce extra costs by considering the way shaders are used in Unity (attached to materials) and how the same object is required to be duplicated for variance in design. Despite this repetition, distinguishable entities are created with the custom shaders through the use of colour.

When considering on what sort of shader(s) we would implement, we looked at the key characteristics of both the synth-wave and outrun art style.

The characteristics we focused on are as such:

*  80s-centric
* Sunset graphics
* Neon grids
* Neon lights
* Wireframe vector graphics
* Retro-futurism

This led us to the creation of the following custom shaders: cel shader, bloom shader, and glow shader. 
These can be found in `./Assets/Shaders` and out of the three, the _cel shader_ and the _glow shader_ are the two which are to be evaluated.

These shaders enhance the visuals of the game by being the visuals, working in conjunction with one another. Without the effects provided in these shaders, we would not be able to achieve the characteristics which are iconic to the inspirations we draw from. Furthermore, cel shading aids our goal of clean and simple. Cel shading typically does not utilize textures, and textures can add a layer of visual information which can overload the player. Regarding the glow and bloom effect, the glow enhances the neon colours, whereas the bloom helps intensify the brightness of the rendered frame.

Super Hop uses Unity’s Built-in Render Pipeline with the forward rendering path. The main focus of these custom shaders is in regard to the drawing and post-processing operations.

#### Cel Shader
```
-Shader
./Assets/Shaders/CelShader.shader

Shader file to be evaluated: ./Assets/Shaders/CelShader.shader
```

Occurring during the drawing process, the cel shader provides a variety of effects made in three passes.

  

These effects are:
* Object Outline
* Diffuse and Specular Light (including posterization)
* Fog
* Inner Glow

In the first pass, all the effects besides the object outline is applied. The steps can be simplified as such:

1.  Diffuse Lighting
2.  Specular Lighting
3.  Inner Glow
4.  Composite the results of 1 to 3.
5.  Apply fog (based on the distance from the player to the object)

In the second pass the object outline is created with the fog also being calculated to ensure the outline does not stand out in the fog. This pass has limitations as the outline can affect other objects, which may produce some visual bugs for the player.

In the third pass the shadow of the object is cast to the scene.

#### Glow Shader
```
– Shader
./Assets/Shaders/CameraGlowShader.shader
→ Used on the camera with GlowPostProcess and GlowCameraMat.mat
./Assets/Shaders/CmdDepthShader.shader
→ Used with GlowObj.cs and GlowObjMat.mat

– Scripts
./Assets/Shaders/GlowObj.cs
./Assets/Shaders/GlowPostProcess.cs
./Assets/Shaders/GlowRender.cs

– Materials
./Assets/Shaders/GlowCameraMat.mat
./Assets/Shaders/GlowObjMat.mat

Shader file to be evaluated: ./Assets/Shaders/CameraGlowShader.shader
```

  

This glow is a post-processing effect that works in two stages of the rendering journey. Firstly, a command buffer is made to run the CmdDepthShader.shader before image effects are applied. Chosen objects in the scene have the GlowObj.cs script attached to them and are then added into the set of Glow Objects in GlowRender.cs. From the GlowRender.cs script the selected object is drawn onto a black Render Texture (RT) in the chosen glow colour. This process involves creating a depth map to see if there is something blocking the object. If so, then the parts which are hidden are not drawn to the RT.

Once the RT is completed, the second stage of the process begins. The RT from the first stage becomes a global texture to be accessed in the GlowPostProcess.cs script. This script which is attached to the camera then grabs a copy of the scene before glow is applied, then downsamples and upsamples the newly made global texture with a box blur. That RT is then applied to the copy of the final scene and blitted into the destination RT, which is what is displayed on the player’s screen.

Further improvements which could be made on this effect is the way the blur is handled, as well as allowing more customization for the colour and strength of the glow.

It should be noted that the bloom effect acts in a similar way to the glow effect. The bloom shader downsamples, upsamples, and uses a box blur. The key difference between the two is that the bloom shader acts on the entire scene and only makes changes to the pixel based on if the red colour value is above a certain threshold.

#### Particle Effects

### Evaluation and Feedback

#### Demographics

#### Feedback Received

#### Changes Made

### References and External Sources