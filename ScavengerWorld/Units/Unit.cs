using ScavengerWorld.Sensory;
using ScavengerWorld.Statistics;
using ScavengerWorld.World;

namespace ScavengerWorld.Units
{
    public abstract class Unit: WorldObject, IDisplayable, ISteppable
    {
        public int Health { get; protected set; }
        protected int HealthMax { get; set; }
        public int Attack { get; protected set; }
        public int Speed { get; protected set; }
        public int LineOfSight { get; protected set; }
        public SensoryDisplay Display { get; protected set; }

        public UnitStats Stats { get; }

        protected Unit()
        {
            Stats = new UnitStats();
        }

        protected int MissingHealth()
        {
            return HealthMax - Health;
        }

        public override bool ShouldRemove()
        {
            return Health < 0;
        }

        public abstract void Step(int timeStep);
    }
}
