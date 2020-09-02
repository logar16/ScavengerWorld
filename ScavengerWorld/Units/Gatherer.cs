using ScavengerWorld.Units.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units
{
    public class Gatherer : BasicUnit
    {
       public Gatherer()
        {
            Health = 30;
            AttackLevel = 4;
            Speed = 3;
            LineOfSight = 6;
        }

        public override void Step(int timeStep)
        {
            throw new NotImplementedException();
        }

        public override void TakeAction(UnitAction action)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
