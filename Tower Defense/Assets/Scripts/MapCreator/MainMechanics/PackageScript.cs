using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageScript : MonoBehaviour
{
    [SerializeField]
    private Text _title;
    private Dropdown _itemList;
    private LogicPickCell _mainLogic;

    private void Awake()
    {
        _itemList = GetComponent<Dropdown>();
    }
    public void CreatePackage(Package package, LogicPickCell logic)
    {
        _mainLogic = logic;
        _title.text = package.Name;
        var cells = Resources.LoadAll<GameObject>(package.Path);
        if (cells != null)
            foreach (var cell in cells)
                _itemList.options.Add(new Dropdown.OptionData() { image = cell.GetComponent<SpriteRenderer>().sprite });
    }

    public void ChangeValue(int input)
    {
        _mainLogic.PickCell(gameObject, input - 1);
    }

    public void SelectNullCell()
    {
        _itemList.value = 0;
    }
}
