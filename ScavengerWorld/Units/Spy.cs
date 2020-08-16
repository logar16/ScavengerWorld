using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units
{
    class Spy: BasicUnit
    {
        public Spy()
        {
            Health = 35;
            Attack = 5;
            Speed = 4;
            LineOfSight = 7;
        }
    }
}
