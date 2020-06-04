using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New cell map", menuName="Basic/Cell map")]
public class CellMap : ScriptableObject
{
    public Cell[,] map;
}
