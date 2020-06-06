using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elias
{
    public static class Utilities
    {
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            return mouseWorldPos;
        }
    }

}
