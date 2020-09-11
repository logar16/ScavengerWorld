using Moq;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.Units.Interfaces;
using ScavengerWorld.World;
using System;
using Xunit;

namespace ScavengerWorldTest.Units.Actions
{
    public class DropActionExecutorTests : ActionExecutorTests
    {
        [Fact]
        public void Drop_ValidObject_OwnerChanged()
        {
            //Setup
            var unit = CreateUnit();
            var dropper = unit.As<IDropper>();
            var state = CreateStateMock(CreateUnitDictionary(unit));

            var subject = new ActionExecutor(state.Object);

            var transferObject = new Mock<WorldObject>();
            var transferable = transferObject.As<ITransferable>();
            transferable.Setup(tr => tr.Owner).Returns(unit.Object.Id);
            var transferGuid = Guid.NewGuid();
            var action = new DropAction(transferGuid);

            dropper.Setup(d => d.Drop(transferable.Object)).Returns(true);

            state.Setup(s => s.FindObject(transferGuid)).Returns(transferObject.Object);

            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            //Act            
            subject.Execute(actions);

            //Assert
            //Dropper dropped and transfer object knows it has been dropped
            dropper.Verify(d => d.Drop(transferable.Object));
            transferable.Verify(t => t.Drop());
        }

        [Fact]
        public void Drop_NonTransferableObject_FailsAction()
        {
            //Setup
            var unit = CreateUnit();
            var dropper = unit.As<IDropper>();
            var state = CreateStateMock(CreateUnitDictionary(unit));

            var transferObject = new Mock<WorldObject>();
            //Object not given the ITransferable interface so it will fail
            var transferGuid = Guid.NewGuid();
            var action = new DropAction(transferGuid);
            state.Setup(s => s.FindObject(transferGuid)).Returns(transferObject.Object);

            var subject = new ActionExecutor(state.Object);

            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            //Act & Assert
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }

        [Fact]
        public void Drop_UnitCannotDropObject_NoActionTaken()
        {
            //Setup
            var unit = CreateUnit();
            var dropper = unit.As<IDropper>();
            var state = CreateStateMock(CreateUnitDictionary(unit));

            var subject = new ActionExecutor(state.Object);

            var transferObject = new Mock<WorldObject>();
            var transferable = transferObject.As<ITransferable>();
            transferable.Setup(tr => tr.Owner).Returns(unit.Object.Id);
            var transferGuid = Guid.NewGuid();
            var action = new DropAction(transferGuid);

            state.Setup(s => s.FindObject(transferGuid)).Returns(transferObject.Object);

            //Unit will not drop on its end
            dropper.Setup(d => d.Drop(transferable.Object)).Returns(false);

            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            //Act            
            subject.Execute(actions);

            //Assert
            //Dropper asked to drop but transfer object not asked to drop
            dropper.Verify(d => d.Drop(transferable.Object));
            transferable.Verify(t => t.Drop(), Times.Never);
        }
        
        
        [Fact]
        public void Drop_UnownedObject_FailAction()
        {
            //Setup
            var unit = CreateUnit();
            var dropper = unit.As<IDropper>();
            var state = CreateStateMock(CreateUnitDictionary(unit));

            var subject = new ActionExecutor(state.Object);

            var transferObject = new Mock<WorldObject>();
            var transferable = transferObject.As<ITransferable>();
            //Has no owner
            transferable.Setup(tr => tr.Owner).Returns(null);
            
            var transferGuid = Guid.NewGuid();
            var action = new DropAction(transferGuid);

            state.Setup(s => s.FindObject(transferGuid)).Returns(transferObject.Object);

            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            //Act & Assert
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }

        [Fact]
        public void Drop_InvalidObject_FailAction()
        {
            //Setup
            var unit = CreateUnit();
            var dropper = unit.As<IDropper>();
            var state = CreateStateMock(CreateUnitDictionary(unit));

            var subject = new ActionExecutor(state.Object);

            var action = new DropAction(Guid.NewGuid());
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            //Act & Assert
            //No object setup so it is invalid
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }
    }
}
