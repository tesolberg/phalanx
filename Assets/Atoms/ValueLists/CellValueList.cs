using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Value List of type `Cell`. Inherits from `AtomValueList&lt;Cell, CellEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Cell", fileName = "CellValueList")]
    public sealed class CellValueList : AtomValueList<Cell, CellEvent> { }
}
