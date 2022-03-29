using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData _instance;
    public static GameData Instance => _instance;

    public GameState State { get; private set; } = GameState.Menu;
    public Dataset CurrentDataset = new Dataset() { name = "Test", stars = 0, Questions = new List<Question>() { new Question("Question 1", "Result 1"), new Question("Question 2", "Result 2"), new Question("Question 3", "Result 3"), new Question("Question 4", "Result 4") } };
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
