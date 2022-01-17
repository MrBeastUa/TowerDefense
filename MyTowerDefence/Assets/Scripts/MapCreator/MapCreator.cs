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
    public GameProcessData createdMap = new GameProcessData();

    [SerializeField]
    private Canvas _startCanvas, _editMapCanvas, _afterEditCanvas;
    private Map _map;
    private string enviromentPath = "";

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
        _instance = this;
    }

    private void Start()
    {
        _pr = GetComponent<Physics2DRaycaster>();
        _pr.enabled = false;
        _startCanvas.gameObject.SetActive(true);
        GameData.Instance.changeState(GameState.MapCreator);
    }

    public void StartEditMode(string name, int x, int y ,int seed, Package enviroment)
    {
        //Save path of packages
        enviromentPath = enviroment.Path;

        CameraMove.instance.CameraStartPosition = new Vector3(((float)x)/2,((float)y)/2,0);

        createdMap.seed = seed;
        _editMapCanvas.gameObject.SetActive(true);
        _editMapCanvas.GetComponent<EditCanvasContoller>().AddPackages(enviroment);

        _map.Name = name;
        _map.Size = new Vector2Int(x,y);
       
        _startCanvas.gameObject.SetActive(false);
    }

    public void StartAfterEditMode()
    {
        _pr.enabled = true;
        _afterEditCanvas.gameObject.SetActive(true);
        _editMapCanvas.gameObject.SetActive(false);
    }

    public void SetCell(Cell cell,int x, int y)
    {
        _map.changeCell(cell, x, y);
    }

    public void Save()
    {
        _map.toSave(enviromentPath);

        //Debug.Log(createdMap.questionsDifficultyInPercents.Count);
        //Debug.Log(createdMap.seed);
        //Debug.Log(createdMap.map.Name);
        //Debug.Log(createdMap.map.size);
        //Debug.Log(createdMap.map.cells.Count);
        //Debug.Log(createdMap.portals.Count);
        
        new JSONSaveMap().Save(createdMap);
    }
}

[System.Serializable]
public class GameProcessData
{
    public string Name;
    public string Description;
    public Vector2Int size;
    public int seed;
    public List<CellInfo> cells = new List<CellInfo>();
    public List<int> questionsDifficultyInPercents = new List<int>();
    public List<PortalInfo> portals = new List<PortalInfo>();

    public GameProcessData() { }

    public GameProcessData(GameProcessData data)
    {
        questionsDifficultyInPercents = data.questionsDifficultyInPercents;
        Name = data.Name;
        Description = data.Description;
        seed = data.seed;
        size = data.size;
        cells = data.cells;
        portals = data.portals;
    }
}
