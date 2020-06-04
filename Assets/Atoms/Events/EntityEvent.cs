using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event of type `Entity`. Inherits from `AtomEvent&lt;Entity&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Entity", fileName = "EntityEvent")]
    public sealed class EntityEvent : AtomEvent<Entity> { }
}
