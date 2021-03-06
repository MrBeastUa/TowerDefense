using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONSaveMap
{
    public GameProcessData Load(TextAsset text)
    {
        GameProcessData data = new GameProcessData();
        JsonUtility.FromJsonOverwrite(text.ToString(), data);
        return data;
    }

    public void Save(GameProcessData info)
    {
        string path = $"Assets/Resources/Maps/{info.Name}.json";

        if (!File.Exists(path))
        {
            using (var file = new StreamWriter(path))
            {
                file.WriteLine(JsonUtility.ToJson(info));
            }
        }
    }
}

