using System;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Reference of type `CellPair`. Inherits from `AtomEventReference&lt;CellPair, CellVariable, CellPairEvent, CellVariableInstancer, CellPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class CellPairEventReference : AtomEventReference<
        CellPair,
        CellVariable,
        CellPairEvent,
        CellVariableInstancer,
        CellPairEventInstancer>, IGetEvent 
    { }
}
