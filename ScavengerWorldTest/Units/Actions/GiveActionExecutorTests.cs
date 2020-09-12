using Moq;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.Units.Interfaces;
using ScavengerWorld.World;
using System;
using System.Drawing;
using Xunit;

namespace ScavengerWorldTest.Units.Actions
{
    /// <summary>
    /// This is a sister-class to the DropActionExecutorTests in that they share lots of functionality
    /// due to dropping being a common feature.
    /// Giving can only happen if: 
    ///     - The giver is an IDropper and has the object
    ///     - The object is ITransferable
    ///     - The receiver is an ITaker
    ///     - Every party exists
    /// Success should look like ownership transferring from giver to taker 
    /// (object being removed from giver and given to taker)
    /// </summary>
    public class GiveActionExecutorTests : ActionExecutorTests
    {
        [Fact]
        public void GiveAway_ValidObjectAndRecepient_OwnerChanged()
        {
            //Setup
            var first = CreateUnit();
            var dropper = first.As<IDropper>();
            var second = CreateUnit();
            var receiver = second.As<IReceiver>();
            var state = CreateStateMock(CreateUnitDictionary(first, second));
            var subject = new ActionExecutor(state.Object);

            var transferObject = new Mock<WorldObject>();
            var transferable = transferObject.As<ITransferable>();
            transferable.Setup(tr => tr.Owner).Returns(first.Object.Id);
            var transferGuid = Guid.NewGuid();
            var action = new GiveAction(transferGuid, second.Object.Id);

            dropper.Setup(d => d.Drop(transferable.Object)).Returns(true);
            receiver.Setup(t => t.Receive(transferable.Object)).Returns(true);

            state.Setup(s => s.FindObject(second.Object.Id)).Returns(second.Object);
            state.Setup(s => s.FindObject(transferGuid)).Returns(transferObject.Object);

            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(first.Object.Id, action);

            //Act            
            subject.Execute(actions);

            //Assert
            //Dropper dropped, reciever received, and transfer object knows its new owner
            dropper.Verify(d => d.Drop(transferable.Object));
            receiver.Verify(r => r.Receive(transferable.Object));
            transferable.Verify(t => t.Drop());
            transferable.Verify(t => t.TransferTo(second.Object.Id));
        }

        [Fact]
        public void GiveAway_ValidObjectInvalidRecepient_ObjectDropped()
        {
            //Setup
            var first = CreateUnit();
            var dropper = first.As<IDropper>();
            var second = CreateUnit();
            var state = CreateStateMock(CreateUnitDictionary(first, second));
            var subject = new ActionExecutor(state.Object);

            var transferObject = new Mock<WorldObject>();
            var transferable = transferObject.As<ITransferable>();
            transferable.Setup(tr => tr.Owner).Returns(first.Object.Id);
            var transferGuid = Guid.NewGuid();
            var action = new GiveAction(transferGuid, second.Object.Id);

            dropper.Setup(d => d.Drop(transferable.Object)).Returns(true);

            state.Setup(s => s.FindObject(second.Object.Id)).Returns(second.Object);
            state.Setup(s => s.FindObject(transferGuid)).Returns(transferObject.Object);



            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(first.Object.Id, action);

            //Act
            subject.Execute(actions);

            //Assert
            //Dropper dropped, and transfer object knows it was dropped
            dropper.Verify(d => d.Drop(transferable.Object));
            transferable.Verify(t => t.Drop());
            transferable.Verify(t => t.TransferTo(second.Object.Id), Times.Never);
        }

        [Fact]
        public void GiveAway_ValidObjectDistantRecepient_ObjectDropped()
        {
            //Setup
            var first = CreateUnit();
            var dropper = first.As<IDropper>();
            var second = CreateUnit();
            second.Setup(s => s.Location).Returns(new Point(10, 10));
            var receiver = second.As<IReceiver>();
            var state = CreateStateMock(CreateUnitDictionary(first, second));
            var subject = new ActionExecutor(state.Object);

            var transferObject = new Mock<WorldObject>();
            var transferable = transferObject.As<ITransferable>();
            transferable.Setup(tr => tr.Owner).Returns(first.Object.Id);
            var transferGuid = Guid.NewGuid();
            var action = new GiveAction(transferGuid, second.Object.Id);

            dropper.Setup(d => d.Drop(transferable.Object)).Returns(true);

            state.Setup(s => s.FindObject(second.Object.Id)).Returns(second.Object);
            state.Setup(s => s.FindObject(transferGuid)).Returns(transferObject.Object);

            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(first.Object.Id, action);

            //Act
            subject.Execute(actions);

            //Assert
            //Dropper dropped, and transfer object knows it was dropped and receiver was not given object
            dropper.Verify(d => d.Drop(transferable.Object));
            transferable.Verify(t => t.Drop());
            transferable.Verify(t => t.TransferTo(second.Object.Id), Times.Never);
            receiver.Verify(r => r.Receive(It.IsAny<ITransferable>()), Times.Never);
        }


        [Fact]
        public void GiveAway_InvalidObject_FailAction()
        {
            //Setup
            var first = CreateUnit();
            var dropper = first.As<IDropper>();
            var second = CreateUnit();
            var state = CreateStateMock(CreateUnitDictionary(first, second));
            var transferGuid = Guid.NewGuid();
            var action = new GiveAction(transferGuid, second.Object.Id);

            var subject = new ActionExecutor(state.Object);


            state.Setup(s => s.FindObject(second.Object.Id)).Returns(second.Object);

            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(first.Object.Id, action);

            //Assertion
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }

    }
}
