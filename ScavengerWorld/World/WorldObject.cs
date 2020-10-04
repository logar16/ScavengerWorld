using Newtonsoft.Json;
using System;
using System.Drawing;

namespace ScavengerWorld.World
{
    public abstract class WorldObject : ICloneable
    {
        public virtual Point Location { get; private set; }
        public virtual Guid Id { get; private set; }
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

        /// <summary>
        /// Moves the point by the amount specified in the point's X and Y.
        /// It does not move to the specified point's location.  
        /// </summary>
        /// <param name="direction"></param>
        public virtual void Move(Point direction)
        {
            Move(direction.X, direction.Y);
        }

        /// <summary>
        /// The amount of spaces to move the object in the X and Y directions.
        /// Negative values move it up and left, positive values down and right.
        /// </summary>
        /// <param name="xDelta"></param>
        /// <param name="yDelta"></param>
        public virtual void Move(int xDelta, int yDelta)
        {
            var location = Location;
            location.Offset(xDelta, yDelta);
            Location = location;
        }

        public virtual void MoveTo(Point point)
        {
            if (point.X < 0)
                point.X = Location.X;
            if (point.Y < 0)
                point.Y = Location.Y;

            Location = point;
        }

        public double DistanceTo(WorldObject other)
        {
            if (other == null)
                throw new ArgumentNullException("Distance cannot be determined from a null input `other`");

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

        /// <summary>
        /// Will be called when the object is being removed so it can clean up any references
        /// or record any data it needs before going to the graveyard.
        /// </summary>
        public virtual void Remove()
        {

        }

        public abstract object Clone();

        public override string ToString()
        {
            return $"{IdPrefix}: {{ Location: {Location} }}";
        }

        /// <summary>
        /// Method which allows the printer object to display the object in a discernable way.
        /// Default is "X", but each object should use a different character to distinguish it visually.
        /// </summary>
        /// <returns></returns>
        virtual internal string DrawAs()
        {
            return "X";
        }
    }
}
