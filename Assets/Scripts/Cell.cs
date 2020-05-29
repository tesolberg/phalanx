using UnityAtoms.Custom;
using UnityEngine;

public class Cell
{

    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////

    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////
    Terrain terrain;
    Vector3Int position;
    CellMap cellMap;

    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////
    public Cell(Terrain terrain, Vector3Int position, CellMap cellMap)
    {
        this.cellMap = cellMap;
        this.position = position;
        this.Terrain = terrain;
    }

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////
    public Terrain Terrain
    {
        get => terrain;
        set
        {
            terrain = value;
            if(cellMap != null && cellMap.cellChanged != null) cellMap.cellChanged.Raise(this);
        }
    }

    public Vector3Int Position { get => position; }


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////


    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////
}

public enum Terrain { Floor, Wall, Grass }
