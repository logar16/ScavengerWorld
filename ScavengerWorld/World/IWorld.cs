using ScavengerWorld.Units.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.World
{
    public interface IWorld
    {
        WorldState CurrentState { get;}
        
        WorldState Step(UnitActionCollection actionsToTake, int timeSteps=1);

        WorldState Reset();

        bool IsDone();

        int StepsTaken { get; }

    }
}
