using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public abstract class Cell : MonoBehaviour
{
    public CellInfo info = new CellInfo();
    //задає стандартні характеристики кожної клітинки;

    public void OnEnable()
    {
        info.Name = name.Remove(name.IndexOf("(Clone)"));
        info.x = (int)transform.position.x;
        info.y = (int)transform.position.y;
    }

    public virtual void ChangeCell(Cell cell)
    {
        MapCreator.Instance.SetCell(cell, (int)transform.position.x, (int)transform.position.y);
        Destroy(gameObject);
    }
}

[System.Serializable]
public class CellInfo
{
    public string Name;
    public int x, y;
}
