using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units
{
    class Gatherer: BasicUnit
    {
       public Gatherer()
        {
            Health = 30;
            Attack = 4;
            Speed = 3;
            LineOfSight = 6;
        }
    }
}
