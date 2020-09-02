using ScavengerWorld.Teams;
using ScavengerWorld.Units.Actions;
using Serilog;
using System;
using static ScavengerWorld.World.AmbientEnvironment;

namespace ScavengerWorld.World
{
    public class FullWorld : IWorld
    {
        private WorldState State;
        private WorldState Initial;
        public int StepsTaken { get; private set; }
        public WorldState CurrentState { get => State.Clone(); }

        public FullWorld(WorldState start)
        {
            //TODO: Enable this when Cloning works
            //State = start.Clone();    
            State = start;
            //Initial = start.Clone();
        }

        public WorldState Step(UnitActionCollection actionsToTake, int timeSteps=1)
        {
            StepsTaken++;
            //TODO: Add unit actions being instructed (Unit::TakeAction) 
                //and then applied in the Step function
            State.Step(timeSteps);
            if (StepsTaken % 100 == 0)
            {
                Log.Debug(State.ToString());
            }
            return State;
            //TODO: return State.Clone();
        }


        public WorldState Reset()
        {
            //TODO: Reset the State to inital
            Log.Warning("Not actually reseting the World to initial state...");
            return State;
        }

        public bool IsDone()
        {
            if (StepsTaken >= 1000)
                return true;
            else if (State.Ambience.Season.Equals(Seasons.WINTER))
                return true;
            else
                return false;
        }
    }
}
