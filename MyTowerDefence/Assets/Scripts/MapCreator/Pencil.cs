using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    //Відповідає за рух олівця і активацію зміни клітинки

    [SerializeField]
    private Cell currentCell = null;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Try to change");
        if (collider.GetComponent<Cell>() != null)
        {
            if (currentCell != null && collider.GetComponent<Cell>().GetType() != currentCell.GetType())
            {
                collider.GetComponent<Cell>().ChangeCell(currentCell);
                //Debug.Log("Change");
            }
        }
    }
    
    private void Movement()
    {
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    private void OnEnable()
    {
        //Debug.Log($"Set mode {StorageCellsController.Instance.drawMode}");
        switch (EditCanvasContoller.drawMode)
        {
            case DrawMode.Watch:
                currentCell = null;
                break;
            case DrawMode.Erase:
                currentCell = StorageCellsController.Instance.blankCell;
                break;
            case DrawMode.Pencil:
                currentCell = StorageCellsController.Instance.selectedCell;
                break;
        }
        EditCanvasContoller.drawBehaviour += Movement;
    }

    private void OnDisable()
    {
        EditCanvasContoller.drawBehaviour -= Movement;
    }
}