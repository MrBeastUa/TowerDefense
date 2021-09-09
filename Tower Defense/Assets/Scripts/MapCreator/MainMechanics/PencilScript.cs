using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilScript : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<IChangableCell>() != null)
            if(collider != MapCreator.instance.currentCellToPlace)
            {
                collider.GetComponent<IChangableCell>().ChangeCell();
            }
    }
}
