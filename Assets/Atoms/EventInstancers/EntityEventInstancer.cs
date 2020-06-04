using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Instancer of type `Entity`. Inherits from `AtomEventInstancer&lt;Entity, EntityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Entity Event Instancer")]
    public class EntityEventInstancer : AtomEventInstancer<Entity, EntityEvent> { }
}
