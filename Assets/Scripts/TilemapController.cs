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
    Tile grass;
    [SerializeField]
    Tile floor;
    [SerializeField]
    Tile wall;
    [SerializeField]
    Tilemap terrainMap;


    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////

    public void OnCellChanged(Cell cell)
    {

        switch (cell.Terrain)
        {
            case Terrain.Grass:
                {
                    terrainMap.SetTile(cell.Position, grass);
                    break;
                }
            case Terrain.Floor:
                {
                    terrainMap.SetTile(cell.Position, floor);
                    break;
                }
            case Terrain.Wall:
                {
                    terrainMap.SetTile(cell.Position, wall);
                    break;
                }
        }

        terrainMap.RefreshTile(cell.Position);
    }



    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////


    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

}
