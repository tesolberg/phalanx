using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Set variable value Action of type `Cell`. Inherits from `SetVariableValue&lt;Cell, CellPair, CellVariable, CellConstant, CellReference, CellEvent, CellPairEvent, CellVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Cell", fileName = "SetCellVariableValue")]
    public sealed class SetCellVariableValue : SetVariableValue<
        Cell,
        CellPair,
        CellVariable,
        CellConstant,
        CellReference,
        CellEvent,
        CellPairEvent,
        CellCellFunction,
        CellVariableInstancer>
    { }
}
