using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class PhalanxTester : MonoBehaviour
    {

        public Entity entity;

        // private void Start()
        // {
        //     phalanx = new Phalanx();
        // }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                entity.ActivePhalanx.RotateAllColumns();
            }
        
        }
    }
}
