

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
* Documentation for this project will be written in Markdown and located in the /docs folder, as there is no Wiki available for this type of repository.

Team communication is done via the team's discord server.

### Team Member Responsibilties
After brainstorming the most important aspects to start off our game, we have settled on two major categories which can be further broken down: Feels Good and Plays Good.

Feels Good is broken down into UI/UX, Custom Shaders, and Gameplay Mechanics/Ideas.

Plays Good is broken down into Character Movement and Level Generation.

Currently, we have assigned the following down below to each team member. Unassigned work is to be delegated and/or expanded upon every sprint.  Each team member can help the other, but what is listed below is their main responsibility.

**Caitlin Grant - Feels Good**
- UI -> Start menu, Shop, Pause, Settings, Exit Menu, Resolution
- Additional game mechanics

**Elizabeth Wong - Plays Good**
- Character Movement (Running, Jumping, etc.)

**Benjamin Yi - Plays Good**
- Object placement and controls

**Sean Maher - Feels Good**
- Level Design -> How each level is laid out
- Additional game mechanics

**Unassigned AND/OR Potential Work**
- Saving and loading files
- Game currency
- Powerups
- Endless Mode
- Custom Shaders 1 & 2
- Procedural Generation
- Storage Format

<!-- [[EndTeamworkPlan]] PLEASE LEAVE THIS LINE UNTOUCHED -->

## Final report

Read the specification for details on what needs to be covered in this report... 

Remember that _"this document"_ should be `well written` and formatted **appropriately**. 
Below are examples of markdown features available on GitHub that might be useful in your report. 
For more details you can find a guide [here](https://docs.github.com/en/github/writing-on-github).

### Table of contents
* [Game Summary](#game-summary)
* [Technologies](#technologies)
* [Using Images](#using-images)
* [Code Snipets](#code-snippets)

### Game Summary
_Super Hop_ is an exciting first-person 3D strategic running game. The aim of this game is to survive each timed level by making it to a goal point at the end of each map. In a race against time, you must use your allocated amount of blocks wisely to pave a path towards the end goal, whilst collecting coins to continue forging your way forwards and keep your timer from ticking to zero. Hop your way to victory with complex movement mechanics such as wall sliding and wall jumping! But beware, not all tiles you step on are as equal as others - one wrong block could send you plummeting to death. Currently playable in two modes: levelled and endless..

### Technologies
Project is created with:
* Unity 2022.1.9f1 
* Ipsum version: 2.33
* Ament library version: 999

### Using Images

You can include images/gifs by adding them to a folder in your repo, currently `Gifs/*`:

<p align="center">
  <img src="Gifs/sample.gif" width="300">
</p>

To create a gif from a video you can follow this [link](https://ezgif.com/video-to-gif/ezgif-6-55f4b3b086d4.mov).

### Code Snippets 

You may wish to include code snippets, but be sure to explain them properly, and don't go overboard copying
every line of code in your project!

```c#
public class CameraController : MonoBehaviour
{
    void Start ()
    {
        // Do something...
    }
}
```
