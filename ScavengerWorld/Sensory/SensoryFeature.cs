using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace ScavengerWorld.Sensory
{
    public class SensoryFeature: ICloneable
    {
        [JsonProperty("strength")]
        [DefaultValue(0)]
        public double Strength { get; private set; }
        private double InitialStrength;

        /// <summary>
        /// Indicates a particular value for the sensory feature.  
        /// Currently arbitrary, but could be 0 = black, 1 = red, 2 = blue, 3 = green, etc.
        /// </summary>
        [JsonProperty("value")]
        [DefaultValue(0)]
        public double Value { get; private set; }

        public static readonly SensoryFeature NONE = new SensoryFeature(0, 0);

        public SensoryFeature(double value, double strength)
        {
            Value = value;
            Strength = InitialStrength = strength;
        }

        public double AdjustStrength(double ratio)
        {
            Strength = InitialStrength * ratio;
            return Strength;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}