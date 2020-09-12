using System;

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
