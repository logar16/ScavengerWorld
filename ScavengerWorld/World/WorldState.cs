using ScavengerWorld.Teams;
using ScavengerWorld.Units;
using ScavengerWorld.World.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScavengerWorld.World
{
    public class WorldState : IState, ISteppable
    {
        public AmbientEnvironment Ambience { get; internal set; }
        public Geography Geography { get; internal set; }
        public List<Team> Teams { get; internal set; }
        public Dictionary<Guid, Unit> AllUnits { get; internal set; }
        public int TotalUnits { get { return Teams.Sum(t => t.UnitCount); } }

        public Dictionary<Guid, Food> Food { get; internal set; }
        public Dictionary<Guid, WorldObject> InanimateObjects { get; internal set; }

        private List<WorldObject> DestroyedObjects;

        public WorldState()
        {
            Teams = new List<Team>();
            Food = new Dictionary<Guid, Food>();
            InanimateObjects = new Dictionary<Guid, WorldObject>();

            DestroyedObjects = new List<WorldObject>();
        }

        internal WorldState Clone()
        {
            var copy = new WorldState();
            copy.Ambience = (AmbientEnvironment)Ambience.Clone();
            copy.Geography = (Geography)Geography.Clone();

            copy.Teams = Teams.Select(team => new Team(team)).ToList();
            copy.AllUnits = copy.Teams.SelectMany(t => t.Units)
                                      .ToDictionary(unit => unit.Id, unit => unit);
            copy.Food = Food.ToDictionary(entry => entry.Key, entry => (Food)entry.Value.Clone());
            copy.InanimateObjects = InanimateObjects.ToDictionary(entry => entry.Key, entry => (WorldObject)entry.Value.Clone());

            return copy;
        }


        public void Step(int timeStep)
        {
            Ambience.Step(timeStep);

            foreach (var unit in AllUnits.Values)
            {
                unit.Step(timeStep);
            }

            foreach (var food in Food.Values)
            {
                food.Step(timeStep);
            }

            foreach (var obj in InanimateObjects.Values)
            {
                if (obj is ISteppable stepper)
                    stepper.Step(timeStep);
            }
        }

        public Unit GetUnit(Guid unitGuid)
        {
            AllUnits.TryGetValue(unitGuid, out Unit unit);
            return unit;
        }

        public WorldObject FindObject(Guid objectId)
        {
            if (AllUnits.TryGetValue(objectId, out Unit unit))
            {
                return unit;
            }
            if (Food.TryGetValue(objectId, out Food food))
            {
                return food;
            }
            if (InanimateObjects.TryGetValue(objectId, out WorldObject obj))
            {
                return obj;
            }

            var storage = Teams.FirstOrDefault(t => t.FoodStorage.Id == objectId)?.FoodStorage;
            if (storage != null)
                return storage;

            throw new KeyNotFoundException($"No such GUID in this WorldState: {objectId}");
        }

        public void Add(WorldObject obj)
        {
            throw new NotImplementedException();
        }

        public void Destroy(WorldObject obj)
        {
            //TODO: The object is known to be destroyed, take it out immediately
            throw new NotImplementedException();
        }

        public void Destroy(Guid id)
        {
            Destroy(FindObject(id));
        }

        #region Printing

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("World: {");

            builder.AppendLine($"Geography: {Geography}");

            builder.AppendLine($"Ambience: {Ambience}");

            builder.AppendLine();

            builder.AppendLine(DrawUnitMap());

            builder.AppendLine("Teams: [");
            foreach (var team in Teams)
            {
                builder.Append(team.ToString());
            }
            builder.AppendLine("]");

            builder.AppendLine("}");
            return builder.ToString();
        }

        private string DrawUnitMap()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Unit Map:");

            int width = Geography.Width;
            int height = Geography.Height;

            var map = new int[height, width];

            foreach (var team in Teams)
            {
                var locations = team.Units.Select(u => u.Location);
                foreach (var location in locations)
                {
                    map[(int)location.Y, (int)location.X] += 1;
                }
            }

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (col == 0)
                        builder.Append("|");
                    builder.Append($"{map[row, col]}|");
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }

        #endregion Printing
    }
}