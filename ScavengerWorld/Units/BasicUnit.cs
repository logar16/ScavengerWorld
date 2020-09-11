﻿using ScavengerWorld.Teams;
using ScavengerWorld.Units.Actions;
using ScavengerWorld.Units.Interfaces;
using ScavengerWorld.World;
using ScavengerWorld.World.Foods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScavengerWorld.Units
{
    public abstract class BasicUnit : Unit, ITaker, IDropper
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

        public override bool CanAttemptAction(UnitAction action)
        {
            if (base.CanAttemptAction(action))
                return true;

            if (action is TransferAction transfer)
            {
                var id = transfer.ObjectId;
                var obj = FoodSupply.FirstOrDefault(f => f.Id == id);
                return obj != null;
            }
            
            return false;
        }

        public virtual bool Take(ITransferable obj)
        {
            if (obj is Food food)
            {
                Gather(food);
                return true;
            }

            return false;
        }

        public bool Gather(Food food)
        {
            if (food.Quantity + TotalFoodQuantity > GatherLimit)
                return false;

            FoodSupply.Add(food);
            return true;
        }

        public virtual bool Drop(ITransferable obj)
        {
            if (obj is Food food)
            {
                DropFood(food);
                return true;
            }
            return false;
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


        #region SpecialDropFoodMethods

        public Food DropFood(Food food)
        {
            return FoodSupply.Remove(food) ? food : null;
        }

        //TODO: should this be changed to drop the largest of this quality?  
        //Make lots of room fast by dropping a big POOR quality piece
        public Food DropFood(Food.FoodQuality quality)
        {
            var min = FoodSupply.Where(food => food.Quality == quality)
                                .Aggregate((f1, f2) => f1.Quantity < f2.Quantity ? f1 : f2);
            return DropFood(min);
        }

        /// <summary>
        /// Drops the least effective piece of food
        /// </summary>
        /// <returns></returns>
        public Food DropFood()
        {
            Food min = FoodSupply.Aggregate((f1, f2) => f1.Effectiveness < f2.Effectiveness ? f1 : f2);
            return DropFood(min);
        }

        #endregion SpecialDropFoodMethods

        public override object Clone()
        {
            BasicUnit copy = (BasicUnit)base.Clone();
            copy.FoodSupply = new List<Food>();
            foreach (var food in FoodSupply)
            {
                copy.FoodSupply.Add((Food)food.Clone());
            }
            return copy;
        }

        
    }
}
