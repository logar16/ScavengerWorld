using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.World.Foods
{
    class FoodGenerator
    {
        private Random Random;

        public FoodGenerator()
        {
            Random = new Random();
        }

        public Food Next()
        {
            var next = new Food(Random.Next(1, 5), RandomFoodQuality());
            return next;
        }

        private Food.FoodQuality RandomFoodQuality()
        {
            var values = Enum.GetValues(typeof(Food.FoodQuality));
            int index = Random.Next(values.Length - 1);
            return (Food.FoodQuality)values.GetValue(index);
        }
    }
}
