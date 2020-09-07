using ScavengerWorld.Units.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units
{
    public class Spy : BasicUnit
    {
        public Spy() : base()
        {
            Health = 35;
            AttackLevel = 5;
            Speed = 4;
            LineOfSight = 7;
        }

        public override void Step(int timeStep)
        {
            throw new NotImplementedException();
        }

        public override bool CanAttemptAction(UnitAction action)
        {
            throw new NotImplementedException();
        }
    }
}
