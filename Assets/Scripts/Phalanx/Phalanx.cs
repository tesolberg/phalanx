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

    public List<Entity> entities = new List<Entity>();

    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

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

    public void Disband(){
        foreach(Entity entity in entities){
            entity.ActivePhalanx = null;
            entity.SelectEntityAsPhalanxMember(false);
        }
    }

    public void SetSelected(bool selected){
        foreach(Entity entity in entities){
            entity.SelectEntityAsPhalanxMember(selected);
        }
    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////


}
