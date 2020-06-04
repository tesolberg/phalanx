using System;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Reference of type `EntityPair`. Inherits from `AtomEventReference&lt;EntityPair, EntityVariable, EntityPairEvent, EntityVariableInstancer, EntityPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class EntityPairEventReference : AtomEventReference<
        EntityPair,
        EntityVariable,
        EntityPairEvent,
        EntityVariableInstancer,
        EntityPairEventInstancer>, IGetEvent 
    { }
}
