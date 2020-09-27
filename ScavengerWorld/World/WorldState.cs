using ScavengerWorld.Teams;
using ScavengerWorld.Units;
using ScavengerWorld.Units.Interfaces;
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
        public IEnumerable<Team> Teams { get; internal set; }
        public IEnumerable<Unit> Units { get => Teams.SelectMany(t => t.Units); }
        public int TotalUnits { get => Teams.Sum(t => t.UnitCount); } 

        public Dictionary<Guid, WorldObject> Objects { get; internal set; }

        private List<WorldObject> DestroyedObjects;

        public WorldState()
        {
            Teams = new List<Team>();
            Objects = new Dictionary<Guid, WorldObject>();

            DestroyedObjects = new List<WorldObject>();
        }

        internal WorldState Clone()
        {
            var copy = new WorldState();
            copy.Ambience = (AmbientEnvironment)Ambience.Clone();
            copy.Geography = (Geography)Geography.Clone();

            copy.Teams = Teams.Select(team => new Team(team)).ToList();
            copy.Objects = Objects.ToDictionary(entry => entry.Key, entry => (WorldObject)entry.Value.Clone());

            return copy;
        }


        public void Step(int timeStep)
        {
            Ambience.Step(timeStep);

            foreach (var obj in Objects.Values)
            {
                if (obj is ISteppable stepper)
                    stepper.Step(timeStep);

                if (obj.ShouldRemove())
                    Destroy(obj);
            }
        }


        public WorldObject FindObject(Guid objectId)
        {
            Objects.TryGetValue(objectId, out WorldObject obj);
            return obj;
        }

        public void Add(WorldObject obj)
        {
            throw new NotImplementedException();
        }

        public void Destroy(WorldObject obj)
        {
            var objectId = obj.Id;

            if (obj is Unit unit)
            {
                //TODO: Remove from Teams/AllUnits
            }
            else if (obj is Food food)
            {
                //TODO: Remove from Food list
            }
            else if (obj is WorldObject wobject)
            {
                //TODO: Remove from InanimateObjects
            }
            else if (obj is FoodStorage storage)
            {
                //TODO: Remove from Team
            }

            //TODO: Make sure owner releases the object
            if (obj is ITransferable transferable && transferable.HasOwner)
            {
                var owner = FindObject(transferable.Owner) as IDropper;
                owner.Drop(transferable);
            }

            DestroyedObjects.Add(obj);
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