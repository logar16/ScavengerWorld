using ScavengerWorld.Sensory;
using System;
using System.Linq;
using Xunit;

namespace ScavengerWorldTest.Sensory
{
    public class SensoryDisplayTests
    {
        [Fact]
        public void Reset_SetAllFeaturesToZero()
        {
            SensoryDisplay subject = CreateSensoryDisplay();

            //Act
            subject.Reset();

            //Assert
            foreach (var feature in subject.AsCollection())
            {
                Assert.Equal(SensoryFeature.NO_VALUE, feature.Value);
                Assert.Equal(0, feature.Strength);
            }
        }

        [Fact]
        public void UpdateTo_AllFeaturesChange()
        {
            SensoryDisplay subject = CreateSensoryDisplay();
            //Create a totally different display
            SensoryDisplay update = CreateSensoryDisplay();
            
            //Act
            subject.UpdateFrom(update);

            //Assert
            var actual = subject.AsCollection();
            var expected = update.AsCollection();
            Assert.Equal(expected, actual);
        }


        private static SensoryDisplay CreateSensoryDisplay()
        {
            Random random = new Random();
            SensoryDisplay subject = new SensoryDisplay();
            subject.Visual.ResetTo(random.Next(4), random.Next(20));
            subject.Auditory.ResetTo(random.Next(4), random.Next(20));
            subject.Smell.ResetTo(random.Next(4), random.Next(20));
            subject.Taste.ResetTo(random.Next(4), random.Next(20));
            return subject;
        }
    }
}
