using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units
{
    class Scout: BasicUnit
    {
        public Scout()
        {
            Health = 20;
            Attack = 3;
            Speed = 7;
            LineOfSight = 10;
        }
    }
}
