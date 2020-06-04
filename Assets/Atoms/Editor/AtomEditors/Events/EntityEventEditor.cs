#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Event property drawer of type `Entity`. Inherits from `AtomEventEditor&lt;Entity, EntityEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(EntityEvent))]
    public sealed class EntityEventEditor : AtomEventEditor<Entity, EntityEvent> { }
}
#endif
