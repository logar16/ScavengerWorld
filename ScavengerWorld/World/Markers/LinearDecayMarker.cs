using ScavengerWorld.Sensory;
using ScavengerWorld.Units;

namespace ScavengerWorld.World.Markers
{
    class LinearDecayMarker : Marker
    {
        public LinearDecayMarker(SensoryDisplay display, Unit owner, double duration, double decayRate) :
            base(display, owner, duration, decayRate)
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
