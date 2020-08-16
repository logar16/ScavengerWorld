using ScavengerWorld.Sensory;
using System;

namespace ScavengerWorld.World.Food
{
    public class Food: WorldObject
    {
        public SensoryDisplay Display { get; protected set; }

        public int Quantity { get; private set; }
        
        public enum FoodQuality { POOR = 1, FAIR, GOOD, EXCELLENT }
        public FoodQuality Quality { get; }

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


        public Food(int quantity, FoodQuality quality): 
            this(quantity, quality, SensoryDisplay.NONE) { }

        public Food(int quantity, FoodQuality quality, SensoryDisplay sensoryDisplay)
        {
            Quantity = quantity;
            Quality = quality;
            Display = sensoryDisplay;
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

        //TODO: Do we want an Age() method which causes food to go bad over time
        //   if it isn't being properly stored?
    }
}
