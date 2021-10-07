using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    //відповідає за передачу інформації між етапами побудови мапи
    private static MapCreator _instance;
    public static MapCreator Instance => _instance;

    [SerializeField]
    private Canvas _startCanvas, _editMapCanvas;
    private Map _map;
    private MapInfo info = new MapInfo();

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
        _startCanvas.gameObject.SetActive(true);
    }

    public void StartEditMode(string name, int x, int y, Package enviroment, Package decor = null)
    {
        //Save path of packages
        info.enviromentPath = enviroment.Path;
        if(decor != null)
        info.decorPath = decor.Path;

        CameraMove.instance.CameraStartPosition = new Vector3(((float)x)/2,((float)y)/2,0);
        
        _editMapCanvas.gameObject.SetActive(true);
        _editMapCanvas.GetComponent<EditCanvasContoller>().AddPackages(enviroment, decor);

        _map.Name = name;
        _map.Size = new Vector2Int(x,y);
       
        _startCanvas.gameObject.SetActive(false);
      
    }

    public void SetCell(Cell cell,int x, int y)
    {
        _map.changeCell(cell, x, y);
    }

    public void Save()
    {
        _map.toSave(info);
        new SaveAndLoadMap().Save(info);
    }
}
