using System;
using UnityEngine.Events;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// None generic Unity Event of type `Entity`. Inherits from `UnityEvent&lt;Entity&gt;`.
    /// </summary>
    [Serializable]
    public sealed class EntityUnityEvent : UnityEvent<Entity> { }
}
