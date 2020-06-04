using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Reference Listener of type `CellPair`. Inherits from `AtomEventReferenceListener&lt;CellPair, CellPairEvent, CellPairEventReference, CellPairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/CellPair Event Reference Listener")]
    public sealed class CellPairEventReferenceListener : AtomEventReferenceListener<
        CellPair,
        CellPairEvent,
        CellPairEventReference,
        CellPairUnityEvent>
    { }
}
