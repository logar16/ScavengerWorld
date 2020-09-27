using Moq;
using ScavengerWorld.Units;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ScavengerWorldTest.Units.Actions
{
    public class ActionExecutorTests
    {
        protected Mock<IState> CreateStateMock(IEnumerable<Unit> units)
        {
            var state = new Mock<IState>();
            state.Setup(s => s.Units).Returns(units);
            return state;
        }

        protected IEnumerable<Unit> CreateUnitList(params Mock<Unit>[] units)
        {
            return units.Select(unit => unit.Object);
        }

        protected Mock<Unit> CreateUnit()
        {
            var unit = new Mock<Unit>();
            unit.Setup(u => u.Id).Returns(Guid.NewGuid());
            unit.Setup(u => u.CanAttemptAction(It.IsAny<UnitAction>())).Returns(true);
            return unit;
        }
        
        protected void AddUnitToState(Mock<Unit> unit, Mock<IState> state)
        {
            state.Setup(s => s.FindObject(unit.Object.Id)).Returns(unit.Object);
        }
    }
}
