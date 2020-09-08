using ScavengerWorld.World.Items;

namespace ScavengerWorld.Units
{
    interface IItemWielder
    {
        bool PickUp(Item item);
        Item DropItem();
    }
}
