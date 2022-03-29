using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDataController : MonoBehaviour
{
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text _description;

    private GameProcessData data;

    public void Init(GameProcessData data)
    {
        _name.text = data.Name;
        _description.text = data.Description;
        this.data = data;
    }

    public void setData()
    {
        GameData.Instance.CurrentMapData = data;
    }
}
