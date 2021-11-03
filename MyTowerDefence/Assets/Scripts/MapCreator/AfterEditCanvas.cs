using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterEditCanvas : MonoBehaviour
{
    //public static GameObject monsterStorage;
    private static AfterEditCanvas _instance;
    public static AfterEditCanvas Instance => _instance;

    [SerializeField]
    private PopUpMenu _popUpMonsterList;
    [SerializeField]
    private GameObject _monstersStorage;
    [SerializeField]
    private GameObject _templateMonsterCard;
    private Dictionary<GameObject, GameObject> _monsters = new Dictionary<GameObject, GameObject>();
    private Dictionary<GameObject, int> _currentSpawnerList;


    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        var tempMonsters = Resources.LoadAll<GameObject>("Prefabs/Monsters");
        foreach (var monster in tempMonsters)
        {
            var monsterCard = Instantiate(_templateMonsterCard, _monstersStorage.transform);
            monsterCard.transform.Find("Name").GetComponent<Text>().text = monster.name;
            monsterCard.transform.Find("Image").GetComponent<Image>().sprite = monster.GetComponent<SpriteRenderer>().sprite;
            monsterCard.transform.Find("CountInPortal").GetComponent<InputField>().text = "0";
            _monsters.Add(monster, monsterCard);
        }

        ChangeUI.SetHeight(_monstersStorage.GetComponent<RectTransform>(), 15 * (_monstersStorage.transform.childCount + 1) + 256 * _monstersStorage.transform.childCount);
    }

    public void SetSpawnerData(Dictionary<GameObject, int> spawnerEnemies)
    {
        _currentSpawnerList = spawnerEnemies;
        if (!_popUpMonsterList.isOpen)
            _popUpMonsterList.StartPopUp();

        foreach (var monster in spawnerEnemies.Keys)
            _monsters[monster.gameObject].transform.Find("CountInPortal").GetComponent<InputField>().text = spawnerEnemies[monster].ToString();
        
    }

    public void SaveSpawnerDate()
    {
        var temp = _monsters.Where(x => int.Parse(x.Value.transform.Find("CountInPortal").GetComponent<InputField>().text) > 0);
        var temp2 = _monsters.Where(x => int.Parse(x.Value.transform.Find("CountInPortal").GetComponent<InputField>().text) == 0);
        foreach (var monster in temp)
            if (_currentSpawnerList.Keys.ToList().Contains(monster.Key))
                _currentSpawnerList[monster.Key] = int.Parse(monster.Value.transform.Find("CountInPortal").GetComponent<InputField>().text);
            else
                _currentSpawnerList.Add(monster.Key, int.Parse(monster.Value.transform.Find("CountInPortal").GetComponent<InputField>().text));
        
        foreach (var monster in temp2)
            if (_currentSpawnerList.Keys.ToList().Contains(monster.Key))
                _currentSpawnerList.Remove(monster.Key);
        
        CancelSave();
    }

    public void CancelSave()
    {
        _popUpMonsterList.StartPopUp();
        foreach (var monster in _monsters)
            monster.Value.transform.Find("CountInPortal").GetComponent<InputField>().text = "0";
    }
}

public static class ChangeUI
{
    public static void SetSize(this RectTransform trans, Vector2 newSize)
    {
        Vector2 oldSize = trans.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }
    public static void SetWidth(this RectTransform trans, float newSize)
    {
        SetSize(trans, new Vector2(newSize, trans.rect.size.y));
    }
    public static void SetHeight(this RectTransform trans, float newSize)
    {
        SetSize(trans, new Vector2(trans.rect.size.x, newSize));
    }
}