using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public abstract class Cell : MonoBehaviour
{
    //задає стандартні характеристики кожної клітинки;
    public virtual void ChangeCell(Cell cell)
    {
        MapCreator.Instance.SetCell(cell, (int)transform.position.x, (int)transform.position.y);
        Destroy(gameObject);
    }
}

[System.Serializable]
public class CellInfo
{
    public string Path;
    public int x, y;

    public CellInfo(int x, int y, string path)
    {
        Path = path;
        this.x = x;
        this.y = y;
    }
}
