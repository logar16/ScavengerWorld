using ScavengerWorld.Teams;
using ScavengerWorld.Units;
using System
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.World
{
    class FullWorld: IWorld
    {
        public WorldState Step(UnitActions actionsToTake)
        {
            throw new NotImplementedException();
        }

        public WorldState CurrentState { get; }
    }
}
