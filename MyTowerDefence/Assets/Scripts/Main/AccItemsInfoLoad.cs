using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccItemsInfoLoad : MonoBehaviour
{
    //відповідає за зчитування інформації про акаунт
    private static AccItemsInfoLoad _instance;
    public static AccItemsInfoLoad Instance => _instance;

    [SerializeField]
    private List<Package> _decorPackages = new List<Package>();
    [SerializeField]
    private List<Package> _enviromentalPackages = new List<Package>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public List<Package> DecorPackages => _decorPackages;
    public List<Package> EnviromentalPackages => _enviromentalPackages;
}
