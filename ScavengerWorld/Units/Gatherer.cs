using ScavengerWorld.Units.Actions;
using ScavengerWorld.Units.Interfaces;
using ScavengerWorld.World;
using System;

namespace ScavengerWorld.Units
{
    public class Gatherer : BasicUnit, ICreator
    {
        //TODO: Any special gatherer abilities should be defined here

        public bool CanCreate(CreateAction action)
        {
            throw new NotImplementedException();
        }

        public WorldObject Create(CreateAction action)
        {
            throw new NotImplementedException();
        }

    }
}
