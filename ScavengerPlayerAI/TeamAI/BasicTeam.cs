using ScavengerWorld.Units.Actions;
using ScavengerWorld.World;
using System;
using System.Linq;

namespace ScavengerPlayerAI.TeamAI
{
    class BasicTeam : IPlayer
    {
        public int TeamIndex { get; private set; }

        public BasicTeam(int teamIndex)
        {
            TeamIndex = teamIndex;
        }

        public UnitActionCollection Step(IState state)
        {
            var team = state.Teams.Select(t => t.Id == TeamIndex);

            throw new NotImplementedException();
        }
    }
}
