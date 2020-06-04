using UnityEngine;

namespace UnityAtoms.Custom
{
    /// <summary>
    /// Event Instancer of type `CellPair`. Inherits from `AtomEventInstancer&lt;CellPair, CellPairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/CellPair Event Instancer")]
    public class CellPairEventInstancer : AtomEventInstancer<CellPair, CellPairEvent> { }
}
