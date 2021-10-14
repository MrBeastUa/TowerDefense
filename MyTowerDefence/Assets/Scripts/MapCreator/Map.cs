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
    }

    private void createBlankMap()
    {
        if (size.x > 0 && size.y > 0)
        {
            if (map != null)
                for (int i = 0; i < size.x; i++)
                    for (int j = 0; j < size.y; j++)
                        Destroy(map[i, j]);

            map = new GameObject[size.x, size.y];
            for (int i = 0; i < size.x; i++)
                for (int j = 0; j < size.y; j++)
                {
                    map[i, j] = Instantiate(StorageCellsController.Instance.blankCell.gameObject, transform);
                    map[i, j].transform.position = new Vector2(i, j);
                }
        }
    }

    public void toSave(MapInfo mapInfo, string enviroment, string decor = "")
    {
        int[,] intMap = returnNumMap();
        mapInfo.Name = Name;
        mapInfo.size = size;
        for(int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if (map[i, j].GetComponent<Cell>().GetType() == new DecorScript().GetType())
                {
                    mapInfo.cells.Add(new CellInfo(j, i, $"{decor}/{map[i, j].GetComponent<Cell>().name.Remove(map[i, j].GetComponent<Cell>().name.IndexOf("(Clone)"))}"));
                }
                else if (map[i, j].GetComponent<Cell>().GetType() == new EnviromentScript().GetType())
                {
                    mapInfo.cells.Add(new CellInfo(j, i, $"{enviroment}/{map[i, j].GetComponent<Cell>().name.Remove(map[i, j].GetComponent<Cell>().name.IndexOf("(Clone)"))}"));
                }
                else
                    mapInfo.cells.Add(new CellInfo(j, i, $"Prefabs / Cells / Functional/{map[i, j].GetComponent<Cell>().name.Remove(map[i, j].GetComponent<Cell>().name.IndexOf("(Clone)"))}"));
            }
        }

        foreach(var cell in map)
        {
            if (cell.GetComponent<Cell>().GetType() == new EnemyPortal().GetType())
            {
                mapInfo.portals.Add(cell.GetComponent<EnemyPortal>().getPortalInfo(intMap));
            }
        }
    }

    public int[,] returnNumMap()//повертає мапу зі значеннями 0 - непрохідні об'єкти, 1 - дорога, 2 - старт, 3 - кінець
    {
        int[,] result = new int[map.GetLength(0), map.GetLength(1)];
        for(int i = 0; i< result.GetLength(0); i++)
            for(int j = 0; j<result.GetLength(1); j++)
            {
                if (map[i, j].GetComponent<Cell>() is EnemyPortal)
                    result[i, j] = 2;
                else if (map[i, j].GetComponent<Cell>() is DefendersPortal)
                    result[i, j] = 3;
                else if (map[i, j].GetComponent<Cell>() is Road)
                    result[i, j] = 1;
                else result[i, j] = 0;
            }

        return result;
    }
}

[System.Serializable]
public class MapInfo
{
    public string Name;
    public Vector2Int size;
    //public SaveType saveType;
    public List<CellInfo> cells = new List<CellInfo>();
    public List<PortalInfo> portals = new List<PortalInfo>();
}