namespace ScavengerWorld.World
{
    public interface IReceiver
    {
        bool Receive(ITransferable obj);
    }
}
