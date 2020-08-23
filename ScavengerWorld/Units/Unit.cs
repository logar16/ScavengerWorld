using ScavengerWorld.Sensory;
using ScavengerWorld.Statistics;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;

namespace ScavengerWorld.Units
{
    public abstract class Unit: WorldObject, IDiscoverable, ISteppable
    {
        public int Health { get; protected set; }
        protected int HealthMax { get; set; }
        public int AttackLevel { get; protected set; }
        public int Speed { get; protected set; }
        public int LineOfSight { get; protected set; }
        public SensoryDisplay Display { get; protected set; }

        public UnitStats Stats { get; }

        protected int MissingHealth { get => HealthMax - Health; }

        protected Unit()
        {
            Stats = new UnitStats();
        }

        public override bool ShouldRemove()
        {
            return Health < 0;
        }

        public abstract void Step(int timeStep);

        public abstract void TakeAction(UnitAction action);
    }
}
