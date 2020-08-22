using ScavengerWorld.Sensory;
using System;

namespace ScavengerWorld.World.Food
{
    public class Food: WorldObject, IDisplayable, ISteppable
    {
        public SensoryDisplay Display { get; protected set; }
        public FoodStorage Storage { get; set; }

        public int Quantity { get; private set; }
        
        public enum FoodQuality { POOR = 1, FAIR, GOOD, EXCELLENT }
        public FoodQuality Quality { get; private set; }

        /// <summary>
        /// A metric for how much food there is and how good it is.
        /// Essentially, lots of really bad food is not as good as 
        /// a few really good food, hence the square factor on quality.
        /// Quantity * Quality^2
        /// </summary>
        public int Effectiveness
        {
            get => Quantity * (int)Quality * (int)Quality;
        }

        private double Health;

        public Food(int quantity, FoodQuality quality): 
            this(quantity, quality, SensoryDisplay.NONE) { }

        public Food(int quantity, FoodQuality quality, SensoryDisplay sensoryDisplay)
        {
            Quantity = quantity;
            Quality = quality;
            Display = sensoryDisplay;
            ResetHealth();
        }

        private void ResetHealth()
        {
            Health = 1000 / (int)Quality;
        }

        public int Consume(int quantityConsumed)
        {
            Quantity -= quantityConsumed;
            if (Quantity < 0)
            {
                throw new ArgumentException("quantityConsumed was greater than remaining quantity");
            }
            return Quantity;
        }

        public override bool ShouldRemove()
        {
            return (Health < 0 && Quality == FoodQuality.POOR);
        }

        public void Step(int timeStep)
        {
            if (Storage != null)
            {
                //TODO: Should storage just reduce impact of decay?
                Decay(timeStep);
            }
        }

        private void Decay(int timeStep)
        {
            Health -= timeStep;
            if (Health < 0)
            {
                if ((int)Quality > 1)
                {
                    Quality = (FoodQuality)((int)Quality - 1);
                    ResetHealth();
                }
                //else if Quality == POOR, it will be removed from the world
            }
        }
    }
}
