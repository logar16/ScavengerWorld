using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScavengerWorld.Sensory
{
    public class SensoryDisplay: ICloneable
    {
        [JsonProperty("visual")]
        public SensoryFeature Visual { get; private set; }
        [JsonProperty("auditory")]
        public SensoryFeature Auditory { get; private set; }
        [JsonProperty("smell")]
        public SensoryFeature Smell { get; private set; }
        [JsonProperty("taste")]
        public SensoryFeature Taste { get; private set; }

        private List<SensoryFeature> AllFeatures;

        public static readonly SensoryDisplay NONE = new SensoryDisplay();

        public SensoryDisplay() : this(new SensoryFeature(0, 0),
                                      new SensoryFeature(0, 0),
                                      new SensoryFeature(0, 0),
                                      new SensoryFeature(0, 0))
        {

        }

        public SensoryDisplay(SensoryFeature visual, 
                              SensoryFeature auditory, 
                              SensoryFeature smell, 
                              SensoryFeature taste)
        {
            Visual = visual;
            Auditory = auditory;
            Smell = smell;
            Taste = taste;

            AllFeatures = new List<SensoryFeature>()
            {
                Visual,
                Auditory,
                Smell,
                Taste
            };
        }

        public ICollection<SensoryFeature> AsCollection()
        {
            return AllFeatures.ToList();
        }

        public object Clone()
        {
            SensoryFeature visual = (SensoryFeature)Visual.Clone();
            SensoryFeature auditory = (SensoryFeature)Auditory.Clone();
            SensoryFeature smell = (SensoryFeature)Smell.Clone();
            SensoryFeature taste = (SensoryFeature)Taste.Clone();
            return new SensoryDisplay(visual, auditory, smell, taste);
        }
    }
}