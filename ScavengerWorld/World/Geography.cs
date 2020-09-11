using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScavengerWorld.World
{
    public class Geography : ICloneable
    {
        public int Width { get; }
        public int Height { get; }

        public enum Terrain { NORMAL, ROUGH };

        private Terrain[,] Space;

        private Random Random;

        protected Geography()
        {

        }

        public Geography(int width, int height) : this(width, height, 1.0) { }

        public Geography(int width, int height, double ratioWalkable, int seed=-1)
        {
            Width = width;
            Height = height;
            Space = new Terrain[height, width];
            Random = (seed < 0) ? new Random() : new Random(seed);

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    var terrain = (Random.NextDouble() < ratioWalkable) ? Terrain.NORMAL : Terrain.ROUGH;
                    Space[row, col] = terrain;
                }
            }
            //Log.Debug("World Geography: " + ToString());
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine();
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    if (col == 0)
                        builder.Append("|");
                    builder.Append($"{(int)Space[row, col]}|");
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public virtual Terrain GetTerrainAt(Point location)
        {
            return Space[(int)location.Y, (int)location.X];
        }
    }
}
