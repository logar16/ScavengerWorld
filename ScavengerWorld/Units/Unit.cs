using Newtonsoft.Json;
using ScavengerWorld.Sensory;
using ScavengerWorld.Statistics;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using System;

namespace ScavengerWorld.Units
{
    public abstract class Unit : WorldObject, IDiscoverable, ISteppable, ICloneable
    {
        [JsonProperty("speed")]
        public int Speed { get; protected set; }

        [JsonProperty("lineOfSight")]
        public int LineOfSight { get; protected set; }

        [JsonProperty("sensoryDisplay")]
        virtual public SensoryDisplay Display { get; protected set; }

        public UnitStats Stats { get; }

        public Guid Team { get; set; }


        protected Unit()
        {
            Display = new SensoryDisplay();
            Stats = new UnitStats();
            HealthMax = Health;
        }

        public virtual void Step(int timeStep)
        {
            //Nothing to do as of now...
        }

        public virtual bool CanAttemptAction(UnitAction action)
        {
            return action is NoopAction;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            //TODO: Record stats
        }


        #region HelperFunctions
        public override string ToString()
        {
            return $"{IdPrefix}: {{ Unit: {GetType().Name}, Location: ({Location}), {Details()} }}";
        }

        protected string Details()
        {
            return $"Health: {Health}";
        }

        internal override string DrawAs()
        {
            return "U";
        }

        public override object Clone()
        {
            Unit copy = (Unit)MemberwiseClone();
            copy.Display = (SensoryDisplay)Display.Clone();
            return copy;
        }

        virtual public void ResetDisplay()
        {
            Display.Reset();
        }

        #endregion HelperFunctions
    }
}
