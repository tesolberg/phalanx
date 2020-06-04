using System;
using UnityEngine;
namespace UnityAtoms.Custom
{
    /// <summary>
    /// IPair of type `&lt;Entity&gt;`. Inherits from `IPair&lt;Entity&gt;`.
    /// </summary>
    [Serializable]
    public struct EntityPair : IPair<Entity>
    {
        public Entity Item1 { get => _item1; set => _item1 = value; }
        public Entity Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private Entity _item1;
        [SerializeField]
        private Entity _item2;

        public void Deconstruct(out Entity item1, out Entity item2) { item1 = Item1; item2 = Item2; }
    }
}