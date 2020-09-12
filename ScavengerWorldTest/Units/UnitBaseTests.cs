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
    }

    internal class UnitImpl : Unit
    {
        public UnitImpl()
        {
        }
    }
}
