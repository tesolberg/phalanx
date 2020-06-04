using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.Custom;

public class Cell
{

    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////
    public readonly bool borderTile;

    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////
    CellEvent cellChanged;
    Vector2Int position;
    Topography topography;

    Entity occupant;
    int dominance;
    Cell[,] cellMap;



    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    public Cell(Topography topography, Vector2Int position, CellEvent cellChanged, Cell[,] cellMap, bool borderTile = false)
    {
        this.topography = GameObject.Instantiate(topography) as Topography;
        this.topography.parentCell = this;
        this.position = position;
        this.borderTile = borderTile;
        this.cellChanged = cellChanged;
        this.cellMap = cellMap;
    }

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////
    public int Dominance
    {
        get => dominance;
        set
        {
            dominance = Mathf.Clamp(value, 0, 99);
            cellChanged.Raise(this);
        }
    }

    public Topography Topography
    {
        get => topography;
        set
        {
            topography = value;
            topography.parentCell = this;
            CellChanged();
        }
    }

    public Vector2Int Position { get => position;}


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////



    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////
    void CellChanged()
    {
        if (cellChanged != null) cellChanged.Raise(this);
    }

}

public enum Terrain { Grass, Floor, Wall }
