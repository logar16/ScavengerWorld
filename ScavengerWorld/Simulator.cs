using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using ScavengerWorld.World.Building;
using Serilog;
using System.Linq;

namespace ScavengerWorld
{
    public class Simulator
    {
        private IWorld World;

        public Simulator()
        {
            var builder = new WorldBuilder();
            World = builder.WithGeography(10, 6)
                           .WithAmbience(100, 20)
                           .WithTeams(2, 10)
                           .Build();
        }

        public void Run()
        {
            var state = World.Reset();
            var actions = new UnitActionCollection();

            while (!World.IsDone())
            {
                var units = state.Teams.SelectMany(t => t.Units);
                actions.SetDefaultActions(units);

                state = World.Step(actions);
                actions.Reset();
            }
            Log.Information("Took {X} steps", World.StepsTaken);
        }
    }
}
