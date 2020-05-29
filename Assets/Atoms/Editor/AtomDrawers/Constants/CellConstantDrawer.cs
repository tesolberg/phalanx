#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Constant property drawer of type `Cell`. Inherits from `AtomDrawer&lt;CellConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(CellConstant))]
    public class CellConstantDrawer : VariableDrawer<CellConstant> { }
}
#endif
