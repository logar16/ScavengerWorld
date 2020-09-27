using Moq;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.Units.Interfaces;
using ScavengerWorld.World;
using System;
using Xunit;

namespace ScavengerWorldTest.Units.Actions
{
    /// <summary>
    /// A Unit can attack any WorldObject.  We need to make sure it does so in a logical manner.
    /// Rules:
    ///     - Object must exist
    ///     - Object to be attacked must be adjacent to unit (someday may include ranged weapons)
    ///     - Object must reduce its hitpoints (unless it is impervious to certain attacks?)
    ///     - Objects which run out of hitpoint should be "destroyed" and removed from the world
    /// </summary>
    public class AttackActionExecutorTests : ActionExecutorTests
    {
        [Fact]
        public void Attack_NotAnIAttacker_FailsAction()
        {
            //Setup
            var unit = CreateUnit();
            var targetId = Guid.NewGuid();
            
            var state = CreateStateMock(CreateUnitList(unit));
            AddUnitToState(unit, state);

            var action = new AttackAction(targetId);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);

            //Act & Assert      
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }
        
        [Fact]
        public void Attack_NoSuchTarget_FailsAction()
        {
            //Setup
            var unit = CreateUnit();
            var targetId = Guid.NewGuid();
            
            var state = CreateStateMock(CreateUnitList(unit));
            AddUnitToState(unit, state);

            var action = new AttackAction(targetId);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);

            //Act & Assert      
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }

        [Fact]
        public void Attack_DistantTarget_FailsAction()
        {
            //Setup
            var unit = CreateUnit();
            var target = new Mock<WorldObject>();
            target.Setup(t => t.Location).Returns(new System.Drawing.Point(10, 10));
            var targetId = Guid.NewGuid();

            var state = CreateStateMock(CreateUnitList(unit));
            state.Setup(s => s.FindObject(targetId)).Returns(target.Object);
            AddUnitToState(unit, state);

            var action = new AttackAction(targetId);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);

            //Act & Assert
            Assert.Throws<BadActionException>(() => subject.Execute(actions));
        }


        [Fact]
        public void Attack_AdjacentTarget_ReducesTargetHealth()
        {
            //Setup
            var unit = CreateUnit();
            var attacker = unit.As<IAttacker>();
            var attack = new Random().Next(1, 10);
            attacker.Setup(a => a.AttackLevel).Returns(attack);
            var target = new Mock<WorldObject>();
            var targetId = Guid.NewGuid();

            var state = CreateStateMock(CreateUnitList(unit));
            state.Setup(s => s.FindObject(targetId)).Returns(target.Object);
            AddUnitToState(unit, state);

            var action = new AttackAction(targetId);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);

            //Act            
            subject.Execute(actions);

            //Assert
            target.Verify(t => t.TakeDamage(attack));
        }

        [Fact]
        public void Attack_TargetHealthBelowZero_TargetDeletedFromState()
        {
            //Setup
            var unit = CreateUnit();
            var attacker = unit.As<IAttacker>();
            var attack = new Random().Next(1, 10);
            attacker.Setup(a => a.AttackLevel).Returns(attack);
            var target = new Mock<WorldObject>();
            target.Setup(t => t.ShouldRemove()).Returns(true);
            var targetId = Guid.NewGuid();

            var state = CreateStateMock(CreateUnitList(unit));
            state.Setup(s => s.FindObject(targetId)).Returns(target.Object);
            AddUnitToState(unit, state);

            var action = new AttackAction(targetId);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var subject = new ActionExecutor(state.Object);

            //Act            
            subject.Execute(actions);

            //Assert
            target.Verify(t => t.ShouldRemove());
            state.Verify(s => s.Destroy(target.Object));
        }
    }
}
