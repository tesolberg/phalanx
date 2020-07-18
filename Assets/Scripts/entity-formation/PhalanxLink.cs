using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PhalanxLink : MonoBehaviour
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////


    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    public Phalanx phalanx;
    public int linkIndex;
    public PhalanxLink frontLink;
    public PhalanxLink rearLink;
    public PhalanxLink rightLink;
    public PhalanxLink leftLink;
    
    Entity entity;

    public Direction phalanxDirection;


    IMovePosition movePosition;
    AIPath aiPath;
    
    LayerMask playerLayer;

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////
    public Entity Entity { get => entity; set => entity = value; }



    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    public void Advance(int steps)
    {
        // Move self
        Vector3 targetPosition = transform.position + DirectionExtensions.DirectionToVector3(phalanxDirection);
        movePosition.SetMovePosition(targetPosition);

        RefreshLinks();

        // Call link behind to advance in same dir
        if (rearLink != null)
        {
            rearLink.Advance(steps);
        }
    }

    public void OnLinkRemovedFromPhalanx()
    {
        RefreshLinks();
        
        if (rearLink != null)
        {
            rearLink.Advance(10);
        }
    }

    public void Retreat(int steps)
    {

    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    private void Start()
    {
        movePosition = GetComponent<IMovePosition>();
        aiPath = GetComponent<AIPath>();
        playerLayer = LayerMask.GetMask("Player");
    }

    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Advance(10);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            RefreshLinks();
        }
    }

    void RefreshLinks(){
        Collider2D ownCollider = GetComponent<Collider2D>();

        ownCollider.enabled = false;

        Vector3 frontLinkPosition = transform.position + DirectionExtensions.DirectionToVector3(phalanxDirection);
        Vector3 rearLinkPosition = transform.position + (DirectionExtensions.DirectionToVector3(phalanxDirection) * -1f);

        RaycastHit2D frontHit = Physics2D.CircleCast(frontLinkPosition, .1f, Vector2.zero, 1f, playerLayer);
        RaycastHit2D rearHit = Physics2D.CircleCast(rearLinkPosition, .1f, Vector2.zero, 1f, playerLayer);

        ownCollider.enabled = true;

        if(frontHit){
            frontLink = frontHit.transform.GetComponent<PhalanxLink>();
        }

        if(rearHit){
            rearLink = rearHit.transform.GetComponent<PhalanxLink>();
        }
    }
}
