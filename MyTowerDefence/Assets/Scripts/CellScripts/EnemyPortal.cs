using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class EnemyPortal : Cell, IPointerClickHandler
{
    private MonstersWave[] _monstersWaves = new MonstersWave[5] { new MonstersWave(), new MonstersWave(), new MonstersWave(), new MonstersWave(), new MonstersWave()};
    public List<Way> Ways = new List<Way>();

    public void FindWay(int[,] map)
    {
        PortalInfo.FindWays(map, this);
    }

    public MonstersWave getWave(int id)
    {
        return _monstersWaves[id];
    }

    public PortalInfo GetInfo
    {
        get
        {
            PortalInfo info = new PortalInfo();

            info.waves = _monstersWaves;
            info.ways = Ways;

            return info;
        }
    }

    private void openPortalMenu()
    {
        AfterEditUIController.Instance.openPortalMenu(this);
    }

    private void checkPortalInfo()
    {

    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        GameState state = GameData.Instance.State;
        switch (state)
        {
            case GameState.Game:
                checkPortalInfo();
                break;
            case GameState.MapCreator:
                openPortalMenu();
                break;
        }
    }
}

[System.Serializable]
public class MonstersWave
{
    public Dictionary<GameObject, int> Monsters = new Dictionary<GameObject, int>();
    [SerializeField]
    private EntityToSpawn[] monsters;
    public int minCountInGroup, maxCountInGroup;
    public float GroupDelay;
    public float DelayBetwGroups;


    public void onMonsterChange()
    {
        monsters = Monsters.Select(x => new EntityToSpawn(x.Key.name, x.Value)).ToArray();
    }
}

[System.Serializable]
public class MonstersWaveData
{
    public List<EntityToSpawn> Entities;
    public float StackDelay;
    public float DelayBetwStacks;
}


[System.Serializable]
public class EntityToSpawn//інформація про кожного монстра, який появиться зі спавнера для збереження в JSON
{
    public string Path = "Assets/Resources/Monsters/";
    public int Count = 1;
    public EntityToSpawn(string path, int count)
    {
        Path += path;
        Count = count;
    }
}