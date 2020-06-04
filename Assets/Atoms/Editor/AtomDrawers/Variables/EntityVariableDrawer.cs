#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Variable property drawer of type `Entity`. Inherits from `AtomDrawer&lt;EntityVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(EntityVariable))]
    public class EntityVariableDrawer : VariableDrawer<EntityVariable> { }
}
#endif
