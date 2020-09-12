using System;

namespace ScavengerWorld.Units.Actions
{
    public abstract class TransferAction : UnitAction
    {
        public Guid ObjectId { get; private set; }
        public TransferAction(Guid objectId)
        {
            ObjectId = objectId;
        }
    }
}
