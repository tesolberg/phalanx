using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type Cell to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Cell Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncCellVariableInstancerToCollection : SyncVariableInstancerToCollection<Cell, CellVariable, CellVariableInstancer> { }
}
