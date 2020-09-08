using Xunit;

namespace ScavengerWorldTest.Units.Actions
{
    public class ActionExecutorTests
    {
        [Fact]
        public void MoveAction_ValidMove_UnitMoved()
        {
            //TODO: Test with every direction
            Assert.True(false, "Not ready!");
        }

        [Fact]
        public void MoveAction_MoveToBadTerrain_UnitNotMoved()
        {
            Assert.True(false, "Not ready!");
        }

        //TODO: Tests for the following cases:
            // Attack with valid/invalid target
            // Drop with valid/invalid object
            // Give with valid/invalid object and valid/invalid recipient
            // Take with valid/invalid target object (what happens if someone grabs it first?)
    }
}
