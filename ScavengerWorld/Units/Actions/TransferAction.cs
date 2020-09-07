using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units.Actions
{
    public class TransferAction : UnitAction
    {
        public Guid ObjectId { get; private set; }
        public TransferAction(Guid objectId)
        {
            ObjectId = objectId;
        }
    }
}
