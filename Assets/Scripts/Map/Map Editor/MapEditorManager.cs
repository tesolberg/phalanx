using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#pragma warning disable 0649 // variable declared but never assigned

namespace MapEditor
{
    public class MapEditorManager : MonoBehaviour
    {

        [SerializeField]
        string filename;
        [SerializeField]
        Tilemap terrainMap;
        [SerializeField]
        Tile[] tiles;

        Grid grid;

        private void Start()
        {
            grid = FindObjectOfType<Grid>();
        }

        public void Save()
        {
            SaveLoadManager.SaveTilemap(terrainMap, filename);
        }

        public void Load()
        {
            terrainMap.ClearAllTiles();
            string[,] stringMap = SaveLoadManager.LoadStringMap(filename);

            for (int x = 0; x < stringMap.GetLength(0); x++)
            {
                for (int y = 0; y < stringMap.GetLength(1); y++)
                {
                    string targetTileName = stringMap[x, y];

                    foreach (var tile in tiles)
                    {
                        if (tile.name.Equals(targetTileName))
                        {
                            terrainMap.SetTile(new Vector3Int(x, y, 0), tile);
                            break;
                        }
                    }
                }
            }
            
            terrainMap.RefreshAllTiles();
        }
    }
}
