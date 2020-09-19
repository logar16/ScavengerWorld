using ScavengerWorld.Sensory;
using System;

namespace ScavengerWorld.World.Markers
{
    public abstract class Marker : WorldObject, ISteppable, IDiscoverable, ITransferable
    {
        public SensoryDisplay Display { get; private set; }
        
        protected double Duration;
        protected double DecayRate;

        Guid ITransferable.Owner { get; set; }

        protected Marker(MarkerDefinition definition, Guid ownerId) : this(definition)
        {
            ((ITransferable)this).TransferTo(ownerId);
        }

        protected Marker(MarkerDefinition definition)
        {
            Display = definition.Display;
            Duration = definition.Duration;
            DecayRate = definition.DecayRate;
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