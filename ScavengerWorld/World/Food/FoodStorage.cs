using ScavengerWorld.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.World.Food
{
    class FoodStorage: WorldObject
    {
        public Team Owner { get; set; }
        public int Limit { get; }
        private List<Food> Supply;

        public FoodStorage(int limit)
        {
            Supply = new List<Food>(100);
            Limit = limit;
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

        public double AverageQuality()
        {
            var quantity = TotalQuantity();
            if (quantity <= 0)
                return 0;

            return Supply.Sum((food) => food.Quantity * (int)food.Quality) / quantity;
        }
    }
}
