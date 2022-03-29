using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShopItem : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text _damage;
    [SerializeField]
    private Text _isAoe;
    [SerializeField]
    private Text _attackSpeed;

    private GameObject _tower;
    public void InitItem(GameObject tower)
    {
        _tower = tower;
        _image.sprite = tower.GetComponent<SpriteRenderer>().sprite;
        _name.text = tower.name;        
    }

    public void selectTower()
    {
        TowersController.Instance.tower = _tower;
    }
}
