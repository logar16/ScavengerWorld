﻿using ScavengerWorld.Sensory;
using ScavengerWorld.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.World.Food
{
    public class FoodStorage: WorldObject, IDisplayable, ISteppable
    {
        public Team Owner { get; set; }
        public int Limit { get; }
        
        private List<Food> Supply;

        /// <summary>
        /// Changes based on content of the supply
        /// </summary>
        public SensoryDisplay Display { get; private set; }

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
            throw new NotImplementedException();
        }

        public override bool ShouldRemove()
        {
            return false;
        }
    }
}
