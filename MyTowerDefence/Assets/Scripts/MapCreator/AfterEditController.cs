using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterEditController : MonoBehaviour
{
    //public static GameObject monsterStorage;
    private static AfterEditController _instance;
    public static AfterEditController Instance => _instance;

    AfterEditUIController afterEditUI;

    [SerializeField]
    private GameObject _monstersStorage;
    [SerializeField]
    private GameObject _monsterTemplate;
    [SerializeField]
    private PortalMenuController _portalMenu;
    private void Awake()
    {
        _instance = this;

        GetAllChilds(_monstersStorage.transform).ForEach(x => Destroy(x.gameObject));
        var tempMonsters = Resources.LoadAll<GameObject>("Prefabs/Monsters").ToList();
        tempMonsters.ForEach(x => loadFromResources(x));

        ChangeUI.SetHeight(_monstersStorage.GetComponent<RectTransform>(), 15 * (_monstersStorage.transform.childCount + 1) + 64 * _monstersStorage.transform.childCount);
    }

    private void Start()
    {
        afterEditUI = AfterEditUIController.Instance;
    }

    private void loadFromResources(GameObject monsterObj)
    {
        var go = Instantiate(_monsterTemplate, _monstersStorage.transform);
        go.GetComponent<MonsterStorageTemplate>().initObject(monsterObj.GetComponent<Monster>());
    }

    public void AddMonsterToPortal(GameObject monsterObject)
    {
        _portalMenu.addToMonsterPanel(monsterObject.GetComponent<Monster>());
    }

    private List<Transform> GetAllChilds(Transform transform)
    {
        List<Transform> result = new List<Transform>();
        foreach (Transform child in transform)
            result.Add(child);

        return result;
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