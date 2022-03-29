using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class EnemyPortal : Cell, IPointerClickHandler
{
    public MonstersWave[] MonstersWaves = new MonstersWave[5] { new MonstersWave(), new MonstersWave(), new MonstersWave(), new MonstersWave(), new MonstersWave()};
    public List<Way> Ways = new List<Way>();
    private int monstersCount = 0;

    public void setInfo(MonstersWave[] waves, List<Way> ways)
    {
        MonstersWaves = waves;
        foreach (var wave in MonstersWaves)
            wave.LoadMonsters(wave.Monsters);
       
        Ways = ways;
        MapSettings.Instance.spawnController += startSpawn;
    }

    public MonstersWave getWave(int id)
    {
        return MonstersWaves[id];
    }

    private void openPortalMenu()
    {
        AfterEditUIController.Instance.openPortalMenu(this);
    }

    public PortalInfo getInfo(int[,] map)
    {
        PortalInfo info = new PortalInfo();

        info.position = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        info.waves = MonstersWaves;
        info.FindWays(map, this);

        return info;
    }

    #region In game spawn logic
    private void startSpawn()
    {
        StartCoroutine(startWave());
    }

    public IEnumerator startWave()
    {
        for (int i = 1; i <= 5; i++)
        {
            yield return new WaitForSeconds(5);
            UIController.ChangeWave(i);
            yield return StartCoroutine(createGroup());

            yield return new WaitWhile(() => UIController.Map.childCount > 0);
            
            MapSettings.Instance.currentWave++;
        }

        UIController.ChangeWave(5);
        UIController.Instance.EndGame();
    }
    private IEnumerator createGroup()
    {

        while (MonstersWaves[MapSettings.Instance.currentWave - 1].Monsters.Select(x => x.Count).Sum() > 0)
        {
            if (MonstersWaves[MapSettings.Instance.currentWave - 1].Monsters.Select(x => x.Count).Sum() >= MonstersWaves[MapSettings.Instance.currentWave - 1].maxCountInGroup)
                monstersCount = MapSettings.Instance.randomizer.Next(MonstersWaves[MapSettings.Instance.currentWave - 1].minCountInGroup, MonstersWaves[MapSettings.Instance.currentWave - 1].maxCountInGroup + 1);
            else 
                monstersCount = MonstersWaves[MapSettings.Instance.currentWave - 1].Monsters.Select(x => x.Count).Sum();

            yield return StartCoroutine(spawnMonster());
            yield return new WaitForSeconds(MonstersWaves[MapSettings.Instance.currentWave].DelayBetwGroups);
        }
    }

    private IEnumerator spawnMonster()
    { 
        var currentMonsterInd = MapSettings.Instance.randomizer.Next(0, MonstersWaves[MapSettings.Instance.currentWave - 1].Monsters.Count);
        var currentMonster = MonstersWaves[MapSettings.Instance.currentWave - 1].getMonsterById(currentMonsterInd);

        var monster = Instantiate(currentMonster.Key , MapSettings.Instance.MonsterList);
        monster.GetComponent<Monster>().Way = Ways[MapSettings.Instance.randomizer.Next(0, Ways.Count)].way;
        monster.transform.position = transform.position;

        yield return new WaitForSeconds(MonstersWaves[MapSettings.Instance.currentWave - 1].GroupDelay);

        if (monstersCount > 0)
        {
            monstersCount--;
            MonstersWaves[MapSettings.Instance.currentWave - 1].changeCount(currentMonsterInd, currentMonster.Value - 1);
            yield return StartCoroutine(spawnMonster());
        }
        
    }
    #endregion

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        GameState state = GameData.Instance.State;
        switch (state)
        {
            //case GameState.Game:
            //    checkPortalInfo();
            //    break;
            case GameState.MapCreator:
                openPortalMenu();
                break;
        }
    }
}

[System.Serializable]
public class MonstersWave
{
    public Dictionary<GameObject, int> _monsters = new Dictionary<GameObject, int>();
    public List<EntityToSpawn> Monsters = new List<EntityToSpawn>();

    public int minCountInGroup, maxCountInGroup;
    public float GroupDelay;
    public float DelayBetwGroups;

    public void LoadMonsters(List<EntityToSpawn> entities)
    {
        //if (value.GroupBy(x => x.Path).Select(x => x.First()).Count() == value.Count())
        _monsters.Clear();

        
        entities.ForEach(x => {
            if(Resources.Load<GameObject>(x.Path))
                _monsters.Add(Resources.Load<GameObject>(x.Path), x.Count);
        });
    }
    private void changeList()
    {
        Monsters.Clear();
        Monsters = _monsters.Select(x => new EntityToSpawn(x.Key.name, x.Value)).ToList(); 
    }

    public List<GameObject> getAllMonsters()
    {
        return _monsters.Keys.ToList();
    }
    public KeyValuePair<GameObject, int> getMonsterById(int id)
    {
        return new KeyValuePair<GameObject, int>(_monsters.Keys.ToList()[id], _monsters[_monsters.Keys.ToList()[id]]);
    }

    public void changeCount(int id, int value)
    {
        _monsters[_monsters.Keys.ToList()[id]] = value;
        changeList();
    }

    public void removeById(int id)
    {
        _monsters.Remove(_monsters.Keys.ToList()[id]);
        changeList();
    }

    public void addMonster(GameObject gameObject, int count)
    {
        _monsters.Add(gameObject, count);
        changeList();
    }

    public void clearMonsterList()
    {
        _monsters.Clear();
        Monsters.Clear();
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
    public string Path = DataPath.Monsters;
    public int Count = 1;
    public EntityToSpawn(string path, int count)
    {
        Path += path;
        Count = count;
    }
}