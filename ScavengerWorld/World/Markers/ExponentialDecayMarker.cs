using ScavengerWorld.Sensory;
using ScavengerWorld.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.World.Markers
{
    class ExponentialDecayMarker : Marker
    {
        public ExponentialDecayMarker(SensoryDisplay display, Unit owner, double duration, double decayRate) : 
            base(display, owner, duration, decayRate) 
        { }

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
