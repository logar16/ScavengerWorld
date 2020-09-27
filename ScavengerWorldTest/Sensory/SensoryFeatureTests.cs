using ScavengerWorld.Sensory;
using Xunit;

namespace ScavengerWorldTest.Sensory
{
    public class SensoryFeatureTests
    {
        [Fact]
        public void UpdateFrom_ValueIsIgnoreValue_NoUpdate()
        {
            SensoryFeature subject = new SensoryFeature(1, 1.0);
            var other = new SensoryFeature(SensoryFeature.IGNORE_UPDATE, 0);

            subject.UpdateFrom(other);

            Assert.Equal(1, subject.Value);
            Assert.Equal(1.0, subject.Strength);
        }

        [Fact]
        public void UpdateFrom_ValidValue_Updated()
        {
            SensoryFeature subject = new SensoryFeature(1, 1.0);
            var other = new SensoryFeature(0, 0.5);

            subject.UpdateFrom(other);

            Assert.Equal(0, subject.Value);
            Assert.Equal(0.5, subject.Strength);
        }


        //TODO: Add tests for JSON deserializing someday.

        /*  TODO: Need to find a home for this... Does not belong in the SensoryFeature class
        /// <summary>
        /// Use to calculate effect of distance on feature strength
        /// </summary>
        /// <param name="distance"></param>
        /// <returns>Estimated strength from that distance</returns>
        public double EffectiveStrength(double distance)
        {
            return Strength * (Math.Pow(0.9, distance) - .2);   //Exponential decay up to ~15 spaces away
        }*/
    }
}
