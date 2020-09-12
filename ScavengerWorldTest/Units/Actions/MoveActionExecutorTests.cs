using Moq;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using System.Drawing;
using Xunit;

namespace ScavengerWorldTest.Units.Actions
{
    /// <summary>
    /// Tests to support the movement of units on the map.
    /// Future day may include units that can move more than one space at a time.
    /// Success is currently defined as able to move one unit in any cardinal direction
    /// as long as the terrain allows it (not ROUGH).
    /// </summary>
    public class MoveActionExecutorTests : ActionExecutorTests
    {
        [Theory]
        [InlineData(MoveAction.Direction.NORTH, 5, 4)]
        [InlineData(MoveAction.Direction.EAST, 6, 5)]
        [InlineData(MoveAction.Direction.SOUTH, 5, 6)]
        [InlineData(MoveAction.Direction.WEST, 4, 5)]
        public void Move_NormalTerrain_UnitMoved(MoveAction.Direction direction, int x, int y)
        {
            //Setup
            var unit = CreateUnit();
            unit.Setup(u => u.Location).Returns(new Point(5, 5));
            var newLocation = new Point(x, y);
            
            var state = CreateStateMock(CreateUnitDictionary(unit));
            state.Setup(s => s.Geography.GetTerrainAt(newLocation)).Returns(Geography.Terrain.NORMAL);


            var action = new MoveAction(direction);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);


            //Act            
            subject.Execute(actions);

            //Assert
            //Moved in the correct direction
            unit.Verify(u => u.Move(newLocation));
        }

        [Theory]
        [InlineData(MoveAction.Direction.NORTH, 5, 4)]
        [InlineData(MoveAction.Direction.EAST, 6, 5)]
        [InlineData(MoveAction.Direction.SOUTH, 5, 6)]
        [InlineData(MoveAction.Direction.WEST, 4, 5)]
        public void Move_RoughTerrain_UnitStationary(MoveAction.Direction direction, int x, int y)
        {
            //Setup
            var unit = CreateUnit();
            unit.Setup(u => u.Location).Returns(new Point(5, 5));
            var newLocation = new Point(x, y);

            var state = CreateStateMock(CreateUnitDictionary(unit));
            state.Setup(s => s.Geography.GetTerrainAt(newLocation)).Returns(Geography.Terrain.ROUGH);


            var action = new MoveAction(direction);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);


            //Act            
            subject.Execute(actions);

            //Assert
            //Moved in the correct direction
            unit.Verify(u => u.Move(newLocation), Times.Never);
        }
    }
}
