using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONSaveMap
{
    public GameProcessData Load(string name)
    {
        string path = $"Assets/Resources/Maps/{name}.json";
        if (File.Exists(path))
        {
            using (var file = new StreamReader(path))
            {
                string text = file.ReadLine();
                return JsonUtility.FromJson<GameProcessData>(text);
            }
        }
        else
        {
            return null;
        }
    }

    public void Save(GameProcessData info)
    {
        string path = $"Assets/Resources/Maps/{info.Name}.json";

        if (!File.Exists(path))
        {
            using (var file = new StreamWriter(path))
            {
                file.WriteLine(JsonUtility.ToJson(info, true));
            }
        }
    }
}

