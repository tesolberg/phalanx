using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Test
{
    public class MapEditorTester : MonoBehaviour
    {

        public Grid grid;
        public Tilemap tilemap;
        public MapEditor.MapEditorManager manager;

        private void Start() {
            manager.Save();
            manager.Load();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int pos = grid.WorldToCell(mousePos);
                Debug.Log(pos);
            }
        }
    }

}
