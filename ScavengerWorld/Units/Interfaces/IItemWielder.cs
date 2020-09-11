using ScavengerWorld.World.Items;

namespace ScavengerWorld.Units
{
    public interface IItemWielder
    {
        bool Wield(Item item);

        bool HasItem { get; }
    }
}
