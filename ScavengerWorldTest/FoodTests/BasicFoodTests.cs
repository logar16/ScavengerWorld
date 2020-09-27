using ScavengerWorld.World.Foods;
using Xunit;

namespace ScavengerWorldTest.FoodTests
{
    public class BasicFoodTests
    {
        [Fact]
        public void Step_StepLargerThanFreshness_FoodQualityDecays()
        {
            Food subject = new Food(5, Food.FoodQuality.EXCELLENT);

            subject.Step(251);

            Assert.Equal(Food.FoodQuality.GOOD, subject.Quality);
        }

        [Fact]
        public void ShouldRemove_FoodQualityPoorAndNotFresh_ReturnTrue()
        {
            Food subject = new Food(5, Food.FoodQuality.POOR);

            subject.Step(1001);

            Assert.True(subject.ShouldRemove());
        }

        [Fact]
        public void ShouldRemove_FoodQualityPoorButStillFresh_ReturnFalse()
        {
            Food subject = new Food(5, Food.FoodQuality.POOR);

            subject.Step(1);

            Assert.False(subject.ShouldRemove());
        }

        [Fact]
        public void ShouldRemove_ZeroQuantity_ReturnTrue()
        {
            Food subject = new Food(5, Food.FoodQuality.POOR);

            subject.Consume(5);

            Assert.True(subject.ShouldRemove());
        }
    }
}
