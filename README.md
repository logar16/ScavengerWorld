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

## TODOs
Reorganize to be
Console App, ScavengerWorld class library, ScavengerPlayerAI class library, external client server

So when launching Console App, it uses the ScavengerWorld and the default ScavengerPlayerAI libraries to run the thing.
If using the external client server, we will still use ScavengerWorld to run the simulation but will depend on remote client for the player AI.

1. Test with basic hand-crafted AI implementations
2. Test printout to visualize
	- Would be nice to have colored units based on team (low priority).
3. Write unit tests for
    - JsonWorldBuilder
    - Individual Units
    - Food and Items (as needed)
    - Geography
    - Ambiance
    - WorldState
    - FullWorld
    - Simulator
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