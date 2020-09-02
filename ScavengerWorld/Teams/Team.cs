using ScavengerWorld.Units;
using ScavengerWorld.World.Foods;
using Serilog;
using System.Collections.Generic;
using System.Text;

namespace ScavengerWorld.Teams
{
    public class Team
    {
        public int Id { get; }
        public List<Unit> Units { get; }

        public FoodStorage FoodStorage { get; }

        public Team(Team team)
        {
            Id = team.Id;
            Units = new List<Unit>();
            foreach (var unit in team.Units)
            {
                Units.Add((Unit)unit.Clone());
            }
        }

        public Team(int id, List<Unit> units, FoodStorage storage) : this(id, units)
        {
            FoodStorage = storage;
        }

        public Team(int id, List<Unit> units)
        {
            Id = id;
            Units = units;
            //Log.Debug(ToString());
        }

        public override string ToString()
        {
            var builder = new StringBuilder($"Team {Id}: [ ");
            foreach (var unit in Units)
            {
                builder.Append($"{unit}, ");
            }
            builder.AppendLine(" ]");
            return builder.ToString();
        }
    }
}
