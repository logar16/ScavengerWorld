using ScavengerWorld.Units.Actions;
using Serilog;
using static ScavengerWorld.World.AmbientEnvironment;

namespace ScavengerWorld.World
{
    public class FullWorld : IWorld
    {
        private const int MAX_STEPS = 1_000_000;

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

        public WorldState Step(UnitActionCollection actionsToTake, int timeSteps = 1)
        {
            StepsTaken++;
            ActionExecutor.Execute(actionsToTake);

            State.Step(timeSteps);
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
            if (StepsTaken >= MAX_STEPS)
            {
                Log.Warning($"World is 'done' because it reached maximum number of steps {MAX_STEPS}");
                return true;
            }
            else if (State.Ambience.Season.Equals(Seasons.WINTER))
            {
                Log.Information("World is 'done' because winter has arrived!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
