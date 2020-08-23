using System;
using System.Collections.Generic;

namespace ScavengerWorld.Units.Actions
{
    public class UnitActionCollection
    {
        private Dictionary<Guid, UnitAction> Actions;

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
            return Actions.ContainsKey(unit.Id) ? Actions[unit.Id] : UnitAction.NONE;
        }

        public void Reset()
        {
            Actions.Clear();
        }

        public void SetDefaultActions(IEnumerable<Unit> units)
        {
            foreach (var unit in units)
            {
                AddAction(unit.Id, UnitAction.NONE);
            }
        }
    }
}
