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
        [JsonProperty("health")]
        public int Health { get; protected set; }
        protected int HealthMax { get; set; }
        protected int MissingHealth { get => HealthMax - Health; }

        [JsonProperty("attack")]
        public int AttackLevel { get; protected set; }
        
        [JsonProperty("speed")]
        public int Speed { get; protected set; }

        [JsonProperty("lineOfSight")]
        public int LineOfSight { get; protected set; }
        public SensoryDisplay Display { get; protected set; }

        public UnitStats Stats { get; }


        protected Unit()
        {
            Stats = new UnitStats();
            HealthMax = Health;
        }

        public override bool ShouldRemove()
        {
            return Health < 0;
        }

        public abstract void Step(int timeStep);

        public abstract void TakeAction(UnitAction action);
        public abstract object Clone();

        public override string ToString()
        {
            var id = Id.ToString().Substring(0, 4);
            return $"{id}: {{ Unit: {GetType().Name}, Location: ({Location}), {Details()} }}";
        }

        protected string Details()
        {
            return $"Health: {Health}";
        }
    }
}
