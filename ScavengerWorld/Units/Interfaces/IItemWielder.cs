using ScavengerWorld.World.Items;

namespace ScavengerWorld.Units
{
    interface IItemWielder
    {
        bool Wield(Item item);
    }
}
