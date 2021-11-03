using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCreator : MonoBehaviour
{
    //відповідає за передачу інформації між етапами побудови мапи
    private static MapCreator _instance;
    public static MapCreator Instance => _instance;
    public CreatingEtap etap = CreatingEtap.Start;

    [SerializeField]
    private Canvas _startCanvas, _editMapCanvas, _afterEditCanvas;
    private Map _map;
    private MapInfo info = new MapInfo();
    private string enviromentPath = "", decorPath = "";
    private Physics2DRaycaster _pr = null;

    public Map Map
    {
        get
        {
            return _map;
        }
        set
        {
            if(_map == null)
                _map = value;
        }
    }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        _pr = GetComponent<Physics2DRaycaster>();
        _pr.enabled = false;
        _startCanvas.gameObject.SetActive(true);
    }

    public void StartEditMode(string name, int x, int y, Package enviroment, Package decor = null)
    {
        //Save path of packages
        etap = CreatingEtap.Edit;
        enviromentPath = enviroment.Path;
        if(decor != null)
        decorPath = decor.Path;

        CameraMove.instance.CameraStartPosition = new Vector3(((float)x)/2,((float)y)/2,0);
        
        _editMapCanvas.gameObject.SetActive(true);
        _editMapCanvas.GetComponent<EditCanvasContoller>().AddPackages(enviroment);
        _map.Name = name;
        _map.Size = new Vector2Int(x,y);
       
        _startCanvas.gameObject.SetActive(false);
      
    }

    public void StartAfterEditMode()
    {
        _pr.enabled = true;
        etap = CreatingEtap.AfterEdit;
        _afterEditCanvas.gameObject.SetActive(true);
        _editMapCanvas.gameObject.SetActive(false);
    }

    public void SetCell(Cell cell,int x, int y)
    {
        _map.changeCell(cell, x, y);
    }

    public void Save()
    {
        _map.toSave(info, enviromentPath, decorPath);
        new JSONSaveMap().Save(info);
    }
}

public enum CreatingEtap
{
    Start,
    Edit,
    AfterEdit
}
