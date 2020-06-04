using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Reference Listener of type `Cell`. Inherits from `AtomEventReferenceListener&lt;Cell, CellEvent, CellEventReference, CellUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Cell Event Reference Listener")]
    public sealed class CellEventReferenceListener : AtomEventReferenceListener<
        Cell,
        CellEvent,
        CellEventReference,
        CellUnityEvent>
    { }
}
