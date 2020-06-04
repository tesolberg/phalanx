#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Value List property drawer of type `Entity`. Inherits from `AtomDrawer&lt;EntityValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(EntityValueList))]
    public class EntityValueListDrawer : AtomDrawer<EntityValueList> { }
}
#endif
