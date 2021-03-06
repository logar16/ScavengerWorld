﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScavengerWorld.Teams;
using ScavengerWorld.Units;
using ScavengerWorld.World.Foods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ScavengerWorld.World.Building
{
    public class JsonWorldBuilder
    {
        private JObject UnitConfig;
        private Random Random;
        private WorldState State;

        public JsonWorldBuilder()
        {
            Random = new Random();
            State = new WorldState();
        }

        /// <summary>
        /// Load the configuration file (may require loading other configuration files in concert)
        /// Note that if a file is not found, it may be because of copy properties on the file have not been properly set.
        /// </summary>
        /// <param name="file">Local filename.  Probably "Config/Worlds/filename.json"  (include file-type suffix)</param>
        /// <returns>An IWorld object that is defined by the configuration file passed into the method</returns>
        public IWorld LoadWorldFile(string file)
        {
            var config = JObject.Parse(File.ReadAllText(file));

            State = new WorldState();
            State.Ambience = ParseAmbiance(config);
            State.Geography = ParseGeography(config);
            State.Teams = ParseTeams(config);
            var objects = new Dictionary<Guid, WorldObject>();
            AppendWorldObjects(objects, State.Units);
            AppendWorldObjects(objects, State.Teams.Select(t => t.FoodStorage));
            AppendWorldObjects(objects, ParseFood(config));
            //TODO: Parse Items
            //AppendWorldObjects(objects, ParseItems(config));

            State.Objects = objects;

            var world = new FullWorld(State);
            return world;
        }

        private static void AppendWorldObjects(Dictionary<Guid, WorldObject> objectMap, IEnumerable<WorldObject> others)
        {
            foreach (var obj in others)
            {
                if (obj != null)
                    objectMap.Add(obj.Id, obj);
            }
        }

        private List<WorldObject> ParseItems(JObject config)
        {
            throw new NotImplementedException();
        }

        private List<Food> ParseFood(JObject config)
        {
            config = config.Value<JObject>("food");
            var ratio = config.Value<double>("ratio");
            var count = (State.TotalUnits * ratio) + 1;

            var size = config.Value<int>("averageSize");
            var quality = Food.FoodQuality.EXCELLENT;
            Enum.TryParse(config.Value<string>("averageQuality"), out quality);

            var height = State.Geography.Height;
            var width = State.Geography.Width;

            var foods = new List<Food>();
            for (int i = 0; i < count; i++)
            {
                var food = new Food(size, quality);
                var location = new Point(Random.Next(width), Random.Next(height));
                food.Move(location);
                foods.Add(food);
            }

            return foods;
        }

        private List<Team> ParseTeams(JObject config)
        {
            var file = config.Value<string>("unitConfig");
            UnitConfig = JObject.Parse(File.ReadAllText(file));

            var teams = new List<Team>();
            var teamsConfig = config.Value<JArray>("teams");
            var i = 0;
            foreach (JObject teamConfig in teamsConfig)
            {
                FoodStorage storage = null;
                if (teamConfig.ContainsKey("storage"))
                {
                    var storageConfig = teamConfig.Value<JObject>("storage");
                    var location = storageConfig.ToObject<Location>().ToPoint();
                    var limit = storageConfig.Value<int>("limit");
                    storage = new FoodStorage(limit, location);
                }

                var home = storage?.Location ?? new Point(-1, -1);

                var units = new List<Unit>();
                string path = teamConfig.Value<string>("units");
                var teamFile = JObject.Parse(File.ReadAllText(path));
                var unitsList = teamFile.Value<JArray>("units");
                foreach (JObject unit in unitsList)
                {
                    units.AddRange(ParseUnits(unit, home));
                }

                var team = (storage != null) ? new Team(i, units, storage) : new Team(i, units);
                teams.Add(team);
                i++;
            }

            return teams;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitConfig">Configuration entry</param>
        /// <param name="home">General location to put the new units (like close to food storage).  
        /// If the X is set to a negative value, units will be placed randomly throughout</param>
        /// <returns></returns>
        private List<Unit> ParseUnits(JObject unitConfig, Point home)
        {
            var name = unitConfig.Value<string>("type");
            var properties = UnitConfig.Value<JObject>(name);

            name = "ScavengerWorld.Units." + name;
            Assembly asm = typeof(Unit).Assembly;
            Type type = asm.GetType(name);

            var count = unitConfig.Value<int>("count");
            var units = new List<Unit>(count);
            var width = State.Geography.Width;
            var height = State.Geography.Height;

            var randomLocation = (home.X < 0);

            for (int i = 0; i < count; i++)
            {
                Unit unit = (Unit)JsonConvert.DeserializeObject(properties.ToString(), type);
                if (randomLocation)
                {
                    unit.Move(Random.Next(width), Random.Next(height));
                }
                else
                {
                    //Put units around food storage to begin
                    int next_x = (int)Math.Min(width - 1, home.X + Random.Next(5) - 2);
                    int next_y = (int)Math.Min(height - 1, home.Y + Random.Next(5) - 2);
                    unit.Move(next_x, next_y);
                }

                units.Add(unit);
            }

            return units;
        }

        private Geography ParseGeography(JObject definition)
        {
            var geo = definition.Value<JObject>("geography");
            int width = geo.Value<int>("width");
            int height = geo.Value<int>("height");
            double ratioWalkable = geo.Value<double>("ratioWalkable");
            int seed = geo.ContainsKey("seed") ? geo.Value<int>("seed") : -1;
            return new Geography(width, height, ratioWalkable, seed);
        }

        private AmbientEnvironment ParseAmbiance(JObject definition)
        {
            var ambience = definition.Value<JObject>("ambience");
            int stepsPerSeason = ambience.Value<int>("stepsPerSeason");
            int stepsPerDay = ambience.Value<int>("stepsPerDay");
            return new AmbientEnvironment(stepsPerSeason, stepsPerDay);
        }

        private class Location
        {
            [JsonProperty("x")]
            int X { get; set; }
            [JsonProperty("y")]
            int Y { get; set; }

            public Point ToPoint()
            {
                return new Point(X, Y);
            }
        }
    }
}
