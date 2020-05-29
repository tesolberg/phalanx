using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Instancer of type `Cell`. Inherits from `AtomEventInstancer&lt;Cell, CellEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Cell Event Instancer")]
    public class CellEventInstancer : AtomEventInstancer<Cell, CellEvent> { }
}
