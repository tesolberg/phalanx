using System;
using UnityEngine.Events;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// None generic Unity Event of type `Cell`. Inherits from `UnityEvent&lt;Cell&gt;`.
    /// </summary>
    [Serializable]
    public sealed class CellUnityEvent : UnityEvent<Cell> { }
}
