using ScavengerWorld.Teams;
using System;
using System.Collections.Generic;

namespace ScavengerWorld.World.Building
{
    public class WorldBuilder
    {
        private WorldState State;
        private Random Random;

        public WorldBuilder()
        {
            State = new WorldState();
            Random = new Random();
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
        //public WorldBuilder WithTeams(int numTeams, int teamSize)
        //{
        //    for (int i = 0; i < numTeams; i++)
        //    {
        //        var team = new Team(i, teamSize, new FoodStorage(1000, new Point()));
        //        State.Teams.Add(team);
        //    }
        //    return this;
        //}

        public WorldBuilder WithTeam(Team team)
        {
            State.Teams.Add(team);
            return this;
        }



        //public WorldBuilder WithItems(ICollection<Item> items)
        //{
        //    State.InanimateObjects.AddRange(items);
        //    return this;
        //}

        //public WorldBuilder WithFood(ICollection<Food> food)
        //{
        //    State.InanimateObjects.AddRange(food);
        //    return this;
        //}

        //public WorldBuilder WithFood(double ratioPerUnit)
        //{
        //    var count = 0.0;
        //    foreach (var list in State.Teams)
        //    {
        //        count += list.Units.Count;
        //    }

        //    count *= ratioPerUnit;

        //    var foodList = new List<Food>();
        //    for (int i = 0; i < count; i++)
        //    {
        //        var food = new Food(1, Food.FoodQuality.EXCELLENT);
        //        foodList.Add(food);
        //    }

        //    return WithFood(foodList);
        //}

        //public WorldBuilder WithFood(int count, int averageSize, Food.FoodQuality averageQuality)
        //{
        //    var foodList = new List<Food>();
        //    var qualityRange = Enum.GetValues(typeof(Food.FoodQuality)).Length;
        //    var standardQuality = (int)averageQuality;

        //    for (int i = 0; i < count; i++)
        //    {
        //        var size = Random.Next(averageSize * 2);
        //        var quality = Random.Next(qualityRange);
        //        if (quality != standardQuality)
        //        {
        //            if (Random.Next(4) == 0)
        //            {
        //                if (quality < standardQuality)
        //                    quality += 1;
        //                else
        //                    quality -= 1;
        //            }
        //        }

        //        var food = new Food(size, (Food.FoodQuality)quality);
        //    }

        //    return WithFood(foodList);
        //}
    }
}
