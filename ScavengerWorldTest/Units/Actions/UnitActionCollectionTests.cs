using ScavengerWorld.Units.Actions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScavengerWorldTest.Units.Actions
{
    public class UnitActionCollectionTests
    {
        [Fact]
        public void Merge_DuplicateGuid_ThrowsArgumentException()
        {
            //Setup
            UnitActionCollection subject = new();
            var guid = Guid.NewGuid();
            subject.AddAction(guid, new NoopAction());

            UnitActionCollection other = new();
            other.AddAction(guid, new NoopAction());

            //Act + Assert
            Assert.Throws<ArgumentException>(() => subject.Merge(other));
        }

        /// <summary>
        /// Tests both that we can AddAction and that we can combine collections
        /// </summary>
        [Fact]
        public void Merge_AllUnique_AllActionsPresentInOriginal()
        {
            //Setup
            UnitActionCollection subject = new();
            var first = Guid.NewGuid();
            subject.AddAction(first, new NoopAction());
            
            UnitActionCollection other = new();
            var second = Guid.NewGuid();
            other.AddAction(second, new NoopAction());

            //Act
            subject.Merge(other);

            //Assert
            Assert.NotNull(subject.GetAction(first));
            Assert.NotNull(subject.GetAction(second));
        }

        [Fact]
        public void Reset_AllActionsReset()
        {
            //Setup
            UnitActionCollection subject = new();
            var id = Guid.NewGuid();
            var action = new MoveAction(MoveAction.Direction.EAST);
            subject.AddAction(id, action);

            //Assert it was added
            Assert.Equal(action, subject.GetAction(id));

            //Act
            subject.Reset();

            //Assert it was reset to Noop
            Assert.IsType<NoopAction>(subject.GetAction(id));
        }
    }
}
