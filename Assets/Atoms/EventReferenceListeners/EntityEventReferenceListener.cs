using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Reference Listener of type `Entity`. Inherits from `AtomEventReferenceListener&lt;Entity, EntityEvent, EntityEventReference, EntityUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Entity Event Reference Listener")]
    public sealed class EntityEventReferenceListener : AtomEventReferenceListener<
        Entity,
        EntityEvent,
        EntityEventReference,
        EntityUnityEvent>
    { }
}
