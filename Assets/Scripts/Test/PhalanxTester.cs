using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class PhalanxTester : MonoBehaviour
    {

        public GameObject markerPrefab;
        public List<Entity> entities;
        Phalanx phalanx;
        public Direction direction = Direction.N;

        private void Start()
        {
            phalanx = new Phalanx(entities);
        }

        // void Update()
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         Vector3 mouseWorldPos = Elias.Utilities.GetMouseWorldPosition();
        //         List<Vector3> positions = phalanx.GetFrontlinePositions(mouseWorldPos, direction);

        //         foreach (var position in positions)
        //         {
        //             GameObject.Instantiate(markerPrefab, position, Quaternion.identity);
        //         }
        //     }
        // }
    }
}
