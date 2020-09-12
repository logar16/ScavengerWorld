using ScavengerWorld.Units.Actions;

namespace ScavengerWorld.World
{
    public interface IWorld
    {
        WorldState CurrentState { get; }

        WorldState Step(UnitActionCollection actionsToTake, int timeSteps = 1);

        WorldState Reset();

        bool IsDone();

        int StepsTaken { get; }

    }
}
