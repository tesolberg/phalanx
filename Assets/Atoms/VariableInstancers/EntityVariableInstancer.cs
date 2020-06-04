using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Variable Instancer of type `Entity`. Inherits from `AtomVariableInstancer&lt;EntityVariable, EntityPair, Entity, EntityEvent, EntityPairEvent, EntityEntityFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Entity Variable Instancer")]
    public class EntityVariableInstancer : AtomVariableInstancer<
        EntityVariable,
        EntityPair,
        Entity,
        EntityEvent,
        EntityPairEvent,
        EntityEntityFunction>
    { }
}
