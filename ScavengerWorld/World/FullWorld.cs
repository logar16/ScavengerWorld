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
        private readonly WorldState Initial;

        private ActionExecutor ActionExecutor;
        public int StepsTaken { get; private set; }
        public WorldState CurrentState { get => State.Clone(); }

        public FullWorld(WorldState start)
        {
            State = start;
            Initial = State.Clone();
            ActionExecutor = new ActionExecutor(State);
        }

        public WorldState Step(UnitActionCollection actionsToTake, int timeSteps=1)
        {
            StepsTaken++;
            ActionExecutor.Execute(actionsToTake);

            State.Step(timeSteps);
            if (StepsTaken % 100 == 0)
            {
                Log.Debug(State.ToString());
            }
            return State;
        }


        public WorldState Reset()
        {
            State = Initial.Clone();
            ActionExecutor = new ActionExecutor(State);
            return State;
        }

        public bool IsDone()
        {
            if (StepsTaken >= 1000000)
                return true;
            else if (State.Ambience.Season.Equals(Seasons.WINTER))
                return true;
            else
                return false;
        }
    }
}
