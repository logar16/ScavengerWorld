using System;
using System.Drawing;
using System.Text;

namespace ScavengerWorld.World
{
    public class Geography : ICloneable
    {
        public int Width { get; }
        public int Height { get; }

        private double RatioWalkable { get; set; }

        public enum Terrain { NORMAL, ROUGH };

        private Terrain[,] Space;

        private Random Random;

        protected Geography()
        {

        }

        public Geography(int width, int height) : this(width, height, 1.0) { }

        public Geography(int width, int height, double ratioWalkable, int seed = -1)
        {
            Width = width;
            Height = height;
            Space = new Terrain[height, width];
            RatioWalkable = ratioWalkable;
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
            return $"[Geography] {{ Height: {Height}, Width: {Width}, RatioWalkable: {RatioWalkable} }}";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public virtual Terrain GetTerrainAt(Point location)
        {
            return GetTerrainAt(location.Y, location.X);
        }

        public virtual Terrain GetTerrainAt(int row, int col)
        {
            return Space[row, col];
        }
    }
}
