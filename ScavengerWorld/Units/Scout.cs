using ScavengerWorld.Units.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units
{
    public class Scout : BasicUnit
    {
        public Scout()
        {
            Health = 20;
            AttackLevel = 3;
            Speed = 7;
            LineOfSight = 10;
        }

        public override void Step(int timeStep)
        {
            throw new NotImplementedException();
        }

        public override void TakeAction(UnitAction action)
        {
            throw new NotImplementedException();
        }

    }
}
