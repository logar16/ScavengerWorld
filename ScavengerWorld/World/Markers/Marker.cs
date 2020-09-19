using ScavengerWorld.Sensory;
using System;

namespace ScavengerWorld.World.Markers
{
    abstract class Marker : WorldObject, ISteppable, IDiscoverable, ITransferable
    {
        public SensoryDisplay Display { get; private set; }
        
        protected double Duration;
        protected double DecayRate;

        Guid ITransferable.Owner { get; set; }

        protected Marker(SensoryDisplay display, Guid ownerId, double duration, double decayRate)
        {
            Display = display;
            Duration = duration;
            DecayRate = decayRate;
            ((ITransferable)this).TransferTo(ownerId);
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