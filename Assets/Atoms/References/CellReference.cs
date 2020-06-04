using System;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Reference of type `Cell`. Inherits from `AtomReference&lt;Cell, CellPair, CellConstant, CellVariable, CellEvent, CellPairEvent, CellCellFunction, CellVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class CellReference : AtomReference<
        Cell,
        CellPair,
        CellConstant,
        CellVariable,
        CellEvent,
        CellPairEvent,
        CellCellFunction,
        CellVariableInstancer>, IEquatable<CellReference>
    {
        public CellReference() : base() { }
        public CellReference(Cell value) : base(value) { }
        public bool Equals(CellReference other) { return base.Equals(other); }
        protected override bool ValueEquals(Cell other)
        {
            throw new NotImplementedException();
        } 
    }
}
