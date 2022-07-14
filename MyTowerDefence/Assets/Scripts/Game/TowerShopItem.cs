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
    private Text _aoe;
    [SerializeField]
    private Text _attackSpeed;

    private GameObject _tower;
    public void InitItem(Tower tower)
    {
        _tower = tower.gameObject;
        _image.sprite = tower.GetComponent<SpriteRenderer>().sprite;
        _name.text = tower.name;

        _damage.text = tower.AttackDamage.ToString();
        _aoe.text = tower.AOERadius.ToString();
        _attackSpeed.text = tower.AttackSpeed.ToString();
    }

    public void selectTower()
    {
        TowersController.Instance.tower = _tower;
    }
}
