using ScavengerWorld.Sensory;
using ScavengerWorld.Units;
using System;

namespace ScavengerWorld.World.Markers
{
    class LinearDecayMarker : Marker
    {
        public LinearDecayMarker(SensoryDisplay display, Guid ownerId, double duration, double decayRate) :
            base(display, ownerId, duration, decayRate)
        { }

        public override void Step(int timeStep)
        {
            Duration -= DecayRate * timeStep;
            UpdateDisplayFeatures();
        }

        public override bool ShouldRemove()
        {
            return Duration > 0;
        }
    }
}
