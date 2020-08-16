using ScavengerWorld.Items;
using System;

namespace ScavengerWorld.Units
{
    class Warrior: BasicUnit, IItemWielder
    {
        public Warrior()
        {
            Health = 40;
            Attack = 10;
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
    }
}
