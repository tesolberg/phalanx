using System;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Reference of type `Entity`. Inherits from `AtomEventReference&lt;Entity, EntityVariable, EntityEvent, EntityVariableInstancer, EntityEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class EntityEventReference : AtomEventReference<
        Entity,
        EntityVariable,
        EntityEvent,
        EntityVariableInstancer,
        EntityEventInstancer>, IGetEvent 
    { }
}
