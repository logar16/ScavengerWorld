using ScavengerWorld.World;
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

            builder.AppendLine(DrawMap(state));

            builder.AppendLine("Teams: [");
            foreach (var team in state.Teams)
            {
                builder.Append(team.ToString());
            }
            builder.AppendLine("]");

            builder.AppendLine("}");
            return builder.ToString();
        }

        private static string DrawMap(IState state)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Map:");

            int width = state.Geography.Width;
            int height = state.Geography.Height;

            var map = new List<WorldObject>[height, width];

            //Put all items into their respective bin
            foreach (var obj in state.Objects.Values)
            {
                var row = obj.Location.Y;
                var col = obj.Location.X;
                var list = map[row, col];
                if (list == null)
                {
                    list = new List<WorldObject>();
                    map[row, col] = list;
                }
                list.Add(obj);
            }

            //var lists = map.Cast<List<WorldObject>>();
            //var counts = lists.Select(l => l?.Count ?? 0);
            //var max = counts.Max();
            var max = map.Cast<List<WorldObject>>().Select(l => l?.Count).Max() ?? 0;
            var size = 1;
            while (size * size < max)
            {
                size += 1;
            }

            //Add all of the stuff to be drawn into a single, nicely spaced grid
            var grids = new string[height * size, width * size];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    var list = map[row, col];
                    var grid = BuildGrid(size, list);
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            grids[row * size + i, col * size + j] = grid[i, j];
                        }
                    }
                }
            }

            //Put the grid into the string builder
            for (int row = 0; row < height * size; row++)
            {
                if (row % size == 0)
                    builder.AppendLine(new string('-', (width * (size + 1) + 1) * 2 - 1));
                for (int col = 0; col < width * size; col++)
                {
                    if (col % size == 0)
                        builder.Append("| ");
                    builder.Append(grids[row, col] + " ");
                }
                builder.AppendLine("|");
            }
            builder.AppendLine(new string('-', (width * (size + 1) + 1) * 2 - 1));

            return builder.ToString();
        }

        //TODO: If we want to get really fancy, print based on the color of team (or neutral)
        private static string[,] BuildGrid(int size, List<WorldObject> objects)
        {
            var grid = new string[size, size];
            var count = 0;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (count < objects?.Count)
                        grid[row, col] = objects[count].DrawAs();
                    else
                        grid[row, col] = " ";

                    count++;
                }
            }
            return grid;
        }
    }
}
