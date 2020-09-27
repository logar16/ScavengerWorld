using ScavengerWorld.Units.Interfaces;
using ScavengerWorld.World;
using Serilog;
using System;
using System.Drawing;

namespace ScavengerWorld.Units.Actions
{
    public class ActionExecutor
    {
        private Random Random;
        private IState State;
        public ActionExecutor(IState state)
        {
            State = state;
            Random = new Random();
        }

        public void Execute(UnitActionCollection actionCollection)
        {
            foreach (var entry in actionCollection.Actions)
            {
                var guid = entry.Key;
                var action = entry.Value;

                var unit = State.FindObject(guid) as Unit;

                if (unit == null)
                    return;
                
                //If the Unit is technically dead, we will skip it and it should be cleaned up for next round
                if (unit.ShouldRemove())
                    continue;

                if (!unit.CanAttemptAction(action))
                    action = new NoopAction();  //Defaults to no-op

                try
                {
                    AttemptAction(unit, action);
                }
                catch (BadActionException ex)
                {
                    Log.Error("Failed to execute action due to the following error: ", ex);
                    throw;      //TODO: Do we really want to throw errors here?
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
                    if (unit is IAttacker attacker)
                        ExecuteAttack(attacker, attack.TargetId);
                    else
                        throw new BadActionException($"Unit {unit} does not implement the IAttacker interface");
                    break;
                case DropAction drop:
                    if (unit is IDropper dropper)
                        ExecuteDrop(dropper, drop);
                    else
                        throw new BadActionException($"Unit {unit} does not implement the IDropper interface");
                    break;
                case GiveAction give:
                    if (unit is IDropper dropper1)
                        GiveAway(dropper1, give);
                    else
                        throw new BadActionException($"Unit {unit} does not implement the IDropper interface");
                    break;
                case TakeAction take:
                    if (unit is ITaker taker)
                        ExecuteTake(taker, take);
                    else
                        throw new BadActionException($"Unit {unit} does not implement the ITaker interface");
                    break;
                case CreateAction create:
                    if (unit is ICreator creator)
                        ExecuteCreate(creator, create);
                    else
                        throw new BadActionException($"Unit {unit} does not implement the ICreator interface");
                    break;
                case DisplayAction display:
                    ExecuteDisplay(unit, display);
                    break;
                case NoopAction _:
                    // TODO: we can record metrics for each type of action, otherwise this does avoid the default exception.
                    break;
                default:
                    throw new BadActionException($"No matching action implemented for {action}");
            }
        }

        private void ExecuteDisplay(Unit unit, DisplayAction action)
        {
            if (action.IsReset)
            {
                unit.ResetDisplay();
            }
            else
            {
                unit.Display.UpdateFrom(action.Update);
            }
        }

        private void ExecuteCreate(ICreator creator, CreateAction create)
        {
            if (!creator.CanCreate(create))
                return;

            var obj = creator.Create(create);
            State.Add(obj);
        }

        private void ExecuteTake(ITaker unit, TakeAction action)
        {
            var obj = State.FindObject(action.ObjectId);
            if (obj == null)
                throw new BadActionException($"Unit {unit} cannot take a non-existing object {action.ObjectId}");

            if (!IsAdjacent((Unit)unit, obj))
                throw new BadActionException($"Unit {unit} not adjacent to the object it desires to take {obj}");

            if (obj is ITransferable transferable)
            {
                if (transferable.HasOwner)
                    throw new BadActionException($"Unit {unit} cannot take a currently-owned object {action.ObjectId}");

                unit.Take(transferable);
                transferable.TransferTo(((Unit)unit).Id);
            }
            else
            {
                throw new BadActionException($"Unit {unit} cannot take a non-transferable object {action.ObjectId}");
            }
        }

        private void GiveAway(IDropper donor, GiveAction action)
        {
            var recipient = State.FindObject(action.Recepient);
            if (!(recipient is IReceiver receiver) || !IsAdjacent((Unit)donor, recipient))
            {
                Drop(donor, action.ObjectId);   //Transfer to a non-receiver is a Drop
                return;
            }

            var transferable = Drop(donor, action.ObjectId);
            //Will be null if Dropper could not drop an item
            if (transferable == null)
                return;

            if (receiver.Receive(transferable))
                transferable.TransferTo(recipient.Id);
            //else the object stays on the ground
        }

        private void ExecuteDrop(IDropper dropper, DropAction action)
        {
            Drop(dropper, action.ObjectId);
        }

        private ITransferable Drop(IDropper dropper, Guid objId)
        {
            var obj = State.FindObject(objId);
            if (obj == null)
                throw new BadActionException($"Unit {dropper} cannot give away a non-existing object {obj}");

            if (obj is ITransferable transferable)
            {
                if (transferable.Owner != ((Unit)dropper).Id)
                    throw new BadActionException($"Unit {dropper} cannot give away the object {obj} because it does not posses it");

                if (!dropper.Drop(transferable))
                    return null;

                transferable.Drop();
                obj.Move(((Unit)dropper).Location);
            }
            else
            {
                throw new BadActionException($"Unit {dropper} cannot drop a non-transferable object {obj}");
            }

            return transferable;
        }

        private void ExecuteAttack(IAttacker attacker, Guid targetId)
        {
            var target = State.FindObject(targetId);
            if (!IsAdjacent((Unit)attacker, target))
                throw new BadActionException($"Unit {attacker} not adjacent to intended target of attack {target}");

            attacker.Attack();
            target.TakeDamage(attacker.AttackLevel);
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
            if (target == null)
                return false;

            return unit.DistanceTo(target) < 3;
        }

    }

    public class BadActionException : Exception
    {
        public BadActionException(string message) : base(message) { }
    }
}
