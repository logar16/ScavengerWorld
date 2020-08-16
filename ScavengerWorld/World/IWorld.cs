using ScavengerWorld.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.World
{
    interface IWorld
    {
        WorldState Step(UnitActions actionsToTake);

        WorldState CurrentState { get;}


    }
}
