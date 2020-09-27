using ScavengerWorld.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScavengerWorld.Printing
{
    public class StatePrinter
    {
        public static string PrintState(IState state)
        {
            var builder = new StringBuilder();
            builder.AppendLine("World: {");

            builder.AppendLine($"Geography: {state.Geography}");

            builder.AppendLine($"Ambience: {state.Ambience}");

            builder.AppendLine();

            builder.AppendLine(DrawUnitMap(state));

            builder.AppendLine("Teams: [");
            foreach (var team in state.Teams)
            {
                builder.Append(team.ToString());
            }
            builder.AppendLine("]");

            builder.AppendLine("}");
            return builder.ToString();
        }

        private static string DrawUnitMap(IState state)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Unit Map:");

            int width = state.Geography.Width;
            int height = state.Geography.Height;

            var map = new int[height, width];

            foreach (var team in state.Teams)
            {
                var locations = team.Units.Select(u => u.Location);
                foreach (var location in locations)
                {
                    map[location.Y, location.X] += 1;
                }
            }

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (col == 0)
                        builder.Append("|");
                    builder.Append($"{map[row, col]}|");
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
