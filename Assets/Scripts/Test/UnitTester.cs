using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Test
{
    public class UnitTester : MonoBehaviour
    {

        Entity entity;

        private void Start()
        {
            entity = GetComponent<Entity>();
        }

        private void OnMouseDown()
        {
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log(entity.ActivePhalanx.GetColumnIndexFromEntity(entity));
            }
        }
    }
}
