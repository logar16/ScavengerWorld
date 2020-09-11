using ScavengerWorld.World;

namespace ScavengerWorld.Units.Interfaces
{
    public interface IDropper
    {
        bool Drop(ITransferable obj);
    }
}
