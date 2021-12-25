using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageScript : MonoBehaviour
{
    //відповідає за передачу даних з Dropdown пакетів для зміни клітинок

    public void ChangeValue(int value)
    {
        if (value > 0)
            StorageCellsController.Instance.SelectCell(GetComponent<Dropdown>(), value - 1);
    }

}
