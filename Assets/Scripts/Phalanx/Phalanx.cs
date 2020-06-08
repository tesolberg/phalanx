using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.Tilemaps;

public class Phalanx
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    List<Entity> entities = new List<Entity>();
    List<PhalanxLink> links = new List<PhalanxLink>();

    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    public Phalanx(List<Entity> entities)
    {
        this.entities = entities;
    }

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    // public void AddEntity(Entity entity){
    //     if(!entities.Contains(entity)) entities.Add(entity);
    // }

    // public void RemoveEntity(Entity entity){
    //     if(entities.Contains(entity)) entities.Remove(entity);
    // }



    public void SetFrontline(Vector3 origin, Direction direction){

    }

    public List<Vector3Int> GetFrontline(Vector3Int position, Direction direction)
    {
        List<Vector3Int> positions = new List<Vector3Int>();

        return positions;
    }

    public bool CheckFrontlineIntegrety()
    {
        return true;
    }

    public void ReestablishFrontlineIntegrity()
    {

    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////


}

public enum Direction { N, NE, E, SE, S, SW, W, NW }

public static class DirectionExtensions {
    public static Vector3 DirectionToVector3(Direction direction){
        switch (direction){
            case Direction.N :
            return new Vector3(0f, 1f, 0f);
            case Direction.NE :
            return new Vector3(.7f, .7f, 0f);
            case Direction.E :
            return new Vector3(1f, 0f, 0f);
            case Direction.SE :
            return new Vector3(.7f, -.7f, 0f);
            case Direction.S :
            return new Vector3(0f, -1f, 0f);
            case Direction.SW :
            return new Vector3(-.7f, -.7f, 0f);
            case Direction.W :
            return new Vector3(-1f, 0f, 0f);
            default :
            return new Vector3(-.7f, .7f, 0f);
       }
    }
}
