#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Value List property drawer of type `Cell`. Inherits from `AtomDrawer&lt;CellValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CellValueList))]
    public class CellValueListDrawer : AtomDrawer<CellValueList> { }
}
#endif
