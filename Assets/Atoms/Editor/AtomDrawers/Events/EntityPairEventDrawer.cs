#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Event property drawer of type `EntityPair`. Inherits from `AtomDrawer&lt;EntityPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(EntityPairEvent))]
    public class EntityPairEventDrawer : AtomDrawer<EntityPairEvent> { }
}
#endif
