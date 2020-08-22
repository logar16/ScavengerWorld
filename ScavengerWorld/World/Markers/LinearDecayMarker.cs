using ScavengerWorld.Sensory;
using ScavengerWorld.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.World.Markers
{
    class LinearDecayMarker : Marker
    {
        public LinearDecayMarker(SensoryDisplay display, Unit owner, double duration, double decayRate) :
            base(display, owner, duration, decayRate)
        { }

        public override bool Age(int timeStep)
        {
            Duration -= DecayRate * timeStep;
            return IsActive();
        }

        public override bool IsActive()
        {
            return Duration > 0;
        }
    }
}
