#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Event property drawer of type `Entity`. Inherits from `AtomDrawer&lt;EntityEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(EntityEvent))]
    public class EntityEventDrawer : AtomDrawer<EntityEvent> { }
}
#endif
