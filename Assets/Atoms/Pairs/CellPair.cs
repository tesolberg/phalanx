using System;
using UnityEngine;
namespace UnityAtoms.Custom
{
    /// <summary>
    /// IPair of type `&lt;Cell&gt;`. Inherits from `IPair&lt;Cell&gt;`.
    /// </summary>
    [Serializable]
    public struct CellPair : IPair<Cell>
    {
        public Cell Item1 { get => _item1; set => _item1 = value; }
        public Cell Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private Cell _item1;
        [SerializeField]
        private Cell _item2;

        public void Deconstruct(out Cell item1, out Cell item2) { item1 = Item1; item2 = Item2; }
    }
}