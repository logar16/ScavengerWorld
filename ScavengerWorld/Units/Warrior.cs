using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using ScavengerWorld.World.Items;

namespace ScavengerWorld.Units
{
    class Warrior : BasicUnit, IItemWielder
    {
        private Item CurrentItem;
        public bool HasItem { get => CurrentItem != null; }

        public bool Wield(Item item)
        {
            if (HasItem)
                return false;

            CurrentItem = item;
            return true;
        }

        public override bool Take(ITransferable obj)
        {
            if (base.Take(obj))
                return true;

            if (obj is Item item)
                return Wield(item);

            return false;
        }

        public override bool Drop(ITransferable obj)
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
