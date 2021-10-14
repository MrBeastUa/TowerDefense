using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONSaveMap
{
    public MapInfo Load(string name)
    {
        string path = $"Assets/Resources/Maps/{name}.json";
        if (File.Exists(path))
        {
            using (var file = new StreamReader(path))
            {
                string text = file.ReadLine();
                return JsonUtility.FromJson<MapInfo>(text);
            }
        }
        else
        {
            return null;
        }
    }

    public void Save(MapInfo mapInfo)
    {
        string path = $"Assets/Resources/Maps/{mapInfo.Name}.json";

        if (!File.Exists(path))
        {
            using (var file = new StreamWriter(path))
            {
                file.WriteLine(JsonUtility.ToJson(mapInfo,true));
            }
        }
    }
}

