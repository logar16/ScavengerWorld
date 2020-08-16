namespace ScavengerWorld.Sensory
{
    public class SensoryDisplay
    {
        public SensoryFeature Visual { get; }
        public SensoryFeature Auditory { get; }
        public SensoryFeature Smell { get; }
        public SensoryFeature Taste { get; }

        public static readonly SensoryDisplay NONE = new SensoryDisplay();

        public SensoryDisplay(SensoryFeature visual, 
                              SensoryFeature auditory, 
                              SensoryFeature olfactory, 
                              SensoryFeature gustatory)
        {
            Visual = visual;
            Auditory = auditory;
            Smell = olfactory;
            Taste = gustatory;
        }

        private SensoryDisplay()
        {
            Visual = new SensoryFeature(0);
            Auditory = new SensoryFeature(0);
            Smell = new SensoryFeature(0);
            Taste = new SensoryFeature(0);
        }
    }
}