using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Package", menuName = "Data/Package")]
public class Package : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _path;

    public string Name => _name;
    public string Path => _path;
}
