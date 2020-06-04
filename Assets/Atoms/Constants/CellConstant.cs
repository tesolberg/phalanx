using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Constant of type `Cell`. Inherits from `AtomBaseVariable&lt;Cell&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-teal")]
    [CreateAssetMenu(menuName = "Unity Atoms/Constants/Cell", fileName = "CellConstant")]
    public sealed class CellConstant : AtomBaseVariable<Cell> { }
}
