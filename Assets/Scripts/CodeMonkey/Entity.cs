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

    GameObject selectedGFX;
    IMovePosition movePosition;

    ////////////////////
    /// CONSTRUCTORS ///
    ////////////////////

	/////////////////////////////////////
    /// PUBLIC PROPERTIES ///////////////
    /////////////////////////////////////


	/////////////////////////////////////
    /// PUBLIC METHODS //////////////////
    /////////////////////////////////////

    public void SelectEntity(bool selected){
        selectedGFX.SetActive(selected);
    }

    public void MoveTo(Vector3 targetPosition){
        movePosition.SetMovePosition(targetPosition);
    }

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    private void Awake() {
        selectedGFX = transform.Find("SelectedGFX").gameObject;
        movePosition = GetComponent<IMovePosition>();
        selectedGFX.SetActive(false);
    }

}
