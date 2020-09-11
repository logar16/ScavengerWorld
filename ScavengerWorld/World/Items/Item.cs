using ScavengerWorld.Units;
using System;

namespace ScavengerWorld.World.Items
{
    public abstract class Item : WorldObject, ITransferable
    {
        Guid ITransferable.Owner { get; set; }

        public double AttackModifier { get; protected set; }
        protected int RemainingUses { get; set; }

        public override bool ShouldRemove()
        {
            return RemainingUses < 0;
        }
        public abstract void Use();

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
