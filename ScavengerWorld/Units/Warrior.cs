using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using ScavengerWorld.World.Items;
using System;

namespace ScavengerWorld.Units
{
    class Warrior: BasicUnit, IItemWielder
    {
        private Item CurrentItem;

        public bool Wield(Item item)
        {
            CurrentItem = item;
            return true;
        }

        public override bool Drop(WorldObject obj)
        {
            if (base.Drop(obj))
                return true;
            
            if (obj is Item item && item == CurrentItem)
            {
                CurrentItem = null;
                return true;
            }

            return false;
        }


        public override bool CanAttemptAction(UnitAction action)
        {
            if (base.CanAttemptAction(action))
                return true;

            if (action is TransferAction transfer)
            {
                return CurrentItem?.Id == transfer.ObjectId;
            }

            return false;
        }
    }
}
