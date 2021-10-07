using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveAndLoadMap : MonoBehaviour
{
    public MapInfo Load(string name)
    {
        if (File.Exists($"Assets/Maps/{name}.json"))
        {
            using (var file = new StreamReader($"Assets/Maps/{name}.json"))
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
        string path = $"Assets/Maps/{mapInfo.Name}.json";

        if (!File.Exists(path))
        {
            using (var file = new StreamWriter(path))
            {
                file.WriteLine(JsonUtility.ToJson(mapInfo));
            }
        }
    }
}

