using ScavengerWorld.Items;
using ScavengerWorld.Teams;
using ScavengerWorld.World.Food;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScavengerWorld.Units
{
    class BasicUnit: Unit
    {
        public double GatherRate { get; protected set; }
        public double GatherLimit { get; protected set; }
        private List<Food> FoodSupply;

        public int TotalFoodQuantity
        {
            get => FoodSupply.Sum(food => food.Quantity);
        }

        public Team Team { get; }

        public BasicUnit()
        {
            FoodSupply = new List<Food>();
        }

        public bool Gather(Food food)
        {
            if (food.Quantity + TotalFoodQuantity > GatherLimit)
                return false;

            FoodSupply.Add(food);
            return true;
        }

        public Food Drop(Food food)
        {
            return FoodSupply.Remove(food) ? food : null;
        }

        //TODO: should this be changed to drop the largest of this quality?  
        //Make lots of room fast by dropping a big POOR quality piece
        public Food Drop(Food.FoodQuality quality)
        {
            var min = FoodSupply.Where(food => food.Quality == quality)
                                .Aggregate((f1, f2) => f1.Quantity < f2.Quantity ? f1 : f2);
            return Drop(min);
        }

        /// <summary>
        /// Drops the least effective piece of food
        /// </summary>
        /// <returns></returns>
        public Food Drop()
        {
            Food min = FoodSupply.Aggregate((f1, f2) => f1.Effectiveness < f2.Effectiveness ? f1 : f2);
            return Drop(min);
        }

        public void Eat()
        {
            var food = FoodSupply.Last();
            Heal(food.Effectiveness);
        }

        public void Heal(int healthPoints)
        {
            Health = Math.Min(Health + healthPoints, HealthMax);
        }

        public void Injure(int attack)
        {
            Health -= attack;
        }

    }
}
