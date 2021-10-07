using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Map : MonoBehaviour
{
    //відповідає за зміну мапи та збереження даних для зберігання
    [SerializeField]public string Name;
    [SerializeField]private Vector2Int size = new Vector2Int(0,0);
    [SerializeField]private GameObject[,] map;

    public Vector2Int Size
    {
        get { return size; }
        set
        {
            size = value;
            createBlankMap();
        }
    }

    private void Start()
    {
        MapCreator.Instance.Map = this;
    }

    public void changeCell(Cell cell, int x, int y)
    {
        map[x, y] = Instantiate(cell.gameObject, transform);
        map[x, y].transform.position = new Vector3(x, y, 0);
        map[x, y].GetComponent<Cell>().info.y = y;
        map[x, y].GetComponent<Cell>().info.x = x;
    }

    private void createBlankMap()
    {
        if (size.x > 0 && size.y > 0)
        {
            if (map != null)
                for (int i = 0; i < size.y; i++)
                    for (int j = 0; j < size.x; j++)
                        Destroy(map[i, j]);

            map = new GameObject[size.y, size.x];
            for (int i = 0; i < size.y; i++)
                for (int j = 0; j < size.x; j++)
                {
                    map[i, j] = Instantiate(StorageCellsController.Instance.blankCell.gameObject, transform);
                    map[i, j].transform.position = new Vector2(i, j);
                    map[i, j].GetComponent<Cell>().info.y = j;
                    map[i, j].GetComponent<Cell>().info.x = i;
                }
        }
    }

    public void toSave(MapInfo mapInfo)
    {
        mapInfo.Name = Name;
        mapInfo.size = size;
        for(int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                mapInfo.cells.Add(map[i, j].GetComponent<Cell>().info);
            }
        }
    }
}

[System.Serializable]
public class MapInfo
{
    public string  functionalPath = "Prefabs / Cells / Functional";
    public string enviromentPath;
    public string decorPath;
    public string Name;
    public Vector2Int size;
    //public SaveType saveType;
    public List<CellInfo> cells = new List<CellInfo>();
}