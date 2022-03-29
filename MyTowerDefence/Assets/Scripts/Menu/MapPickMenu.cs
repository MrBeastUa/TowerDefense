using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapPickMenu : MonoBehaviour
{
    [SerializeField]
    private Dropdown _datasetsContainer;
    [SerializeField]
    private Image[] _stars = new Image[5];
    [SerializeField]
    private GameObject _mapsContainer;
    [SerializeField]
    private GameObject _mapTemplate;
    [SerializeField]
    private Image _activeStar, _unactiveStar;

    private List<Dataset> datasets = new List<Dataset>();
    private List<GameProcessData> mapsData = new List<GameProcessData>(); 
    private int _datasetInd = 0;

    private void Start()
    {
        mapsData = Resources.LoadAll<TextAsset>(Links.mapsFolder).Select(x => new JSONSaveMap().Load(x)).ToList();
        loadMaps();
        //loadDatasets();
    }

    private void Update()
    {
        
    }

    private void loadMaps()
    {
        mapsData.ForEach(x =>
        {
            var map = Instantiate(_mapTemplate, _mapsContainer.transform);
            map.GetComponent<MapDataController>().Init(x); 
        });
    }

    private void loadDatasets()
    {
        datasets.ForEach(x => _datasetsContainer.options.Add(new Dropdown.OptionData() { text = x.name }));
        resetStars(0);
    }

    public void resetStars(int value)
    {
        for (int i = 0; i < _stars.Length; i++)
            _stars[i].sprite = i < datasets[value].stars ? _activeStar.sprite : _unactiveStar.sprite;

        _datasetInd = value;
    }
}
