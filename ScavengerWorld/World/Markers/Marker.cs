using ScavengerWorld.Sensory;
using ScavengerWorld.Units;
using System;

namespace ScavengerWorld.World.Markers
{
    abstract class Marker : WorldObject, ISteppable
    {
        public SensoryDisplay Display { get; private set; }
        public Guid OwnerId { get; }

        protected double Duration;
        protected double DecayRate;

        protected Marker(SensoryDisplay display, Guid ownerId, double duration, double decayRate)
        {
            Display = display;
            OwnerId = ownerId;
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

        public override object Clone()
        {
            Marker copy = (Marker)MemberwiseClone();
            copy.Display = (SensoryDisplay)Display.Clone();
            return copy;
        }
    }
}