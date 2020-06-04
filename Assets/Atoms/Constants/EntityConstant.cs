using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Constant of type `Entity`. Inherits from `AtomBaseVariable&lt;Entity&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-teal")]
    [CreateAssetMenu(menuName = "Unity Atoms/Constants/Entity", fileName = "EntityConstant")]
    public sealed class EntityConstant : AtomBaseVariable<Entity> { }
}
