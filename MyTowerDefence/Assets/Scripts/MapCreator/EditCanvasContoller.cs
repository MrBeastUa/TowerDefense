using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void toUpdate();
public class EditCanvasContoller : MonoBehaviour
{
    //відповідає за всі виклики Update та передачу пакетів графіки для подальшої обробки
    //дає можливість вибрати режим малювання

    public static toUpdate drawBehaviour = null;
    public static DrawMode drawMode = DrawMode.Watch;

    [SerializeField]
    private Pencil pencil;

    [SerializeField]
    private GridLayoutGroup _packagesStorageUI;

    [SerializeField]
    private Dropdown _packageTemplate;


    public void SetDrawMode(int value)
    {
        drawMode = (DrawMode)value;
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
                pencil.gameObject.SetActive(true);
            else if (Input.GetMouseButtonUp(0))
                pencil.gameObject.SetActive(false);

            if(drawBehaviour != null)
            if (drawBehaviour.GetInvocationList().Length > 0)
                drawBehaviour.Invoke();
        }
    }

    public void AddPackages(Package enviroment)//, Package decor = null)
    {
        StorageCellsController.Instance.AddToPackage(Instantiate(_packageTemplate.gameObject, _packagesStorageUI.transform),enviroment, true);
        StorageCellsController.Instance.AddToPackage(Instantiate(_packageTemplate.gameObject, _packagesStorageUI.transform), enviroment, false);
        //if(decor != null)
        //    StorageCellsController.Instance.AddToPackage(Instantiate(_packageTemplate.gameObject, _packagesStorageUI.transform), decor);
    }
}

public enum DrawMode
{
    Watch,
    Pencil,
    Erase
}