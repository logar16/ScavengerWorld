using ScavengerWorld.Teams;
using ScavengerWorld.World.Foods;
using ScavengerWorld.World.Items;
using System.Collections.Generic;

namespace ScavengerWorld.World.Building
{
    public class WorldBuilder
    {
        private WorldState State;
        public WorldBuilder()
        {
            State = new WorldState();
        }

        public IWorld Build()
        {
            return new FullWorld(State);
        }

        public WorldBuilder WithAmbience(AmbientEnvironment ambience)
        {
            State.Ambience = ambience;
            return this;
        }
        
        public WorldBuilder WithAmbience(int stepsPerSeason, int stepsPerDay)
        {
            State.Ambience = new AmbientEnvironment(stepsPerSeason, stepsPerDay);
            return this;
        }
        
        public WorldBuilder WithGeography(Geography geography)
        {
            State.Geography = geography;
            return this;
        }

        public WorldBuilder WithGeography(int width, int height)
        {
            State.Geography = new Geography(width, height);
            return this;
        }

        public WorldBuilder WithTeams(ICollection<Team> teams)
        {
            State.Teams.AddRange(teams);
            return this;
        }
        public WorldBuilder WithTeams(int numTeams, int teamSize)
        {
            for (int i = 0; i < numTeams; i++)
            {
                var team = new Team(i, teamSize, new FoodStorage(1000));
                State.Teams.Add(team);
            }
            return this;
        }

        public WorldBuilder WithTeam(Team team)
        {
            State.Teams.Add(team);
            return this;
        }

        

        public WorldBuilder WithItems(ICollection<Item> items)
        {
            State.InanimateObjects.AddRange(items);
            return this;
        }

        public WorldBuilder WithFood(ICollection<Food> food)
        {
            State.InanimateObjects.AddRange(food);
            return this;
        }
    }
}
