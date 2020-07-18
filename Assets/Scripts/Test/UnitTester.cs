using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Test
{
    public class UnitTester : MonoBehaviour
    {
        private void OnMouseDown() {
            if(Input.GetKey(KeyCode.D)){
                GetComponent<Entity>().Die();
            }
        }

    }
}
