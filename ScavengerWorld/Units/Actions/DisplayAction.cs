using ScavengerWorld.Sensory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScavengerWorld.Units.Actions
{
    public class DisplayAction : UnitAction
    {
        public SensoryDisplay Update { get; private set; }

        public bool IsReset { get => Update == null; }

        public DisplayAction(SensoryDisplay update)
        {
            Update = update;
        }

        public DisplayAction()
        {

        }
    }
}
