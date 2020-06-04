using System;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Reference of type `Cell`. Inherits from `AtomEventReference&lt;Cell, CellVariable, CellEvent, CellVariableInstancer, CellEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class CellEventReference : AtomEventReference<
        Cell,
        CellVariable,
        CellEvent,
        CellVariableInstancer,
        CellEventInstancer>, IGetEvent 
    { }
}
