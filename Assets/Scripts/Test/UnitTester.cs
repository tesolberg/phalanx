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
                entity.ActivePhalanx.AdvanceColumnContainingEntity(entity);
            }
            else if(Input.GetKey(KeyCode.Z)){
                entity.ActivePhalanx.RetreatColumnContainingEntity(entity);
            }
            else if(Input.GetKey(KeyCode.D)){
                entity.Die();
            }
        }
    }
}
