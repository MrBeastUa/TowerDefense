using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterPanelController : MonoBehaviour
{
    [SerializeField]
    private Image _mainImage;
    [SerializeField]
    private InputField _count;
    [SerializeField]
    private Text _name;

    private GameObject _mainObj;

    public void addMonster(Monster monster, int count)
    {
        _mainImage.GetComponent <Image> ().sprite = monster.GetComponent<SpriteRenderer>().sprite;
        _mainObj = monster.gameObject;
        _name.text = monster.name;
        _count.text = count.ToString();
    }

    public void deleteMonster()
    {
        Destroy(gameObject);
    }

    public KeyValuePair<GameObject, int> returnData()
    {
        return new KeyValuePair<GameObject, int>(_mainObj, int.Parse(_count.text)); 
    }
}
