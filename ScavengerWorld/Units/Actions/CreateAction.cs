using Newtonsoft.Json.Linq;

namespace ScavengerWorld.Units.Actions
{
    public class CreateAction : UnitAction
    {
        public int ActionIndex { get; private set; }

        public JObject Data { get; private set; }

        public CreateAction(int actionIndex) : this(actionIndex, new JObject()) { }

        public CreateAction(int actionIndex, JObject data)
        {
            ActionIndex = actionIndex;
            Data = data;
        }
    }
}
