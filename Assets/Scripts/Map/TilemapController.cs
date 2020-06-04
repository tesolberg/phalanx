using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.Custom;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////
    [SerializeField]
    CellMap cellMap;

    Tilemap terrain;
    Grid grid;
    Tilemap overlay;

    [SerializeField]
    Tile redSquare;
    [SerializeField]
    Tile greenSquare;


    bool tileChanged = false;

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////

    public void OutlineTilesGreen(Vector3Int[] positions)
    {
        overlay.ClearAllTiles();

        foreach (var position in positions)
        {
            overlay.SetTile(position, greenSquare);
        }

        overlay.RefreshAllTiles();
    }

    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    public void Init()
    {
        grid = GetComponent<Grid>();
        terrain = GameObject.Find("/Grid/Terrain").GetComponent<Tilemap>();
        overlay = GameObject.Find("/Grid/Overlay").GetComponent<Tilemap>();

        GetComponent<CellEventReferenceListener>().enabled = true;
    }

    internal void DrawCellMap()
    {       
        for (int x = 0; x < cellMap.map.GetLength(0); x++)
        {
            for (int y = 0; y < cellMap.map.GetLength(1); y++)
            {
                terrain.SetTile(new Vector3Int(x,y,0), cellMap.map[x,y].Topography.tile);
            }
        }
    }

    //     public Cell CellAtWorldPosition(Vector3 position)
    // {
    //     position.z = 0f;
    //     Vector3Int tilePos = grid.WorldToCell(position);
    //     return cellMap.cells[tilePos.x, tilePos.y];
    // }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    void UpdateTile(Cell cell)
    {

        Vector3Int pos = (Vector3Int)cell.Position;

        // Sets tile on terrain tilemap
        terrain.SetTile(pos, cell.Topography.tile);
        terrain.RefreshTile(pos);
    }

}
