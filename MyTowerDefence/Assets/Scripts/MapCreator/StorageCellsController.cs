using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StorageCellsController : MonoBehaviour
{
    //відповідає за заповнення пакетів графіки з папок та вибору клітинок для малювання

    private static StorageCellsController _instance;
    public static StorageCellsController Instance => _instance;

    public Cell selectedCell;
    public Cell blankCell;
    public Dictionary<Dropdown,List<GameObject>> packages = new Dictionary<Dropdown, List<GameObject>>();

    private void Awake()
    {
        _instance = this;
    }

    public void AddToPackage(GameObject package, Package packageInfo, bool isFunctional = false)
    {
        Dropdown dropdown = package.GetComponent<Dropdown>();
        List<GameObject> cells;
        if (isFunctional)
        {
            cells = Resources.LoadAll<GameObject>(packageInfo.Path + "/Functional").ToList();
            package.transform.GetChild(0).GetComponent<Text>().text = "Functional";
        }
        else
        {
            cells = Resources.LoadAll<GameObject>(packageInfo.Path + "/Enviroment").ToList();
            package.transform.GetChild(0).GetComponent<Text>().text = "Enviroment";
        }
        dropdown.options.AddRange(cells.Select(cell => new Dropdown.OptionData() { text = cell.name, image = cell.GetComponent<SpriteRenderer>().sprite }));

        
        packages.Add(dropdown, cells);

    }

    public void SelectCell(Dropdown pack, int value)
    {
        packages.Where(x => x.Key != pack).Select(x => x.Key).ToList().ForEach(x => 
        {
            x.value = 0;
        });
        selectedCell = packages.Where(x => x.Key == pack).First().Value[value].GetComponent<Cell>();
    }
}
