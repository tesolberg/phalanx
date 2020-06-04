using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Value List of type `Entity`. Inherits from `AtomValueList&lt;Entity, EntityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Entity", fileName = "EntityValueList")]
    public sealed class EntityValueList : AtomValueList<Entity, EntityEvent> { }
}
