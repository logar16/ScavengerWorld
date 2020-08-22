using System.Collections.Generic;
using System.Linq;

namespace ScavengerWorld.Sensory
{
    public class SensoryDisplay
    {
        public SensoryFeature Visual { get; private set; }
        public SensoryFeature Auditory { get; private set; }
        public SensoryFeature Smell { get; private set; }
        public SensoryFeature Taste { get; private set; }

        private List<SensoryFeature> AllFeatures;

        public static readonly SensoryDisplay NONE = CreateNone();

        public SensoryDisplay(SensoryFeature visual, 
                              SensoryFeature auditory, 
                              SensoryFeature olfactory, 
                              SensoryFeature gustatory)
        {
            Visual = visual;
            Auditory = auditory;
            Smell = olfactory;
            Taste = gustatory;

            AllFeatures = new List<SensoryFeature>()
            {
                Visual,
                Auditory,
                Smell,
                Taste
            };
        }

        private static SensoryDisplay CreateNone()
        {
            return new SensoryDisplay(new SensoryFeature(0),
                                      new SensoryFeature(0),
                                      new SensoryFeature(0),
                                      new SensoryFeature(0));
        }

        public ICollection<SensoryFeature> AsCollection()
        {
            return AllFeatures.ToList();
        }
    }
}