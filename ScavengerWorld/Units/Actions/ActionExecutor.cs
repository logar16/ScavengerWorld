using ScavengerWorld.Units.Interfaces;
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
                        action = new NoopAction(); //Defaults to no-op

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
                    if (unit is IDropper dropper)
                        GiveAway(dropper, give);
                    else
                        throw new BadActionException($"Unit {unit} does not implement the IDropper interface");
                    break;
                case TakeAction take:
                    if (unit is ITaker taker)
                        ExecuteTake(taker, take);
                    else
                        throw new BadActionException($"Unit {unit} does not implement the ITaker interface");
                    break;
                case NoopAction noop:
                    // TODO: we can record metrics for each type of action, otherwise this does avoid the default exception.
                    break;
                default:
                    throw new BadActionException($"No matching action implemented for {action}");
            }
        }

        private void ExecuteTake(ITaker unit, TakeAction take)
        {
            var obj = State.GetObject(take.ObjectId);
            if (obj == null)
                throw new BadActionException($"Unit {unit} cannot take a non-existing object {take.ObjectId}");
            unit.Take(obj);
        }

        private void GiveAway(IDropper unit, GiveAction give)
        {
            var recipient = State.GetObject(give.Recepient);
            if (!(recipient is IReceiver receiver))
            {
                ExecuteDrop(unit, new DropAction(give.ObjectId));   //Transfer to a non-receiver is a Drop
                return;
            }

            if (!IsAdjacent((Unit)unit, recipient))
                throw new BadActionException($"Unit {unit} not adjacent to intended recipient of transfer {recipient}");
            
            var obj = State.GetObject(give.ObjectId);
            if (obj == null)
                throw new BadActionException($"Unit {unit} cannot give away a non-existing object {give.ObjectId}");

            unit.Drop(obj);
            receiver.Receive(obj);
        }

        private void ExecuteDrop(IDropper unit, DropAction drop)
        {
            var obj = State.GetObject(drop.ObjectId);
            if (obj == null)
                throw new BadActionException($"Unit {unit} cannot give away a non-existing object {drop.ObjectId}");

            unit.Drop(obj);
        }

        private void ExecuteAttack(Unit unit, Guid targetId)
        {
            var target = State.GetObject(targetId);
            if (!IsAdjacent(unit, target))
                throw new BadActionException($"Unit {unit} not adjacent to intended target of attack {target}");

            unit.Attack();
            target.TakeDamage(unit.AttackLevel);
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
