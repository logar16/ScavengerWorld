using ScavengerWorld.Units.Actions;
using ScavengerWorld.World.Items;
using System;

namespace ScavengerWorld.Units
{
    class Warrior: BasicUnit, IItemWielder
    {
        private Item CurrentItem;

        public bool PickUp(Item item)
        {
            CurrentItem = item;
            return true;
        }

        public Item DropItem()
        {
            Item dropped = CurrentItem;
            CurrentItem = null;
            return dropped;
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
