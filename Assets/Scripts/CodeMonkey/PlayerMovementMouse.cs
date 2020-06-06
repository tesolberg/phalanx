﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMouse : MonoBehaviour
{
    private void Update() {
        if(Input.GetMouseButtonDown(1)){
            GetComponent<IMovePosition>().SetMovePosition(Elias.Utilities.GetMouseWorldPosition());
        }
    }
}
