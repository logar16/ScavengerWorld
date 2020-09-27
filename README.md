# Scavenger World
This is the Server simulator which should be setting up games, er simulations, 
and playing them.  The simulation is described in more detail in this [Google Doc](https://docs.google.com/document/d/14XEu3WsUuBcH3lxiHeGoCCMesSdKM1msx8qH6TlIveY/edit?usp=sharing).
I chose to do the simulation part in C# so it can take advantage of type safety and speed.
I will probably do the learning agents in other languages for their tools.


## Units

### Actions

Units can take the following actions

| Actions | Additional Input                                                    |
| ------- | ------------------------------------------------------------------- |
| No-op   |                                                                     |
| Move    | Cardinal Direction                                                  |
| Take    | GUID of object (item or food)                                       |
| Create  | int index of item to create and optional data defining object       |
| Drop    | GUID of object to drop                                              |
| Give    | GUID of object to give and GUID of receiver (unit or storage)       |
| Attack  | GUID of object to attack                                            |
| Display | Indicate the `SensoryFeature`s that should change and their values  |

A note on giving, it can be used to give to units that don't necessarily want the item 
(I don't have a good mechanism for them deciding if they want the item in that same "turn").  This could be fun if units give bad gifts (items that do damage to the wielder or low-grade food)
Giving is also how the unit can drop off food into the food storage area.  Giving when there is nothing to give to results in a drop.

If the unit is holding an item, the item can influence whatever action the unit takes for better or for worse.

Some units can create markers (like pheremones) which can be dropped and observed by others.  Units could presummably create several types of markers which can be interpreted in different ways by its teammates or enemies.  The actual communication is up to interpretation.

In the case that a unit can create other things (let's say land mine or a bomb or something else useful), that will be done with the same `create` command, but with a different ID and with other data given for its construction.

## Ideas

### Sensory Displays
For the agent making decisions based on the data, consider breaking the different sensory data into a 3D map--grid of surrounding area for 2D and the other dimension is the sensory input.  This should allow the agent to learn patterns based on how it sees or how it hears, etc. without having to treat them as the exact same type of stimulus.

Note: Stimulus will initially just indicate the exact location of the emitter (if display had sufficient strength the reach the unit).  Maybe someday will treat smell and sight differently as they work very differently in reality. 

### Simulation vs. Decision-making vs. Rendering
I have found that C# is great for full on simulations, because its type-safety helps me avoid nasty little errors that would kill me in Python/JavaScript.  However, the UI tools aren't great, so I think I will write some rendering code in JavaScript which will take in the `WorldState` as a JSON and render it all pretty-like.  Finally, I am much more familiar with the ML tools in Python, so I will probably stick to those, although this may be an opportunity to look into ML tools for C# or F#.


## TODOs
Reorganize to be
Console App, ScavengerWorld class library, ScavengerPlayerAI class library, external client server

So when launching Console App, it uses the ScavengerWorld and the default ScavengerPlayerAI libraries to run the thing.
If using the external client server, we will still use ScavengerWorld to run the simulation but will depend on remote client for the player AI.

1. Integ Test with basic, hand-crafted AI implementations
2. Test printout to visualize
	- Would be nice to have colored units based on team (low priority).
3. Write retroactive unit tests for
    - JsonWorldBuilder
    - Individual Units
    - Food and Items (as needed)
    - Geography
    - Ambiance
    - WorldState
    - FullWorld
4. `SensoryDisplay` and how distance affects it
	- Keep in mind how this will be communicated to agents
5. Tune configuration for Unit attributes
6. Configuration and creation for items (low-priority)
7.  Finish mimicing the OpenAI gym interface (low-priority)
8. Communication Protocol/API
	- Should external clients request certain units or the whole world state?
9. Write learning code elsewhere that can decide updates
	- Could have multiple sources that are each in charge of a particular team?
10. Simulator to await input from "external" agent.  May be a local implementation or a remote client written in another language
11. Write rendering code elsewhere to make for a more fun game to watch/interact with
    - Probably do it in React or Angular to take advantage of great UI tools
12. Eventually optimize (after everything else is in place and if it turns out to be slow)