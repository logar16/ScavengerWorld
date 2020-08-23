namespace ScavengerWorld.Units.Actions
{
    public class UnitAction
    {
        internal static readonly UnitAction NONE = new UnitAction(0);

        public int ActionIndex { get; set; }

        public UnitAction(int index)
        {
            ActionIndex = index;
        }
    }
}