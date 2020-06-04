using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Custom.Editor
{
    /// <summary>
    /// Variable Inspector of type `Cell`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(CellVariable))]
    public sealed class CellVariableEditor : AtomVariableEditor<Cell, CellPair> { }
}
