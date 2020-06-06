using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elias;

public class UnitController : MonoBehaviour
{

    Vector3 startPosition;

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            // Left mouse button pressed
            startPosition = Utilities.GetMouseWorldPosition();
        }

        if(Input.GetMouseButtonUp(0)){
            // Left mouse button released
            
        }
    }
}
