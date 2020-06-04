using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    /////////////////////
    /// PUBLIC FIELDS ///
    /////////////////////


	//////////////////////
    /// PRIVATE FIELDS ///
    //////////////////////

    Vector2Int position;


    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

	/////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


	/////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////


    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    private void Update() {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }
}
