using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

[CreateAssetMenu(fileName="Faction", menuName="Entity/Faction")]
public class Faction : ScriptableObject
{
    public new string name;
    public List<Faction> allies = new List<Faction>();
    public List<Faction> enemies = new List<Faction>();
}
