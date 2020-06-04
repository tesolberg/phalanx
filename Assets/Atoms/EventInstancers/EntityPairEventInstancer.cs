using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Instancer of type `EntityPair`. Inherits from `AtomEventInstancer&lt;EntityPair, EntityPairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/EntityPair Event Instancer")]
    public class EntityPairEventInstancer : AtomEventInstancer<EntityPair, EntityPairEvent> { }
}
