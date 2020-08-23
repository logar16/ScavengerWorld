using ScavengerWorld.Units.Actions;
using ScavengerWorld.World.Items;
using System;

namespace ScavengerWorld.Units
{
    class Warrior: BasicUnit, IItemWielder
    {
        public Warrior()
        {
            Health = 40;
            AttackLevel = 10;
            Speed = 3;
            LineOfSight = 5;
            GatherRate = 1;
            GatherLimit = 1;
        }

        public bool PickUpItem(Item item)
        {
            return false;
        }

        public void DropItem()
        {
            throw new NotImplementedException();
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
