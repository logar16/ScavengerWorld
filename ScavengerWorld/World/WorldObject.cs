using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScavengerWorld.World
{
    public abstract class WorldObject : ICloneable
    {
        public Point Location { get; private set; }
        public Guid Id { get; private set; }

        protected WorldObject()
        {
            Id = Guid.NewGuid();
            Location = new Point();
        }

        public void Move(Point direction)
        {
            Move((int)direction.X, (int)direction.Y);
        }

        public void Move(int xDelta, int yDelta)
        {
            var location = Location;
            location.Offset(xDelta, yDelta);
            Location = location;
        }

        public abstract bool ShouldRemove();

        public abstract object Clone();

        public override string ToString()
        {
            return $"{Id}: {{ Location: {Location} }}";
        }
    }
}
