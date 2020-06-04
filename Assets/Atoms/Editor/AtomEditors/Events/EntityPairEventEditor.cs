#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Event property drawer of type `EntityPair`. Inherits from `AtomEventEditor&lt;EntityPair, EntityPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(EntityPairEvent))]
    public sealed class EntityPairEventEditor : AtomEventEditor<EntityPair, EntityPairEvent> { }
}
#endif
