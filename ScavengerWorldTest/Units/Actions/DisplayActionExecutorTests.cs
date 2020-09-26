using Moq;
using ScavengerWorld.Sensory;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScavengerWorldTest.Units.Actions
{
    public class DisplayActionExecutorTests : ActionExecutorTests
    {
        [Fact]
        public void Display_DisplayUpdateCalled()
        {
            //Setup
            var unit = CreateUnit();
            SensoryDisplay display = new SensoryDisplay();
            display.Taste.ResetTo(1, 5);
            unit.Setup(u => u.Display.UpdateTo(display));
            var action = new DisplayAction(display);
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var state = CreateStateMock(CreateUnitDictionary(unit));

            var subject = new ActionExecutor(state.Object);

            //Act 
            subject.Execute(actions);

            //Assert
            unit.Verify(unit => unit.Display.UpdateTo(display));
        }

        [Fact]
        public void Display_Reset_UnitResetDisplayCalled()
        {
            //Setup
            var unit = CreateUnit();
            
            var action = new DisplayAction();
            UnitActionCollection actions = new UnitActionCollection();
            actions.AddAction(unit.Object.Id, action);

            var state = CreateStateMock(CreateUnitDictionary(unit));

            var subject = new ActionExecutor(state.Object);

            //Act 
            subject.Execute(actions);

            //Assert
            unit.Verify(unit => unit.ResetDisplay());
        }

    }
}
