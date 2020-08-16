using ScavengerWorld.Items;
using ScavengerWorld.Teams;
using System.Collections.Generic;

namespace ScavengerWorld.World
{
    public class WorldState
    {
        private AmbientEnvironment Ambience;
        private Geography Geography;
        private List<Team> Teams;
        private List<WorldObject> Objects;
        private List<WorldObject> DestroyedObjects;
    }
}