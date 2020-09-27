using ScavengerWorld.Printing;
using ScavengerWorld.World;
using Xunit;
using Xunit.Abstractions;

namespace ScavengerWorldTest.Printing
{
    public class GeographyPrintTest
    {
        private readonly ITestOutputHelper Output;

        public GeographyPrintTest(ITestOutputHelper output)
        {
            Output = output;
        }

        [Fact]
        public void Print_StandardGeography()
        {
            var geo = new Geography(10, 10, 0.8);

            var print = GeographyPrinter.PrintGeography(geo);

            Output.WriteLine(print);

            Assert.NotEqual(0, print.Length);
        }
    }
}
