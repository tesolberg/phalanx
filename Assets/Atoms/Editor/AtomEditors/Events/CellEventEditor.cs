#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Event property drawer of type `Cell`. Inherits from `AtomEventEditor&lt;Cell, CellEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(CellEvent))]
    public sealed class CellEventEditor : AtomEventEditor<Cell, CellEvent> { }
}
#endif
