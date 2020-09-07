using ScavengerWorld.Sensory;
using System;

namespace ScavengerWorld.World.Foods
{
    public class Food : WorldObject, IDiscoverable, ISteppable
    {
        public SensoryDisplay Display { get; protected set; }
        //public FoodStorage Storage { get; set; }   //Not sure if the food needs to know where it is.  Maybe someday...

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

        private double Freshness;

        public Food(int quantity, FoodQuality quality) :
            this(quantity, quality, SensoryDisplay.NONE)
        { }

        public Food(int quantity, FoodQuality quality, SensoryDisplay sensoryDisplay)
        {
            Quantity = quantity;
            Quality = quality;
            Display = sensoryDisplay;
            ResetHealth();
        }

        private void ResetHealth()
        {
            Freshness = 1000 / (int)Quality;
        }

        public int Consume(int quantityConsumed)
        {
            Quantity -= quantityConsumed;
            if (Quantity < 0)
                throw new ArgumentException("quantityConsumed was greater than remaining quantity");
            return Quantity;
        }

        public override bool ShouldRemove()
        {
            return Freshness < 0 && Quality == FoodQuality.POOR;
        }

        public void Step(int timeStep)
        {
            //if (Storage != null)
            //TODO: Should storage just reduce impact of decay?
            Decay(timeStep);
        }

        private void Decay(int timeStep)
        {
            Freshness -= timeStep;
            if (Freshness < 0)
                if ((int)Quality > 1)
                {
                    Quality = (FoodQuality)((int)Quality - 1);
                    ResetHealth();
                }
        }

        public override object Clone()
        {
            Food copy = (Food)MemberwiseClone();
            copy.Display = (SensoryDisplay)Display.Clone();

            return copy;
        }
    }
}
