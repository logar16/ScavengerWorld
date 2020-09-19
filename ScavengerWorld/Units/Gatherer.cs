using ScavengerWorld.Units.Actions;
using ScavengerWorld.Units.Interfaces;
using ScavengerWorld.World;
using ScavengerWorld.World.Markers;
using System;

namespace ScavengerWorld.Units
{
    public class Gatherer : BasicUnit, ICreator
    {
        //TODO: Any special gatherer abilities should be defined here

        private enum CreationIndex { MARKER }
        private static readonly int CreationIndexCount = Enum.GetValues(typeof(CreationIndex)).Length;

        public bool CanCreate(CreateAction action)
        {
            return action.ActionIndex <= CreationIndexCount;
        }

        public WorldObject Create(CreateAction action)
        {
            if (!CanCreate(action))
                return null;

            switch ((CreationIndex)action.ActionIndex)
            {
                case CreationIndex.MARKER:  
                    return MarkerDefinition.ParseMarker(action.Data, Id);
                default:
                    return null;
            }
        }

    }
}
