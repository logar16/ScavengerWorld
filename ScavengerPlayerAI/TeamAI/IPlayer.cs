using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;

namespace ScavengerPlayerAI.TeamAI
{
    /// <summary>
    /// This interface should take in the state and return the actions of the units under its control.
    /// </summary>
    public interface IPlayer
    {
        UnitActionCollection Step(IState state);
    }
}
