using System;

namespace ScavengerWorld.Units.Actions
{
    public class GiveAction : TransferAction
    {
        public Guid Recepient { get; private set; }

        public GiveAction(Guid transferObject, Guid recepient) : base(transferObject)
        {
            Recepient = recepient;
        }

    }
}
