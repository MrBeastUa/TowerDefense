using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreator : MonoBehaviour
{
    public static MapCreator instance;
    private PhaseOfCreation phaseOfCreation;
    public GameObject currentCellToPlace;
    public Transform map;

    [SerializeField]
    private GameObject _blankCell;
    [SerializeField]
    private Canvas _firstPhaseOfCreating, _secondPhaseOfCreating, _thirdPhaseOfCreating;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        phaseOfCreation = PhaseOfCreation.SettingBasicConditions;
        _firstPhaseOfCreating.gameObject.SetActive(true);
        _secondPhaseOfCreating.gameObject.SetActive(false);
        //_thirdPhaseOfCreating.enabled = false;
    }

    public void BuildStartMap()
    {
        if(phaseOfCreation == PhaseOfCreation.SettingBasicConditions)
        {
            uint height = _firstPhaseOfCreating.GetComponent<BaseInputCanvasInfo>().Height;
            uint width = _firstPhaseOfCreating.GetComponent<BaseInputCanvasInfo>().Width;
            string name = _firstPhaseOfCreating.GetComponent<BaseInputCanvasInfo>().Name;

            if (width > 0 && height > 0 && name != "")
            {
                phaseOfCreation = PhaseOfCreation.CreateMap;
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                        Instantiate(_blankCell,map).transform.position = new Vector2(height - 1 - i, j);
                CameraMovement.instance.initCameraPosition((float)height / 2,(float)width / 2);
                ErrorOutput.instance.OutputError("Hi, bitches");
                _secondPhaseOfCreating.gameObject.SetActive(true);
                _firstPhaseOfCreating.gameObject.SetActive(false);
            }
        }
    }

    
}


public enum PhaseOfCreation
{
    SettingBasicConditions,
    CreateMap,
    SettingMainConditions
}
