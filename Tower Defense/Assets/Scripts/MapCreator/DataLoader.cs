using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;

    [SerializeField]
    private Package _functional;
    private List<Package> _packages = new List<Package>();
    public List<Package> Packages => _packages;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        _packages.Add(_functional);
    }

}
