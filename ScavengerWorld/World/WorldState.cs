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

        public override string ToString()
        {
            return $"[WorldState] {{ Objects: {Objects.Count}, Ambiance: {Ambience} }}";
        }
    }
}