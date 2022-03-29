using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Links
{
    public static string currentPackage;
    public static string resourcesFolder = "Assets/Resources/";
    public static string mapsFolder = "Maps";
    public static string monstersFolder = "Prefabs/Monsters";

    public static string CurrentEnviroment => $"{resourcesFolder}/{currentPackage}/Enviroment";
    public static string CurrentFunctional => $"{resourcesFolder}/{currentPackage}/Functional";
    public static string Maps => $"{resourcesFolder}/{mapsFolder}";
}
