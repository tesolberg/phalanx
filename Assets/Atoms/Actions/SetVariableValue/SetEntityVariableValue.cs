using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Set variable value Action of type `Entity`. Inherits from `SetVariableValue&lt;Entity, EntityPair, EntityVariable, EntityConstant, EntityReference, EntityEvent, EntityPairEvent, EntityVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Entity", fileName = "SetEntityVariableValue")]
    public sealed class SetEntityVariableValue : SetVariableValue<
        Entity,
        EntityPair,
        EntityVariable,
        EntityConstant,
        EntityReference,
        EntityEvent,
        EntityPairEvent,
        EntityEntityFunction,
        EntityVariableInstancer>
    { }
}
