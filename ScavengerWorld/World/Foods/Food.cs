using ScavengerWorld.Sensory;
using System;

namespace ScavengerWorld.World.Foods
{
    public class Food : WorldObject, IDiscoverable, ISteppable, ITransferable
    {
        public SensoryDisplay Display { get; protected set; }
        Guid ITransferable.Owner { get; set; }

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
        /// <summary>
        /// Should be set by the outside world so the food knows how fast it should decay
        /// </summary>
        public bool InStorage { get; set; }

        private double Freshness;

        public Food(int quantity, FoodQuality quality) :
            this(quantity, quality, SensoryDisplay.NONE)
        { }

        public Food(int quantity, FoodQuality quality, SensoryDisplay sensoryDisplay)
        {
            Quantity = quantity;
            Quality = quality;
            Display = sensoryDisplay;
            ResetFreshness();
        }

        private void ResetFreshness()
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
            return Quantity <= 0 || (Freshness < 0 && Quality == FoodQuality.POOR);
        }

        public void Step(int timeStep)
        {
            Decay(timeStep);
        }

        /// <summary>
        /// Being in storage impacts decay rate
        /// </summary>
        /// <param name="timeStep"></param>
        private void Decay(int timeStep)
        {
            var modifier = InStorage ? 0.25 : 1.0;
            Freshness -= timeStep * modifier;
            if (Freshness < 0 && (int)Quality > 1)
            {
                Quality = (FoodQuality)((int)Quality - 1);
                ResetFreshness();
            }
        }

        internal override string DrawAs()
        {
            return "*";
        }

        public override object Clone()
        {
            Food copy = (Food)MemberwiseClone();
            copy.Display = (SensoryDisplay)Display.Clone();

            return copy;
        }

    }
}
