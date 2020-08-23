using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units
{
    class UnitGenerator
    {
        private Random Random;
        public UnitGenerator()
        {
            Random = new Random();
        }
        
        //TODO: Location of units matters, need to reflect that here (probably)
        public List<Unit> CreateRandomUnits(int count)
        {
            var list = new List<Unit>(count);
            for (int i = 0; i < count; i++)
            {
                list.Add(new Warrior());
            }
            return list;
        }

        public static List<Unit> CreateUnits(Type unitType, int count)
        {
            var list = new List<Unit>(count);
            for (int i = 0; i < count; i++)
            {
                list[i] = (Unit)Activator.CreateInstance(unitType);
            }
            return list;
        }
    }
}
