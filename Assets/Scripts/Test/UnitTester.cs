using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Test
{
    public class UnitTester : MonoBehaviour
    {
        public Entity entity;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.C)){
                entity.IncomingAttack(entity);
            }
        }
    }
}
