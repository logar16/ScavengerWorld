# Scavenger World
This is the Server simulator which should be setting up games, er simulations, 
and playing them.  The simulation is described in more detail in this [Google Doc](https://docs.google.com/document/d/14XEu3WsUuBcH3lxiHeGoCCMesSdKM1msx8qH6TlIveY/edit?usp=sharing).
I chose to do the simulation part in C# so it can take advantage of type safety and speed.
I will probably do the learning agents in other languages for their tools.


## Units

### Actions

Units can take the following actions

| Actions | Additional Input                                              |
|---------|---------------------------------------------------------------|
| No-op   |                                                               |
| Move    | Cardinal Direction                                            |
| Take    | GUID of object (item or food)                                 |
| Drop    | GUID of object to drop                                        |
| Give    | GUID of object to give and GUID of receiver (unit or storage) |
| Attack  | GUID of object to attack                                      |

A note on giving, it can be used to give to units that don't necessarily want the item 
(I don't have a good mechanism for them deciding if they want the item in that same "turn").  This could be fun if units give bad gifts (items that do damage to the wielder or low-grade food)
Giving is also how the unit can drop off food into the food storage area.  Giving when there is nothing to give to results in a drop.

If the unit is holding an item, the item can influence whatever action the unit takes for better or for worse.

## TODOs

1. Add `Takeable` interface which applies to `Food` and `Item` objects.
	* Have an option to point to the GUID of the current holder or test if there is a holder, so we don't accidentally share an object
1. Test printout to visualize
	- Currently works but it would be nice to have more detailed visualization to improve understandability
2. Write tests for
    - Individual Units
    - Food and Items (as needed)
    - Geography
    - Ambiance
    - WorldState
    - FullWorld
    - Simulator
3. `SensoryDisplay` and how distance affects it
	- Keep in mind how this will be communicated to agents
1. Tune configuration for Unit attributes
1. Configuration and creation for items (low-priority)
1. Finish mimicing the OpenAI gym interface
1. Test basic hand-crafted AI implementations
1. Communication Protocol/API
	- Should external clients request certain units or the whole world state?
1. Write learning code elsewhere that can decide updates
	- Could have multiple sources that are each in charge of a particular team?
1. Write rendering code elsewhere to make for a more fun game to watch
1. Eventually optimize (after everything else is in place and if it turns out to be slow)