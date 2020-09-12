using ScavengerWorld.Units;
using ScavengerWorld.World.Foods;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScavengerWorld.Teams
{
    public class Team
    {
        public int Id { get; }
        public List<Unit> Units { get; }
        public int UnitCount { get => Units.Count; }

        public FoodStorage FoodStorage { get; }

        public Team(Team other)
        {
            Id = other.Id;
            Units = other.Units.Select(unit => (Unit)unit.Clone()).ToList();

            if (other.FoodStorage != null)
            {
                FoodStorage = (FoodStorage)other.FoodStorage.Clone();
            }
        }

        public Team(int id, List<Unit> units, FoodStorage storage) : this(id, units)
        {
            FoodStorage = storage;
            FoodStorage.TeamId = Id;
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
