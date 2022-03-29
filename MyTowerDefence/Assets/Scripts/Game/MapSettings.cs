using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void SpawnController();

public class MapSettings : MonoBehaviour
{
    private static MapSettings _instance;
    public static MapSettings Instance => _instance;

    public int currentWave = 1;
    public System.Random randomizer;
    public Transform Map, MonsterList;

    [SerializeField]
    private Transform _map;
    public SpawnController spawnController;
    private void Awake()
    {
        _instance = this;
        createMap();
        spawnController.Invoke();
        GameData.Instance.changeState(GameState.Game);
    }

    private void createMap()
    {
        if (GameData.Instance.CurrentMapData != null)
        {
            GameData.Instance.CurrentMapData.cells.ForEach(x =>
            {
                GameObject gameObject = Instantiate(Resources.Load<GameObject>(x.Path), _map);
                gameObject.transform.position = new Vector2(x.x, x.y);

                if (GameData.Instance.CurrentMapData.portals.Where(y => y.position.x == x.x && y.position.y == x.y).Count() > 0)
                {
                    gameObject.GetComponent<EnemyPortal>().setInfo(GameData.Instance.CurrentMapData.portals.Where(y => y.position.x == x.x && y.position.y == x.y).First().waves,
                                                                   GameData.Instance.CurrentMapData.portals.Where(y => y.position.x == x.x && y.position.y == x.y).First().ways);
                }
            });
            randomizer = new System.Random(GameData.Instance.CurrentMapData.seed);
        }
    }
}
