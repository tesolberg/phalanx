using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhalanxLink
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    Vector3 position;
    Entity entity;
    bool endLink;
    PhalanxLink rightLink;
    PhalanxLink leftLink;

    public static readonly float linkRadius = .45f;
    public static readonly float maxLinkDist = 1.25f;
    public static readonly float minLinkDist = 1f;


    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    public PhalanxLink(Entity entity, bool endLink, PhalanxLink rightLink, PhalanxLink leftLink)
    {
        this.Entity = entity;
        this.EndLink = endLink;
        this.RightLink = rightLink;
        this.LeftLink = leftLink;
    }

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////
    public PhalanxLink LeftLink { get => leftLink; set => leftLink = value; }
    public PhalanxLink RightLink { get => rightLink; set => rightLink = value; }
    public bool EndLink { get => endLink; set => endLink = value; }
    public Entity Entity { get => entity; set => entity = value; }
    public Vector3 Position
    {
        get => position;
        set
        {
            position = value;
            // Move entity to position
        }
    }


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    // Moves to maintain phalanx line
    public void Follow(PhalanxLink link)
    {
        float distance = (link.entity.transform.position - entity.transform.position).magnitude;

    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

}
