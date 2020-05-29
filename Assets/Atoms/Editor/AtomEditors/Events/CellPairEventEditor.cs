#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Event property drawer of type `CellPair`. Inherits from `AtomEventEditor&lt;CellPair, CellPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(CellPairEvent))]
    public sealed class CellPairEventEditor : AtomEventEditor<CellPair, CellPairEvent> { }
}
#endif
