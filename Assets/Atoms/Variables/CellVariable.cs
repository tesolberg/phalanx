using UnityEngine;
using System;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Variable of type `Cell`. Inherits from `AtomVariable&lt;Cell, CellPair, CellEvent, CellPairEvent, CellCellFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Cell", fileName = "CellVariable")]
    public sealed class CellVariable : AtomVariable<Cell, CellPair, CellEvent, CellPairEvent, CellCellFunction>
    {
        protected override bool ValueEquals(Cell other)
        {
            throw new NotImplementedException();
        }
    }
}
