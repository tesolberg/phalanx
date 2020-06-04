using System;
using UnityEngine.Events;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// None generic Unity Event of type `EntityPair`. Inherits from `UnityEvent&lt;EntityPair&gt;`.
    /// </summary>
    [Serializable]
    public sealed class EntityPairUnityEvent : UnityEvent<EntityPair> { }
}
