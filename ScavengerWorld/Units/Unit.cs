using Newtonsoft.Json;
using ScavengerWorld.Sensory;
using ScavengerWorld.Statistics;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using System;

namespace ScavengerWorld.Units
{
    public abstract class Unit: WorldObject, IDiscoverable, ISteppable, ICloneable
    {
        [JsonProperty("attack")]
        public int AttackLevel { get; protected set; }
        
        [JsonProperty("speed")]
        public int Speed { get; protected set; }

        [JsonProperty("lineOfSight")]
        public int LineOfSight { get; protected set; }

        [JsonProperty("sensoryDisplay")]
        public SensoryDisplay Display { get; protected set; }

        public UnitStats Stats { get; }


        protected Unit()
        {
            Display = new SensoryDisplay();
            Stats = new UnitStats();
            HealthMax = Health;
        }

        public override bool ShouldRemove()
        {
            return Health < 0;
        }

        public virtual void Step(int timeStep)
        {
            //Nothing to do as of now...
        }

        public virtual bool CanAttemptAction(UnitAction action)
        {
            return action == UnitAction.NOOP || action is AttackAction;
        }

        public void Attack(WorldObject target)
        {
            //TODO: record stat
            target.Injure(AttackLevel);
        }



        public override string ToString()
        {
            return $"{IdPrefix}: {{ Unit: {GetType().Name}, Location: ({Location}), {Details()} }}";
        }

        protected string Details()
        {
            return $"Health: {Health}";
        }

        public override object Clone()
        {
            Unit copy = (Unit)MemberwiseClone();
            copy.Display = (SensoryDisplay)Display.Clone();
            return copy;
        }
    }
}
