using ScavengerWorld.Sensory;
using ScavengerWorld.Units.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ScavengerWorld.World.Foods
{
    public class FoodStorage : WorldObject, IDiscoverable, ISteppable, ITaker
    {
        public int TeamId { get; set; }
        public int Limit { get; }

        private List<Food> Supply;

        /// <summary>
        /// Changes based on content of the supply
        /// </summary>
        public SensoryDisplay Display { get; private set; }

        public FoodStorage(int limit, Point location)
        {
            Supply = new List<Food>(100);
            Limit = limit;
            Move(location);
            Display = new SensoryDisplay();
        }

        public FoodStorage(FoodStorage other)
        {
            Supply = other.Supply.Select(food => (Food)food.Clone()).ToList();

            Limit = other.Limit;
            Move(other.Location);

            Display = (SensoryDisplay)other.Display.Clone();

            TeamId = other.TeamId;
        }

        public bool Add(Food food)
        {
            if (food.Quantity + TotalQuantity() > Limit)
                return false;   //Failed to add

            Supply.Add(food);
            return true;
        }

        public int TotalQuantity()
        {
            return Supply.Sum((food) => food.Quantity);
        }

        public bool IsFull()
        {
            return TotalQuantity() >= Limit;
        }

        public double WeightedQuality()
        {
            var quantity = TotalQuantity();
            if (quantity <= 0)
                return 0;   //Avoid divide by 0

            return Supply.Sum((food) => food.Quantity * (int)food.Quality) / quantity;
        }

        public int TotalQuality()
        {
            return Supply.Sum((food) => (int)food.Quality);
        }

        public void Step(int timeStep)
        {
            //TODO: Determine what the sensory display should be
            //throw new NotImplementedException();
        }

        public override bool ShouldRemove()
        {
            return false;
        }

        public bool Take(ITransferable obj)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            return new FoodStorage(this);
        }

        internal override string DrawAs()
        {
            return "@";
        }
    }
}
