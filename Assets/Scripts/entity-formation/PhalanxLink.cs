using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PhalanxLink
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////

    public Vector3 position;
    public Entity entity;

    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    public PhalanxLink(Vector3 position, Entity entity){
        this.position = position;
        this.entity = entity;
    }

    public PhalanxLink(Vector3 position){
        this.position = position;
    }

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////



    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////


    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

}
