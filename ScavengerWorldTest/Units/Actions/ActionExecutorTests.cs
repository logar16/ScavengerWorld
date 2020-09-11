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
        protected Mock<IState> CreateStateMock(Dictionary<Guid, Unit> units)
        {
            var state = new Mock<IState>();
            state.Setup(s => s.AllUnits).Returns(units);
            return state;
        }

        protected Dictionary<Guid, Unit> CreateUnitDictionary(params Mock<Unit>[] units)
        {
            return units.ToDictionary(unit => unit.Object.Id, unit => unit.Object);
        }

        protected Mock<Unit> CreateUnit()
        {
            var unit = new Mock<Unit>();
            unit.Setup(u => u.Id).Returns(Guid.NewGuid());
            unit.Setup(u => u.CanAttemptAction(It.IsAny<UnitAction>())).Returns(true);
            return unit;
        }

        //TODO: Tests for the following cases:
        // Attack with valid/invalid target
        // Drop with valid/invalid object
        // Give with valid/invalid object and valid/invalid recipient
        // Take with valid/invalid target object (what happens if someone grabs it first?)
    }
}
