using ScavengerWorld.Sensory;
using System;

namespace ScavengerWorld.World.Markers
{
    public class ExponentialDecayMarker : Marker
    {
        public ExponentialDecayMarker(MarkerDefinition definition, Guid ownerId) : base(definition, ownerId) { }
        public ExponentialDecayMarker(MarkerDefinition definition) : base(definition) { }

        public override void Step(int timeStep)
        {
            var modifier = Math.Pow(DecayRate, timeStep + 1);
            Duration *= modifier;
            UpdateDisplayFeatures();
        }

        public override bool ShouldRemove()
        {
            return Duration > 0.001;
        }
    }
}
