using System.Collections;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Tilemaps;

public static class SaveLoadManager
{
    public static void SaveTilemap(Tilemap tileMap, string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.dataPath + "/FileStreams/Maps/" + filename + ".map", FileMode.Create);

        string[,] stringMap = TilemapToStringArray(tileMap);
        MapData data = new MapData(stringMap);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static string[,] LoadStringMap(string filename)
    {
        if (File.Exists(Application.dataPath + "/FileStreams/Maps/" + filename + ".map"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.dataPath + "/FileStreams/Maps/" + filename + ".map", FileMode.Open);

            MapData data = bf.Deserialize(stream) as MapData;
            return data.map;
        }
        
        Debug.LogError("Could not open file at path: " + Application.dataPath + "/FileStreams/Maps/" + filename + ".map");
        return null;
    }


    private static string[,] TilemapToStringArray(Tilemap tilemap)
    {
        tilemap.CompressBounds();

        BoundsInt bounds = tilemap.cellBounds;


        string[,] map = new string[bounds.size.x, bounds.size.y];

        for (int x = bounds.xMin, i = 0; x < bounds.xMax; x++, i++)
        {
            for (int y = bounds.yMin, j = 0; y < bounds.yMax; y++, j++)
            {
                var tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                map[i, j] = tile ? tile.name : "";
            }
        }


        return map;
    }
}

[Serializable]
public class MapData
{
    public string[,] map;

    public MapData(string[,] map)
    {
        this.map = map;
    }
}
