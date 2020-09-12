using Moq;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.Units.Interfaces;
using ScavengerWorld.World;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScavengerWorldTest.Units.Actions
{
    public class TakeActionExecutorTests : ActionExecutorTests
    {
        [Fact]
        public void Take_AdjacentAvailableTarget_UnitOwnsTarget()
        {
            //Setup
            var unit = CreateUnit();
            var taker = unit.As<ITaker>();
            var target = new Mock<WorldObject>();
            var transfer = target.As<ITransferable>();
            var targetId = Guid.NewGuid();

            var state = CreateStateMock(CreateUnitDictionary(unit));
            state.Setup(s => s.FindObject(targetId)).Returns(target.Object);

            var action = new TakeAction(targetId);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);

            //Act            
            subject.Execute(actions);

            //Assert
            transfer.Verify(t => t.TransferTo(unit.Object.Id));
            taker.Verify(t => t.Take(transfer.Object));
        }

        [Fact]
        public void Take_InvalidTarget_FailAction()
        {
            //Setup
            var unit = CreateUnit();
            var taker = unit.As<ITaker>();
            var target = new Mock<WorldObject>();
            var targetId = Guid.NewGuid();

            var state = CreateStateMock(CreateUnitDictionary(unit));
            state.Setup(s => s.FindObject(targetId)).Returns(target.Object);

            var action = new TakeAction(targetId);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);

            //Act & Assert
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }

        [Fact]
        public void Take_NonExistantTarget_FailAction()
        {
            //Setup
            var unit = CreateUnit();
            var taker = unit.As<ITaker>();
            var targetId = Guid.NewGuid();

            var state = CreateStateMock(CreateUnitDictionary(unit));

            var action = new TakeAction(targetId);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);

            //Act & Assert
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }

        [Fact]
        public void Take_DistantTarget_FailsAction()
        {
            //Setup
            var unit = CreateUnit();
            var taker = unit.As<ITaker>();
            var target = new Mock<WorldObject>();
            var transfer = target.As<ITransferable>();
            var targetId = Guid.NewGuid();
            target.Setup(t => t.Location).Returns(new System.Drawing.Point(10, 10));

            var state = CreateStateMock(CreateUnitDictionary(unit));
            state.Setup(s => s.FindObject(targetId)).Returns(target.Object);

            var action = new TakeAction(targetId);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);

            //Act & Assert
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }

        [Fact]
        public void Take_TargetAlreadyOwned_FailsAction()
        {
            //Setup
            var unit = CreateUnit();
            var taker = unit.As<ITaker>();
            var target = new Mock<WorldObject>();
            var transfer = target.As<ITransferable>();
            var targetId = Guid.NewGuid();

            transfer.Setup(t => t.HasOwner).Returns(true);

            var state = CreateStateMock(CreateUnitDictionary(unit));
            state.Setup(s => s.FindObject(targetId)).Returns(target.Object);

            var action = new TakeAction(targetId);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);

            //Act & Assert
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }
    }
}
