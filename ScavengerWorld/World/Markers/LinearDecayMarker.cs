using ScavengerWorld.Sensory;
using System;

namespace ScavengerWorld.World.Markers
{
    public class LinearDecayMarker : Marker
    {
        public LinearDecayMarker(MarkerDefinition definition, Guid ownerId) : base(definition, ownerId) { }
        public LinearDecayMarker(MarkerDefinition definition) : base(definition) { }

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
