using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class PhalanxTester : MonoBehaviour
    {

        public EntityUIController uIController;

        // private void Start()
        // {
        //     phalanx = new Phalanx();
        // }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && uIController.selectedPhalanx != null)
            {
                uIController.selectedPhalanx.RotateAllColumns();
            }
        
        }
    }
}
