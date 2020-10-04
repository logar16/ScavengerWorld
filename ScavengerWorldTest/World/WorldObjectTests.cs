using ScavengerWorld.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xunit;

namespace ScavengerWorldTest.World
{
    public class WorldObjectTests
    {
        //TODO: Test that inherited functions are correct

        [Fact]
        public void Move_ToPoint_XandYMatchToPoint()
        {
            WObject subject = new();

            var point = new Point(3, 5);
            subject.Move(point);

            Assert.Equal(point, subject.Location);
        }

        [Fact]
        public void Move_NegativeToPoint_XandYSubtractToPoint()
        {
            WObject subject = new();
            subject.Move(10, 10);

            var point = new Point(-3, -5);
            subject.Move(point);

            point = new Point(7, 5);
            Assert.Equal(point, subject.Location);
        }

        [Theory]
        [InlineData(1, 2, 1, 2)]
        [InlineData(-1, 3, 10, 3)]
        [InlineData(1, -3, 1, 10)]
        [InlineData(-1, -30, 10, 10)]
        public void MoveTo_NegativeAxisIgnored_PositiveAxisChanged(int in_x, int in_y, int out_x, int out_y)
        {
            WObject subject = new();
            subject.Move(10, 10);

            var point = new Point(in_x, in_y);
            subject.MoveTo(point);

            point = new Point(out_x, out_y);
            Assert.Equal(point, subject.Location);
        }

        [Fact]
        public void DistanceTo_NullOther_ThrowError()
        {
            WObject subject = new();
            Assert.Throws<ArgumentNullException>(() => subject.DistanceTo(null));
        }

        [Theory]
        [InlineData(7, 6, 5)]
        [InlineData(5, 5, 7.0710678)]
        [InlineData(-5, -5, 21.21320344)]
        public void DistanceTo_Point_ReturnsEuclideanDistance(int x, int y, double distance)
        {
            WObject subject = new();
            subject.Move(10, 10);

            var point = new Point(x, y);
            var actual = subject.DistanceTo(point);

            Assert.Equal(distance, actual, 7);
        }
    }

    internal class WObject : WorldObject
    {
        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
