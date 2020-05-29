using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Variable Instancer of type `Cell`. Inherits from `AtomVariableInstancer&lt;CellVariable, CellPair, Cell, CellEvent, CellPairEvent, CellCellFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Cell Variable Instancer")]
    public class CellVariableInstancer : AtomVariableInstancer<
        CellVariable,
        CellPair,
        Cell,
        CellEvent,
        CellPairEvent,
        CellCellFunction>
    { }
}
