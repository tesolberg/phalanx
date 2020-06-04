using System;
using UnityEngine.Events;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// None generic Unity Event of type `CellPair`. Inherits from `UnityEvent&lt;CellPair&gt;`.
    /// </summary>
    [Serializable]
    public sealed class CellPairUnityEvent : UnityEvent<CellPair> { }
}
