using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName="Topography", menuName="Map/Topography")]
public class Topography : ScriptableObject
{
    public new string name;
    public bool walkable;
    public Tile tile;
    public Cell parentCell;
}
