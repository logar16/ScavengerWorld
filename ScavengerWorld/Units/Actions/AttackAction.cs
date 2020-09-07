using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units.Actions
{
    public class AttackAction : UnitAction
    {
        public Guid TargetId { get; private set; }
        public AttackAction(Guid targetId)
        {
            TargetId = targetId;
        }
    }
}
