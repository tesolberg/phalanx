using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace Test
{
    public class UnitTester : MonoBehaviour
    {
        public AIPath ai;
        public Transform target;

        private void Update()
        {
            ai.destination = target.position;
        }
    }
}
