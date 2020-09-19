using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;

namespace ScavengerWorld.Units.Interfaces
{
    public interface ICreator
    {
        bool CanCreate(CreateAction action);

        /// <summary>
        /// The newly created item should be registered with the world state
        /// </summary>
        /// <param name="action">Instructions on what to create</param>
        /// <returns>The newly created item</returns>
        WorldObject Create(CreateAction action);
    }
}
