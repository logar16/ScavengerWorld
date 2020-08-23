using ScavengerWorld.Teams;
using System;
using System.Collections.Generic;

namespace ScavengerWorld.World
{
    public class WorldState : ISteppable
    {
        public AmbientEnvironment Ambience { get; internal set; }
        public Geography Geography { get; internal set; }
        public List<Team> Teams { get; internal set; }
        public List<WorldObject> InanimateObjects;

        public WorldState()
        {
            Teams = new List<Team>();
            InanimateObjects = new List<WorldObject>();
        }

        internal WorldState Clone()
        {
            throw new NotImplementedException();
        }

        private List<WorldObject> DestroyedObjects;

        public void Step(int timeStep)
        {
            Ambience.Step(timeStep);
        }
    }
}