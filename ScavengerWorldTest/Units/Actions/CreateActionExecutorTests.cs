using Moq;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.Units.Interfaces;
using ScavengerWorld.World;
using Xunit;

namespace ScavengerWorldTest.Units.Actions
{
    public class CreateActionExecutorTests : ActionExecutorTests
    {
        [Fact]
        public void Create_NotACreatingUnit_FailsAction()
        {
            //Setup
            var unit = CreateUnit();

            var state = CreateStateMock(CreateUnitDictionary(unit));

            var action = new CreateAction(0);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);

            //Act & Assert      
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }

        [Fact]
        public void Create_InValidCreateAction_NoCreationNoop()
        {
            //Setup
            var unit = CreateUnit();
            var creator = unit.As<ICreator>();

            var action = new CreateAction(0);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var obj = new Mock<WorldObject>().Object;
            creator.Setup(c => c.CanCreate(action)).Returns(false);

            var state = CreateStateMock(CreateUnitDictionary(unit));

            var subject = new ActionExecutor(state.Object);

            //Act 
            subject.Execute(actions);

            //Assert
            creator.Verify(c => c.CanCreate(action));
            creator.Verify(c => c.Create(action), Times.Never);
            state.Verify(s => s.Add(obj), Times.Never);
        }

        [Fact]
        public void Create_ValidCreateAction_NewObjectCreatedAndAddedToState()
        {
            //Setup
            var unit = CreateUnit();
            var creator = unit.As<ICreator>();

            var action = new CreateAction(0);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);
            
            var obj = new Mock<WorldObject>().Object;
            creator.Setup(c => c.CanCreate(action)).Returns(true);
            creator.Setup(c => c.Create(action)).Returns(obj);

            var state = CreateStateMock(CreateUnitDictionary(unit));

            var subject = new ActionExecutor(state.Object);

            //Act 
            subject.Execute(actions);

            //Assert
            creator.Verify(c => c.CanCreate(action));
            creator.Verify(c => c.Create(action));
            state.Verify(s => s.Add(obj));
        }
    }
}
