using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace ScavengerWorld.Sensory
{
    public class SensoryFeature : ICloneable
    {
        [JsonProperty("strength")]
        [DefaultValue(0)]
        public double Strength { get; private set; }
        private double InitialStrength;

        /// <summary>
        /// Indicates a particular value for the sensory feature.  
        /// Currently arbitrary, but could be used like 0 = black, 1 = red, 2 = blue, 3 = green, etc.
        /// </summary>
        [JsonProperty("value")]
        [DefaultValue(0)]
        public int Value { get; private set; }

        public SensoryFeature(int value, double strength)
        {
            Value = value;
            Strength = InitialStrength = strength;
        }

        public double AdjustStrength(double ratio)
        {
            Strength = InitialStrength * ratio;
            return Strength;
        }

        public void ResetTo(int value, double strength)
        {
            Value = value;
            Strength = InitialStrength = strength;
        }

        public void ResetTo(SensoryFeature other)
        {
            Value = other.Value;
            Strength = InitialStrength = other.Strength;
        }

        /// <summary>
        /// Use to calculate effect of distance on feature strength
        /// </summary>
        /// <param name="distance"></param>
        /// <returns>Estimated strength from that distance</returns>
        public double EffectiveStrength(double distance)
        {
            return Strength * (Math.Pow(0.9, distance) - .2);   //Exponential decay up to ~15 spaces away
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}