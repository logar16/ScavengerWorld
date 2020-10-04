using System;
using System.Collections.Generic;

namespace ScavengerWorld.Units.Actions
{
    public class UnitActionCollection
    {
        internal Dictionary<Guid, UnitAction> Actions { get; private set; }

        public UnitActionCollection()
        {
            Actions = new Dictionary<Guid, UnitAction>();
        }

        public void AddAction(Unit unit, UnitAction action)
        {
            AddAction(unit.Id, action);
        }

        public void AddAction(Guid id, UnitAction action)
        {
            Actions.Add(id, action);
        }

        public UnitAction GetAction(Unit unit)
        {
            return Actions.ContainsKey(unit.Id) ? Actions[unit.Id] : new NoopAction();
        }

        public UnitAction GetAction(Guid guid)
        {
            return Actions.ContainsKey(guid) ? Actions[guid] : new NoopAction();
        }

        public void Reset()
        {
            Actions.Clear();
        }

        public void SetDefaultActions(IEnumerable<Unit> units)
        {
            foreach (var unit in units)
            {
                AddAction(unit.Id, new NoopAction());
            }
        }

        /// <summary>
        /// Add in the actions from the other collection to this collection.
        /// </summary>
        /// <param name="other"></param>
        /// <exception cref="ArgumentException">If there are any conflicting GUIDs 
        /// (no check is made to determine if the action is the same)</exception>
        public void Merge(UnitActionCollection other)
        {
            foreach (var item in other.Actions)
            {
                var guid = item.Key;
                var action = item.Value;

                try
                {
                    AddAction(guid, action);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException($"Action already exists for {guid}", ex);
                }
            }
        }
    }
}
