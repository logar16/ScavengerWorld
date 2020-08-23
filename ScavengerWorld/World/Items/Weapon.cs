using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.World.Items
{
    public class Weapon : Item
    {
        public Weapon(double attack, int numUses)
        {
            AttackModifier = attack;
            RemainingUses = numUses;
        }

        public override void Use()
        {
            RemainingUses--;
        }
    }
}
