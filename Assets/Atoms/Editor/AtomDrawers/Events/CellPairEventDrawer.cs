#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Event property drawer of type `CellPair`. Inherits from `AtomDrawer&lt;CellPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CellPairEvent))]
    public class CellPairEventDrawer : AtomDrawer<CellPairEvent> { }
}
#endif
