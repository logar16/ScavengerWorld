using ScavengerWorld.Teams;
using ScavengerWorld.World.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScavengerWorld.World
{
    public class WorldState : ISteppable
    {
        public AmbientEnvironment Ambience { get; internal set; }
        public Geography Geography { get; internal set; }
        public List<Team> Teams { get; internal set; }
        public int TotalUnits { get { return Teams.Sum(t => t.UnitCount); } }

        public List<Food> Food { get; internal set; }
        public List<WorldObject> InanimateObjects { get; internal set; }
        //private List<WorldObject> DestroyedObjects;

        public WorldState()
        {
            Teams = new List<Team>();
            Food = new List<Food>();
            InanimateObjects = new List<WorldObject>();
        }

        internal WorldState Clone()
        {
            var other = (WorldState)MemberwiseClone();
            other.Ambience = (AmbientEnvironment)Ambience.Clone();
            other.Geography = (Geography)Geography.Clone();

            other.Teams = new List<Team>();
            foreach (var team in Teams)
            {
                var copy = new Team(team);
                other.Teams.Add(copy);
            }

            other.Food = new List<Food>();
            foreach (var food in Food)
            {
                other.Food.Add((Food)food.Clone());
            }
            
            other.InanimateObjects = new List<WorldObject>();
            foreach (var item in InanimateObjects)
            {
                other.InanimateObjects.Add((WorldObject)item.Clone());
            }

            return other;
        }


        public void Step(int timeStep)
        {
            Ambience.Step(timeStep);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("World: {");

            builder.AppendLine($"Geography: {Geography}");

            builder.AppendLine($"Ambience: {Ambience}");

            builder.AppendLine();

            builder.AppendLine(DrawUnitMap());

            builder.AppendLine("Teams: [");
            foreach (var team in Teams)
            {
                builder.Append(team.ToString());
            }
            builder.AppendLine("]");

            builder.AppendLine("}");
            return builder.ToString();
        }

        private string DrawUnitMap()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Unit Map:");

            int width = Geography.Width;
            int height = Geography.Height;

            var map = new int[height, width];

            foreach (var team in Teams)
            {
                var locations = team.Units.Select(u => u.Location);
                foreach (var location in locations)
                {
                    map[(int)location.Y, (int)location.X] += 1;
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