﻿using ScavengerWorld.Units.Actions;
using System;

namespace ScavengerWorld.Units
{
    /// <summary>
    /// A unit which roams about, attacking other units
    /// </summary>
    public class Monster : Unit
    {

        public override void Step(int timeStep)
        {
            throw new NotImplementedException();
        }

        public override bool CanAttemptAction(UnitAction action)
        {
            throw new NotImplementedException();
        }
    }
}
