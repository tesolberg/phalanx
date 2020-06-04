#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Constant property drawer of type `Entity`. Inherits from `AtomDrawer&lt;EntityConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(EntityConstant))]
    public class EntityConstantDrawer : VariableDrawer<EntityConstant> { }
}
#endif
