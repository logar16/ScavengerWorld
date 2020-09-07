using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
