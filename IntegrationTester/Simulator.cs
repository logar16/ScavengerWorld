using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using ScavengerWorld.World.Building;
using Serilog;
using System.Diagnostics;

namespace IntegrationTests
{
    public class Simulator
    {
        private IWorld World;

        public Simulator(string file)
        {
            var builder = new JsonWorldBuilder();
            World = builder.LoadWorldFile(file);
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
