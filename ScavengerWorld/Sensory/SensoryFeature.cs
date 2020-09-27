using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace ScavengerWorld.Sensory
{
    public class SensoryFeature : ICloneable
    {
        public static readonly int NO_VALUE = -1;
        public static readonly int IGNORE_UPDATE = -2;

        /// <summary>
        /// Note that Strength is a ratio, with 1 being full strength 
        /// and 0 being none at all (totally ineffective)
        /// </summary>
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

        public SensoryFeature()
        {
            Value = NO_VALUE;
            Strength = 0.0;
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

        public void UpdateFrom(SensoryFeature other)
        {
            if (other.Value <= IGNORE_UPDATE)
                return;

            Value = other.Value;
            Strength = InitialStrength = other.Strength;
        }

        public void Reset()
        {
            ResetTo(NO_VALUE, 0);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (obj is SensoryFeature other)
            {
                return other.Value == Value && other.Strength == Strength;
            }

            return false;
        }
    }
}