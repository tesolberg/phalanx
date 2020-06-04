using UnityAtoms.Custom;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

#pragma warning disable 0649 // variable declared but never assigned


namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        /////////////////////
        /// PUBLIC FIELDS ///
        /////////////////////


        //////////////////////
        /// PRIVATE FIELDS ///
        //////////////////////
        [SerializeField]
        CellMap cellMap;
        [SerializeField]
        CellEvent cellChanged;
        [SerializeField]
        string[] mapFiles;
        [SerializeField]
        Topography[] topographies;

        bool refreshAstar = false;

        /////////////////////////////////////
        /// PUBLIC PROPERTIES ///////////////
        /////////////////////////////////////


        /////////////////////////////////////
        /// PUBLIC METHODS //////////////////
        /////////////////////////////////////


        //////////////////////////////////////
        /// PRIVATE METHODS AND PROPERTIES ///
        //////////////////////////////////////
        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(1);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode){
            
            string[,] stringMap = SaveLoadManager.LoadStringMap(mapFiles[0]);

            BuildCellMap(stringMap);
            
            TilemapController tilemapController = Object.FindObjectOfType<TilemapController>();
            tilemapController.Init();
            tilemapController.DrawCellMap();

            refreshAstar = true;
        }

        void BuildCellMap(string[,] stringMap)
        {
            int xSize = stringMap.GetLength(0);
            int ySize = stringMap.GetLength(1);

            cellMap.map = new Cell[xSize, ySize];

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    string targetTopographyName = stringMap[x, y];

                    foreach (var topography in topographies)
                    {
                        if (topography.name.Equals(targetTopographyName))
                        {
                            if (x == 0 || x == xSize - 1 || y == 0 || y == ySize - 1)
                            {
                                cellMap.map[x, y] = new Cell(topography, new Vector2Int(x, y), cellChanged, cellMap.map, true);
                            }
                            else cellMap.map[x, y] = new Cell(topography, new Vector2Int(x, y), cellChanged, cellMap.map);

                            break;
                        }
                    }
                }
            }
        }

        private void LateUpdate() {
            if(refreshAstar){
                AstarPath.active.Scan();
                refreshAstar = false;
            }
        }

    }

}
