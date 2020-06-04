using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Reference Listener of type `EntityPair`. Inherits from `AtomEventReferenceListener&lt;EntityPair, EntityPairEvent, EntityPairEventReference, EntityPairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/EntityPair Event Reference Listener")]
    public sealed class EntityPairEventReferenceListener : AtomEventReferenceListener<
        EntityPair,
        EntityPairEvent,
        EntityPairEventReference,
        EntityPairUnityEvent>
    { }
}
