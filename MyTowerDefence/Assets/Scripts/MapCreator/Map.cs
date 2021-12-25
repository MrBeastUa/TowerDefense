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

    public void toSave(string enviroment)
    {
        MapCreator.Instance.createdMap.Name = Name;
        MapCreator.Instance.createdMap.size = size;

        int[,] intMap = new int [map.GetLength(0), map.GetLength(1)];
        for (int i = 0; i < intMap.GetLength(0); i++)
            for (int j = 0; j < intMap.GetLength(1); j++)
            {
                if (map[i, j].GetComponent<Cell>() is EnemyPortal)
                    intMap[i, j] = 2;
                else if (map[i, j].GetComponent<Cell>() is DefendersPortal)
                    intMap[i, j] = 3;
                else if (map[i, j].GetComponent<Cell>() is Road)
                    intMap[i, j] = 1;
                else intMap[i, j] = 0;
            }

        for(int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                if (map[i, j].GetComponent<Cell>().GetType() == new EnviromentScript().GetType())
                    MapCreator.Instance.createdMap.cells.Add(new CellInfo(j, i, $"{enviroment}/Enviroment/{map[i, j].GetComponent<Cell>().name.Remove(map[i, j].GetComponent<Cell>().name.IndexOf("(Clone)"))}"));
                else
                    MapCreator.Instance.createdMap.cells.Add(new CellInfo(j, i, $"{enviroment}/Functional/{map[i, j].GetComponent<Cell>().name.Remove(map[i, j].GetComponent<Cell>().name.IndexOf("(Clone)"))}"));

        foreach (var cell in map)
            if (cell.GetComponent<Cell>().GetType() == new EnemyPortal().GetType())
            {
                cell.GetComponent<EnemyPortal>().FindWay(intMap);
                MapCreator.Instance.createdMap.portals.Add(cell.GetComponent<EnemyPortal>().GetInfo);
            }
    }
}