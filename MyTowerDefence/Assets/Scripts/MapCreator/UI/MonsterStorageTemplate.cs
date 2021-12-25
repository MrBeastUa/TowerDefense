using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStorageTemplate : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Text _name;

    private GameObject monsterObj;

    public void initObject(Monster monster)
    {
        _image.sprite = monster.GetComponent<SpriteRenderer>().sprite;
        _name.text = monster.name;
        monsterObj = monster.gameObject;
    }

    public void returnObject()
    {
        AfterEditController.Instance.AddMonsterToPortal(monsterObj);
    }
}
