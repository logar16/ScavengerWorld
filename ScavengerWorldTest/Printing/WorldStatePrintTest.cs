using Moq;
using ScavengerWorld.Printing;
using ScavengerWorld.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace ScavengerWorldTest.Printing
{
    public class WorldStatePrintTest
    {
        private readonly ITestOutputHelper Output;

        public WorldStatePrintTest(ITestOutputHelper output)
        {
            Output = output;
        }

        [Fact]
        public void Print_MultipleUnitsOnSpace_AllRepresented()
        {
            var state = new WorldState();
            state.Geography = new Geography(10, 10);
            GenerateObjects(state, 800);

            var print = StatePrinter.PrintState(state);

            Output.WriteLine(print);

            Assert.NotEqual(0, print.Length);
        }

        private void GenerateObjects(WorldState state, int count)
        {
            var x = state.Geography.Width;
            var y = state.Geography.Height;
            var rand = new Random();
            for (int i = 0; i < count; i++)
            {
                var mock = new Mock<WorldObject>();
                var location = new Point(rand.Next(x), rand.Next(y));
                mock.Setup(m => m.Location).Returns(location);
                mock.Setup(m => m.Id).Returns(Guid.NewGuid());
                state.Add(mock.Object);
            }
        }
    }
}
