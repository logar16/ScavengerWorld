using ScavengerWorld.Sensory;
using ScavengerWorld.Units;

namespace ScavengerWorld.World.Markers
{
    abstract class Marker : WorldObject, ISteppable
    {
        public SensoryDisplay Display { get; }
        public Unit Owner { get; }

        protected double Duration;
        protected double DecayRate;

        protected Marker(SensoryDisplay display, Unit owner, double duration, double decayRate)
        {
            Display = display;
            Owner = owner;
            Duration = duration;
            DecayRate = decayRate;
        }
        
        protected void UpdateDisplayFeatures()
        {
            var features = Display.AsCollection();
            foreach (var item in features)
            {
                item.AdjustStrength(Duration);
            }
        }

        public abstract void Step(int timeStep);
    }
}