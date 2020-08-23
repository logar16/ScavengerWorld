using ScavengerWorld.Units;
using ScavengerWorld.World.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Teams
{
    public class Team
    {
        public int Id { get; }
        public List<Unit> Units { get; }

        public FoodStorage FoodStorage { get; }

        public Team(int id, int numUnits, FoodStorage storage)
        {
            Id = id;
            var generator = new UnitGenerator();
            Units = generator.CreateRandomUnits(numUnits);
        }

        
    }
}
