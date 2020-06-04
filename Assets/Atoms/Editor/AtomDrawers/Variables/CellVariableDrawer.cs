#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Variable property drawer of type `Cell`. Inherits from `AtomDrawer&lt;CellVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CellVariable))]
    public class CellVariableDrawer : VariableDrawer<CellVariable> { }
}
#endif
