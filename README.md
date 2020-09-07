# Scavenger World
This is the Server simulator which should be setting up games, er simulations, 
and playing them.  The simulation is described in more detail in this [Google Doc](https://docs.google.com/document/d/14XEu3WsUuBcH3lxiHeGoCCMesSdKM1msx8qH6TlIveY/edit?usp=sharing).
I chose to do the simulation part in C# so it can take advantage of type safety and speed.
I will probably do the learning agents in other languages for their tools.

## TODOs

1. Test printout to visualize
  * Currently works but it would be nice to have more detailed visualization to improve understandability
1. Configuration and creation for food and items (items low-priority)
1. Action input handled
1. `SensoryDisplay` and how distance affects it
1. Tune configuration for Unit attributes
1. Finish mimicing the OpenAI gym interface
1. Test basic hand-crafted AI implementations
1. Communication Protocol/API
	* Should external clients request certain units or the whole world state?
1. Write learning code elsewhere that can decide updates
	1. Could have multiple sources that are each in charge of a particular team?
1. Write rendering code elsewhere to make for a more fun game to watch
1. Eventually optimize (after everything else is in place and if it turns out to be slow)