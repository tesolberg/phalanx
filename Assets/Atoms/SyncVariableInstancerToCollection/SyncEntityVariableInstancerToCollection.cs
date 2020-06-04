using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type Entity to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Entity Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncEntityVariableInstancerToCollection : SyncVariableInstancerToCollection<Entity, EntityVariable, EntityVariableInstancer> { }
}
