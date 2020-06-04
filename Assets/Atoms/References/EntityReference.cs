using System;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Reference of type `Entity`. Inherits from `AtomReference&lt;Entity, EntityPair, EntityConstant, EntityVariable, EntityEvent, EntityPairEvent, EntityEntityFunction, EntityVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class EntityReference : AtomReference<
        Entity,
        EntityPair,
        EntityConstant,
        EntityVariable,
        EntityEvent,
        EntityPairEvent,
        EntityEntityFunction,
        EntityVariableInstancer>, IEquatable<EntityReference>
    {
        public EntityReference() : base() { }
        public EntityReference(Entity value) : base(value) { }
        public bool Equals(EntityReference other) { return base.Equals(other); }
        protected override bool ValueEquals(Entity other)
        {
            throw new NotImplementedException();
        } 
    }
}
