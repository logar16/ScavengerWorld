﻿using ScavengerWorld.World;
using ScavengerWorld.World.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerWorld.Units.Interfaces
{
    /// <summary>
    /// Can pick things up off the ground or take receive things from other units
    /// </summary>
    public interface ITaker
    {
        /// <summary>
        /// Instruct the unit to take the object if possible
        /// </summary>
        /// <param name="obj">The object to be taken</param>
        /// <returns>Will return true if able to take the object or false otherwise.</returns>
        bool Take(ITransferable obj);
    }
}
