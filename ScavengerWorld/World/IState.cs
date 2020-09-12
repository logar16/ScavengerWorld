using ScavengerWorld.Teams;
using ScavengerWorld.Units;
using ScavengerWorld.World.Foods;
using System;
using System.Collections.Generic;

namespace ScavengerWorld.World
{
    public interface IState
    {
        AmbientEnvironment Ambience { get; }
        Geography Geography { get; }
        List<Team> Teams { get; }
        Dictionary<Guid, Unit> AllUnits { get; }
        Dictionary<Guid, Food> Food { get; }
        Dictionary<Guid, WorldObject> InanimateObjects { get; }

        WorldObject FindObject(Guid guid);
        void Destroy(WorldObject obj);
        void Destroy(Guid id);
    }
}