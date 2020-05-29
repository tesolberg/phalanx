using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event of type `CellPair`. Inherits from `AtomEvent&lt;CellPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/CellPair", fileName = "CellPairEvent")]
    public sealed class CellPairEvent : AtomEvent<CellPair> { }
}
