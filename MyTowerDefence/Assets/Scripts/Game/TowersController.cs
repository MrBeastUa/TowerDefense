using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowersController : MonoBehaviour
{
    private static TowersController _instance;
    public static TowersController Instance => _instance;

    [SerializeField]
    private GameObject _shop, _storage;
    [SerializeField]
    private GameObject _towerItemTemplate;

    private TowerPoint _selectedPlace;
    public GameObject tower;

    private void Awake()
    {
        _instance = this;
        var towers = Resources.LoadAll<GameObject>(DataPath.Towers).ToList();
        towers.ForEach(x => {
            var obj = Instantiate(_towerItemTemplate, _storage.transform);
            obj.GetComponent<TowerShopItem>().InitItem(x);
        });
    }

    public void openShop(TowerPoint point)
    {
        _shop.SetActive(true);
        _selectedPlace = point;
    }

    public void closeShop()
    {
        _shop.SetActive(false);
    }

    public void buildTower()
    {
        if(tower != null)
            _selectedPlace.BuildTower(tower);
    }
}
