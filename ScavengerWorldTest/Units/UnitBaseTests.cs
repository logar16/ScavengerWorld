using Moq;
using ScavengerWorld.Sensory;
using ScavengerWorld.Units;
using ScavengerWorld.Units.Actions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScavengerWorldTest.Units
{
    /// <summary>
    /// The Unit base class should implment some basic functionality:
    ///     - It knows it can noop
    ///     - TODO: It should record some basic stats
    /// </summary>
    public class UnitBaseTests
    {
        [Fact]
        public void CanAct_NoopAction_ReturnTrue()
        {
            var subject = new UnitImpl();
            var noop = new NoopAction();
            Assert.True(subject.CanAttemptAction(noop));
        }

        [Fact]
        public void ResetDisplay_SetAllFeaturesToZero()
        {
            //Setup
            var subject = new UnitImpl();
            var display = new Mock<SensoryDisplay>();
            subject.SetDisplay(display.Object);

            //Act
            subject.ResetDisplay();

            //Assert
            display.Verify(d => d.Reset());
        }
    }

    internal class UnitImpl : Unit
    {
        public UnitImpl()
        {
        }

        public void SetDisplay(SensoryDisplay display) => Display = display;
    }
}
