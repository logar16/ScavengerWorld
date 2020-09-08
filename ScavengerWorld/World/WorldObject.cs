using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public string IdPrefix { get => Id.ToString().Substring(0, 4); }

        [JsonProperty("hp")]
        public int Health { get; protected set; }
        protected int HealthMax { get; set; }
        protected int MissingHealth { get => HealthMax - Health; }

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

        public double DistanceTo(WorldObject other)
        {
            return DistanceTo(other.Location);
        }

        public double DistanceTo(Point point)
        {
            var xdiff = Math.Pow(Location.X - point.X, 2);
            var ydiff = Math.Pow(Location.Y - point.Y, 2);
            return Math.Sqrt(xdiff + ydiff);
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public virtual bool ShouldRemove()
        {
            return Health < 0;
        }

        public abstract object Clone();

        public override string ToString()
        {
            return $"{IdPrefix}: {{ Location: {Location} }}";
        }
    }
}
