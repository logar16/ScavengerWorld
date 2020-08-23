using ScavengerWorld.Units;
using System;

namespace ScavengerWorld.World.Items
{
    public abstract class Item : WorldObject
    {
        public Unit Owner { get; private set; }
        public double AttackModifier { get; protected set; }
        protected int RemainingUses { get; set; }

        public bool IsInHand { get => Owner != null; }

        public bool PickUp(Unit owner)
        {
            if (IsInHand)
                return false;
            
            Owner = owner;
            return true;
        }

        public void Drop()
        {
            Owner = null;
        }

        


        public override bool ShouldRemove()
        {
            return RemainingUses < 0;
        }
        public abstract void Use();
    }
}
