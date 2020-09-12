using ScavengerWorld.Units.Actions;
using ScavengerWorld.Units.Interfaces;
using ScavengerWorld.World;
using ScavengerWorld.World.Foods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScavengerWorld.Units
{
    public abstract class BasicUnit : Unit, ITaker, IDropper, IAttacker
    {
        public double GatherRate { get; protected set; }
        public double GatherLimit { get; protected set; }
        
        private List<Food> FoodSupply;

        public int TotalFoodQuantity
        {
            get => FoodSupply.Sum(food => food.Quantity);
        }
        public int AttackLevel { get; }

        public BasicUnit()
        {
            FoodSupply = new List<Food>();
        }

        public override bool CanAttemptAction(UnitAction action)
        {
            if (base.CanAttemptAction(action))
                return true;

            //Have to own to give
            if (action is GiveAction give)
            {
                var id = give.ObjectId;
                var food = FoodSupply.FirstOrDefault(f => f.Id == id);
                return food != null;
            }
            else if (action is TransferAction)
            {
                return CanTakeFood(1);  //If not too full of food already, can attempt the action at least
            }

            //Can always move
            return action is MoveAction || action is AttackAction;
        }

        public virtual bool Take(ITransferable obj)
        {
            if (obj is Food food)
                return Gather(food);

            return false;
        }

        private bool Gather(Food food)
        {
            if (!CanTakeFood(food.Quantity))
                return false;

            if (FoodSupply.Any(f => f.Id == food.Id))
                return false;

            FoodSupply.Add(food);
            return true;
        }

        private bool CanTakeFood(int quantity)
        {
            return quantity + TotalFoodQuantity <= GatherLimit;
        }

        public virtual bool Drop(ITransferable obj)
        {
            if (obj is Food food)
            {
                return DropFood(food);
            }
            return false;
        }


        public virtual bool CanAttack()
        {
            return true;
        }

        /// <summary>
        /// Called whenever a unit launches an attack.
        /// Use this to record metrics or otherwise impact the unit
        /// </summary>
        public void Attack()
        {
            //TODO: record stat
        }

        #region Future Abilities?
        public void Eat()
        {
            var food = FoodSupply.Last();
            Heal(food.Effectiveness);
        }

        public void Heal(int healthPoints)
        {
            Health = Math.Min(Health + healthPoints, HealthMax);
        }

        #endregion Future Abilities?


        #region SpecialDropFoodMethods

        public bool DropFood(Food food)
        {
            return FoodSupply.Remove(food);
        }

        //TODO: should this be changed to drop the largest of this quality?  
        //Make lots of room fast by dropping a big POOR quality piece
        public Food DropFood(Food.FoodQuality quality)
        {
            var min = FoodSupply.Where(food => food.Quality == quality)
                                .Aggregate((f1, f2) => f1.Quantity < f2.Quantity ? f1 : f2);
            return DropFood(min) ? min : null;
        }

        /// <summary>
        /// Drops the least effective piece of food
        /// </summary>
        /// <returns></returns>
        public Food DropFood()
        {
            Food min = FoodSupply.Aggregate((f1, f2) => f1.Effectiveness < f2.Effectiveness ? f1 : f2);
            return DropFood(min) ? min : null;
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
