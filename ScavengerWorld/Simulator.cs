using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using ScavengerWorld.World.Building;
using Serilog;
using System.Diagnostics;
using System.Linq;

namespace ScavengerWorld
{
    public class Simulator
    {
        private IWorld World;

        public Simulator()
        {
            //var builder = new WorldBuilder();
            //World = builder.WithGeography(10, 6)
            //               .WithAmbience(100, 20)
            //               .WithTeams(2, 10)
            //               .WithFood(2.0)
            //               .Build();

            var builder = new JsonWorldBuilder();
            World = builder.LoadWorldFile("Config/Worlds/test-world.json");
        }

        public void Run()
        {
            var state = World.Reset();
            var actions = new UnitActionCollection();
            var fullWatch = Stopwatch.StartNew();

            while (!World.IsDone())
            {
                var units = state.AllUnits.Values;
                actions.SetDefaultActions(units);

                state = World.Step(actions);
                actions.Reset();
            }

            fullWatch.Stop();
            Log.Information("Took {steps} steps in {time} milliseconds with an average step of {average} ms", 
                World.StepsTaken, fullWatch.ElapsedMilliseconds, (double)fullWatch.ElapsedMilliseconds / World.StepsTaken);
        }
    }
}
