using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.World
{
    /// <summary>
    /// This interface should be implemented by any object that needs to be stepped 
    /// as part of the simulation.
    /// </summary>
    interface ISteppable
    {
        void Step(int timeStep);
    }
}
