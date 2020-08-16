using System;

namespace ScavengerWorld.Sensory
{
    public class SensoryFeature: WorldObject
    {
        public double Strength { get; }

        public SensoryFeature(double strength)
        {
            Strength = strength;
        }
    }
}