using ScavengerWorld.Units;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.World.Markers;
using Xunit;

namespace ScavengerWorldTest.Units.GathererTests
{
    public class GathererCreateTests
    {
        [Fact]
        public void CanCreate_OutOfRange_ReturnsFalse()
        {
            //Setup
            Gatherer subject = new();

            var action = new CreateAction(5);

            //Act
            var can = subject.CanCreate(action);

            //Assert
            Assert.False(can, "Gatherer thinks it can create something it definitely cannot");
        }

        [Fact]
        public void CanCreate_InRange_ReturnsTrue()
        {
            //Setup
            Gatherer subject = new();

            var action = new CreateAction(0);

            //Act
            var can = subject.CanCreate(action);

            //Assert
            Assert.True(can, "Gatherer thinks it cannot create something it definitely can");
        }

        [Fact]
        public void Create_OutOfRange_ReturnsNull()
        {
            //Setup
            Gatherer subject = new();

            var action = new CreateAction(5);

            //Act
            var obj = subject.Create(action);

            //Assert
            Assert.Null(obj);
        }

        [Fact]
        public void Create_Marker_ReturnsMarker()
        {
            //Setup
            Gatherer subject = new();

            var action = new CreateAction(0);

            //Act
            var obj = subject.Create(action);

            //Assert
            Assert.IsAssignableFrom<Marker>(obj);
        }
    }
}
