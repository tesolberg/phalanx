using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPainter : MonoBehaviour
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////
    public CellMap cellMap;


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////
    [SerializeField]
    Terrain selectedTerrain = Terrain.Grass;

    // References
    [SerializeField]
    Grid grid;

    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////


    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////
    private void Start()
    {
        if (cellMap.cells == null)
        {
            InitBlankMap();
        }
    }

    private void InitBlankMap()
    {
        cellMap.cells = new Cell[cellMap.xSize, cellMap.ySize];

        for (int x = 0; x < cellMap.xSize; x++)
        {
            for (int y = 0; y < cellMap.ySize; y++)
            {
                cellMap.cells[x, y] = new Cell(Terrain.Grass, new Vector3Int(x, y, 0), cellMap);
            }
        }
    }

    private void Update()
    {
        UpdateSelectedTerrain();
        DrawToMap();
    }

    private void UpdateSelectedTerrain()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedTerrain = Terrain.Grass;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTerrain = Terrain.Floor;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedTerrain = Terrain.Wall;
        }
    }

    private void DrawToMap()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = grid.WorldToCell(mousePosition);

            int x = cellPosition.x;
            int y = cellPosition.y;

            if(x >= 0 && x < cellMap.xSize && y >= 0 && y < cellMap.ySize){
                cellMap.cells[x,y].Terrain = selectedTerrain;
            }
        }
    }
}
