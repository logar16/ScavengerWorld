using ScavengerWorld.Sensory;
using ScavengerWorld.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units
{
    class Unit: WorldObject
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

        public bool IsAlive()
        {
            return Health > 0;
        }

        protected int MissingHealth()
        {
            return HealthMax - Health;
        }
    }
}
