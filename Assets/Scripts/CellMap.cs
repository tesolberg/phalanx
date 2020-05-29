using System.Collections;
using System.Collections.Generic;
using UnityAtoms.Custom;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Cell Map", menuName = "Map/Cell map")]
public class CellMap : ScriptableObject
{
    public int xSize;
    public int ySize;
    public Cell[,] cells;
    public CellEvent cellChanged;
}
