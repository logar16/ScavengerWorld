# Scavenger World
This is the Server simulator which should be setting up games, er simulations, 
and playing them.  The simulation is described in more detail in this [Google Doc](https://docs.google.com/document/d/14XEu3WsUuBcH3lxiHeGoCCMesSdKM1msx8qH6TlIveY/edit?usp=sharing).
I chose to do the simulation part in C# so it can take advantage of type safety and speed.
I will probably do the learning agents in other languages for their tools.

## TODOs

1. Test printout to visualize
1. Create units/teams in a meaningful way
	1. Different types and distributions pers team
	1. Location of the units should be based on team starting location
	1. Food storage should be close to that
	1. Teams should not be too close to each other
1. Add WorldState::Clone
1. Action input handled
1. Test basic hand-crafted AI implementations
1. Communication Protocol/API
1. Write learning code elsewhere that can decide updates
	1. Could have multiple sources that are each in charge of a particular team?
1. Write rendering code elsewhere to make for a more fun game to watch