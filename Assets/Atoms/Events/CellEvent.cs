using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event of type `Cell`. Inherits from `AtomEvent&lt;Cell&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Cell", fileName = "CellEvent")]
    public sealed class CellEvent : AtomEvent<Cell> { }
}
