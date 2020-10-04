using ScavengerPlayerAI.TeamAI;
using ScavengerWorld.Printing;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using ScavengerWorld.World.Building;
using Serilog;
using System.Collections.Generic;
using System.Diagnostics;

namespace IntegrationTests
{
    public class Simulator
    {
        private IWorld World;

        private List<IPlayer> Players;

        public Simulator(string file)
        {
            var builder = new JsonWorldBuilder();
            World = builder.LoadWorldFile(file);

            Players = new List<IPlayer>();
        }

        public void Run()
        {
            var state = World.Reset();
            var actions = DecideNextActions(state);
            var fullWatch = Stopwatch.StartNew();

            while (!World.IsDone())
            {
                state = World.Step(actions);

                actions = DecideNextActions(state);

                if (World.StepsTaken % 100 == 0)
                {
                    Log.Debug(StatePrinter.PrintState(state));
                }
            }

            fullWatch.Stop();
            Log.Information("Took {steps} steps in {time} milliseconds with an average step of {average} ms",
                World.StepsTaken, fullWatch.ElapsedMilliseconds, (double)fullWatch.ElapsedMilliseconds / World.StepsTaken);
        }

        private UnitActionCollection DecideNextActions(WorldState state)
        {
            var actions = new UnitActionCollection();

            if (Players.Count == 0)
            {
                var units = state.Units;
                actions.SetDefaultActions(units);
                return actions;
            }


            foreach (var player in Players)
            {
                actions.Merge(player.Step(state));
            }

            return actions;
        }

    }
}
