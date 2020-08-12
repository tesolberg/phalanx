using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding.RVO;

#pragma warning disable 0649 // variable declared but never assigned

public class Entity : MonoBehaviour
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////
    public Faction faction;

    //////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    public bool alive = true;

    GameObject selectedGFX;
    GameObject selectedPhalanxGFX;
    IMovePosition movePosition;
    Phalanx activePhalanx;
    LayerMask ownLayer;

    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

    /////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////
    public Phalanx ActivePhalanx
    {
        get => activePhalanx;
        set
        {
            activePhalanx = value;
        }
    }

    public bool Alive { get => alive; }


    /////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    public void SelectEntity(bool selected)
    {
        selectedGFX.SetActive(selected);
    }

    public void SelectEntityAsPhalanxMember(bool selected)
    {
        selectedPhalanxGFX.SetActive(selected);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        if (alive) movePosition.SetMovePosition(targetPosition);
    }

    public int GetPushPower(Vector2 rearVector){
        // if in phalanx, return column depth
        if(activePhalanx != null) return activePhalanx.GetColumnDepth(this);

        // Else return depth of ranks including self in opposite vector of attack vector
        Vector2 ownPosition = transform.position;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(ownPosition, .2f, rearVector, 50f, ownLayer);
        return hits.Length;
    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    private void Awake()
    {
        selectedGFX = transform.Find("SelectedGFX").gameObject;
        selectedPhalanxGFX = transform.Find("SelectedPhalanxGFX").gameObject;
        movePosition = GetComponent<IMovePosition>();
        selectedGFX.SetActive(false);
        selectedPhalanxGFX.SetActive(false);
        ownLayer = LayerMask.GetMask(faction.name);

    }

    public void Die()
    {
        alive = false;

        // Deselects unit
        SelectEntity(false);
        SelectEntityAsPhalanxMember(false);

        // Deactivates components
        GetComponent<Collider2D>().enabled = false;
        Attack attackComponent = GetComponent<Attack>();
        if (attackComponent) attackComponent.enabled = false;

        RVOController controller = GetComponent<RVOController>();
        controller.enabled = false;

        // Updated GFX
        transform.Find("GFX").GetComponent<SpriteRenderer>().color = Color.black;
        transform.Find("GFX").GetComponent<SpriteRenderer>().sortingOrder = 0;

        // Removes unit from phalanx
        if (activePhalanx != null) activePhalanx.RemoveEntity(this);
    }

}
