using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlankCellScript : MonoBehaviour, IChangableCell
{
    public float speed = 50f;
    public void ChangeCell()
    {
        if (MapCreator.instance.currentCellToPlace != null)
        {
            Instantiate(MapCreator.instance.currentCellToPlace, MapCreator.instance.map).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
