using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsSetings : MonoBehaviour
{
    [SerializeField]
    public InputField _easey, _normal, _hard;

    public void cancel()
    {
        _easey.text = "";
        _normal.text = "";
        _hard.text = "";
        gameObject.SetActive(false);
    }

    public void save()
    {
        int easey, normal, hard;
        if (int.TryParse(_easey.text, out easey) && int.TryParse(_normal.text, out normal) && int.TryParse(_hard.text, out hard))
        {
            if (easey + normal + hard <= 100)
            {
                if (MapCreator.Instance.createdMap.questionsDifficultyInPercents.Count == 0)
                {
                    MapCreator.Instance.createdMap.questionsDifficultyInPercents.Add(100 - hard - normal);
                    MapCreator.Instance.createdMap.questionsDifficultyInPercents.Add(normal);
                    MapCreator.Instance.createdMap.questionsDifficultyInPercents.Add(hard);
                }
                else
                {
                    MapCreator.Instance.createdMap.questionsDifficultyInPercents[0] = 100 - hard - normal;
                    MapCreator.Instance.createdMap.questionsDifficultyInPercents[1] = normal;
                    MapCreator.Instance.createdMap.questionsDifficultyInPercents[2] = hard;
                }

            }
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("ErrorWtf");
        }
    }
}
