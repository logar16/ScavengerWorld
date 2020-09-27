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
        IEnumerable<Team> Teams { get; }
        IEnumerable<Unit> Units { get; }
        Dictionary<Guid, WorldObject> Objects { get; }

        WorldObject FindObject(Guid guid);
        void Add(WorldObject obj);
        void Destroy(WorldObject obj);
        void Destroy(Guid id);
    }
}