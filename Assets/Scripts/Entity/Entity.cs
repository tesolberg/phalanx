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

    //////////////////////////////////////
    /// PRIVATE METHODS AND PROPERTIES ///
    //////////////////////////////////////

    private void Awake() {
        selectedGFX = transform.Find("SelectedGFX").gameObject;
        selectedGFX.SetActive(false);
    }

}
