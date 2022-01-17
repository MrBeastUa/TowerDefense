using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData _instance;
    public static GameData Instance => _instance;

    public GameState State { get; private set; } = GameState.Menu;
    public Dataset CurrentDataset;
    public GameProcessData CurrentMapData;

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
    }

    public void changeState(GameState state)
    {
        if (State == state)
            return;

        State = state;
    }

}

public enum GameState
{
    Menu,
    Game,
    MapCreator
}
