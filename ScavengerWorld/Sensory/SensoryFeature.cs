using System;

namespace ScavengerWorld.Sensory
{
    public class SensoryFeature
    {
        private double InitialStrength;
        public double Strength { get; private set; }

        public SensoryFeature(double strength)
        {
            Strength = InitialStrength = strength;
        }

        public double AdjustStrength(double ratio)
        {
            Strength = InitialStrength * ratio;
            return Strength;
        }
    }
}