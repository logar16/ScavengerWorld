using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScavengerWorld
{
    public abstract class WorldObject
    {
        public Point Location { get; }
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
            Location.Offset(xDelta, yDelta);
        }

        public abstract bool ShouldRemove();
    }
}
