using System;

namespace ScavengerWorld.World
{
    public interface ITransferable
    {
        Guid Owner { get; protected set; }

        bool HasOwner
        {
            get => Owner != Guid.Empty;
        }

        bool TransferTo(Guid owner)
        {
            if (HasOwner)
                return false;

            Owner = owner;
            return true;
        }

        void Drop()
        {
            Owner = Guid.Empty;
        }


    }
}
