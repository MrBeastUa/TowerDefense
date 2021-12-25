using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PortalMenuController : MonoBehaviour
{
    [SerializeField]
    private Dropdown _waveNums;
    [SerializeField]
    private InputField _groupDelay, _betwGroupsDelay, _minCountInGroup, _maxCountInGroup;
    [SerializeField]
    private GameObject _monstersContainer;
    [SerializeField]
    private GameObject _monsterInPanelTemp;
    
    private EnemyPortal _currentPortal;
    private MonstersWave _currentWave;
    private int _currentWaveIndex;
    private List<Monster> _addedMonsters = new List<Monster>();

    public EnemyPortal CurrentPortal
    {
        private get => _currentPortal;
        set
        {
            _currentPortal = value;
            changeWave(0);
            _waveNums.value = 0;
        }
    }

    private void Start()
    {
        changeWave(0);
    }

    public void changeWave(int num)
    {      
        _currentWaveIndex = num;
        _currentWave = _currentPortal.getWave(num);
        _minCountInGroup.text = _currentWave.minCountInGroup.ToString();
        _maxCountInGroup.text = _currentWave.maxCountInGroup.ToString();
        _groupDelay.text = _currentWave.GroupDelay.ToString();
        _betwGroupsDelay.text = _currentWave.DelayBetwGroups.ToString();
        GetAllChilds(_monstersContainer.transform).ForEach(x => {
            Debug.Log("Delete");
            Destroy(x.gameObject);
        });
        _addedMonsters.Clear();

        loadMonstersFromPortal();
    }

    public void cancelChanges()
    {
        changeWave(_currentWaveIndex);
    }

    public List<GameObject> getMonstersInWave()
    {
        return _currentWave.Monsters.Select(x => x.Key).ToList();
    }

    #region MonsterPanelSettings
    public void addToMonsterPanel(Monster monster, int count = 0)
    {
        if (!_addedMonsters.Find(x => monster))
        {
            _addedMonsters.Add(monster);
            GameObject template = Instantiate(_monsterInPanelTemp, _monstersContainer.transform);
            template.GetComponent<MonsterPanelController>().addMonster(monster, count);
        }
        else
        {
            Debug.Log("Error");
            ErrorOutput.instance.OutputError("This monster was already added");
        }
    }

    private void loadMonstersFromPortal()
    {
        foreach (var monster in _currentWave.Monsters)
            addToMonsterPanel(monster.Key.GetComponent<Monster>(), monster.Value);
    }

    public void saveMonstersToPortal()
    {
        _currentWave.Monsters.Clear();
        foreach(var temp in GetAllChilds(_monstersContainer.transform))
        {
            var controller = temp.GetComponent<MonsterPanelController>();
            _currentWave.Monsters.Add(controller.returnData().Key, controller.returnData().Value);
        }
        _currentWave.onMonsterChange();
    }
    #endregion

    public void saveWaveData()
    {
        string inputFieldError = "must be integer number then greater than 0";

        uint temp = 0;
        if (uint.TryParse(_minCountInGroup.text, out temp) && temp > 0)
            _currentWave.minCountInGroup = (int)temp;
        else
        {
            ErrorOutput.instance.OutputError("Minimal count of monsters" + inputFieldError);
            return;
        }

        if (uint.TryParse(_maxCountInGroup.text, out temp) && temp > 0)

            _currentWave.maxCountInGroup = (int)temp;
        else
        {
            ErrorOutput.instance.OutputError("Maximal count of monsters" + inputFieldError);
            return;
        }

        float temp_2 = 0;
        if (float.TryParse(_groupDelay.text, out temp_2) && temp_2 > 0)
            _currentWave.GroupDelay = temp_2;
        else
        {
            ErrorOutput.instance.OutputError("Group delay" + inputFieldError);
            return;
        }

        if (float.TryParse(_betwGroupsDelay.text, out temp_2) && temp_2 > 0)
            _currentWave.DelayBetwGroups = temp_2;
        else
        {
            ErrorOutput.instance.OutputError("Delay between groups" + inputFieldError);
            return;
        }
    }

    private List<Transform> GetAllChilds(Transform transform)
    {
        List<Transform> result = new List<Transform>();
        for(int i = 0; i < transform.childCount; i++)
            result.Add(transform.GetChild(i));

        return result;
    }
}
