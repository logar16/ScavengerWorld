using ScavengerWorld.World.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units
{
    interface IItemWielder
    {
        bool PickUpItem(Item item);
        void DropItem();
    }
}
