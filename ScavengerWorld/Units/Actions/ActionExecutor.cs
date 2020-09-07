using ScavengerWorld.World;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ScavengerWorld.Units.Actions
{
    public class ActionExecutor
    {
        private Random Random;
        private WorldState State;
        public ActionExecutor(WorldState state)
        {
            State = state;
            Random = new Random();
        }

        public void Execute(UnitActionCollection actionCollection)
        {
            var allUnits = State.AllUnits;
            foreach (var entry in actionCollection.Actions)
            {
                var guid = entry.Key;
                var action = entry.Value;

                if (allUnits.TryGetValue(guid, out Unit unit))
                {
                    if (!unit.CanAttemptAction(action))
                        action = UnitAction.NOOP; //Defaults to no-op

                    try
                    {
                        AttemptAction(unit, action);
                    }
                    catch (BadActionException ex)
                    {
                        Log.Error("Failed to execute action due to the following error: ", ex);
                    }
                }
            }
        }

        private void AttemptAction(Unit unit, UnitAction action)
        {
            switch (action)
            {
                case MoveAction move:
                    MoveUnit(unit, move.MoveDirection);
                    break;
                case AttackAction attack:
                    ExecuteAttack(unit, attack.TargetId);
                    break;
                case GiveAction give:
                    GiveAway(unit, give);
                    break;
                case TransferAction transfer:
                    TransferItem(unit, transfer);
                    break;
                case UnitAction noop:
                    // TODO: we can record metrics for each type of action, otherwise this does nothing.
                    break;
                default:
                    throw new BadActionException($"No matching action implemented for {action}");
            }
        }


        //TODO: Implement the GiveAway and TransferItem actions
        private void GiveAway(Unit unit, GiveAction give)
        {
            throw new NotImplementedException();
        }

        private void TransferItem(Unit unit, TransferAction transfer)
        {
            throw new NotImplementedException();
        }

        private void ExecuteAttack(Unit unit, Guid targetId)
        {
            var target = State.GetObject(targetId);
            if (!IsAdjacent(unit, target))
                throw new BadActionException($"Unit {unit} not adjacent to intended target of attack {target}");

            unit.Attack(target);
            if (target.ShouldRemove())
            {
                State.Destroy(target);
                //unit.Stats.AddStat(killed the target)
            }
        }

        private void MoveUnit(Unit unit, MoveAction.Direction moveDirection)
        {
            Point newLocation = unit.Location;
            switch (moveDirection)
            {
                case MoveAction.Direction.NORTH:
                    newLocation.Offset(0, -1);
                    break;
                case MoveAction.Direction.EAST:
                    newLocation.Offset(1, 0);
                    break;
                case MoveAction.Direction.SOUTH:
                    newLocation.Offset(0, 1);
                    break;
                case MoveAction.Direction.WEST:
                    newLocation.Offset(-1, 0);
                    break;

            }
            if (State.Geography.GetTerrainAt(newLocation) == Geography.Terrain.NORMAL)
            {
                unit.Move(newLocation);
            }
        }

        private bool IsAdjacent(Unit unit, WorldObject target)
        {
            return unit.DistanceTo(target) < 3;
        }



        private class BadActionException : Exception
        {
            public BadActionException(string message) : base(message)
            {

            }
        }
    }
}
