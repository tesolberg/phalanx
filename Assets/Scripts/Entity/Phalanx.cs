using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

public class Phalanx
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////


	//////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    List<Entity> entities = new List<Entity>();
    Entity commander;


    List<Vector3Int> desiredFrontline = new List<Vector3Int>();


    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    public Phalanx(){

    }

	/////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


	/////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    public void AddEntity(Entity entity){
        if(!entities.Contains(entity)) entities.Add(entity);
    }

    public void RemoveEntity(Entity entity){
        if(entities.Contains(entity)) entities.Remove(entity);
    }

    public List<Vector3Int> GetFrontline(Vector3Int position, Direction direction){
        List<Vector3Int> positions = new List<Vector3Int>();

        return positions;
    }

    public bool CheckFrontlineIntegrety(){
        return true;
    }

    public void ReestablishFrontlineIntegrity(){

    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

}

public enum Direction{N,S,E,W}
