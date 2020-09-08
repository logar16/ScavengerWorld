using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units.Actions
{
    public class TakeAction : TransferAction
    {
        public TakeAction(Guid objectId) : base(objectId)
        {
        }
    }
}
