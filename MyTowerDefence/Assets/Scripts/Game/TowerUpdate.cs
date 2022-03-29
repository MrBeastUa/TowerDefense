using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpdate : MonoBehaviour
{
    private static System.Random random = new System.Random();
    private static TowerUpdate _instance;
    public static TowerUpdate Instance => _instance;
    private int _starsCount = 5;

    [SerializeField]
    private GameObject _stars;
    [SerializeField]
    private GameObject _quizStorage;
    [SerializeField]
    private GameObject _buttonTemplate;
    [SerializeField]
    private Text _problem;

    private Dataset _dataset;
    private Question _question;
    private List<string> _result = new List<string>();

    private void Start()
    {
        _instance = this;
        _dataset = GameData.Instance.CurrentDataset;
    }

    public void FindQuestions()
    {
        _question = _dataset.Questions[Random.Range(0, _dataset.Questions.Count)];
        _problem.text = _question.Problem;
        _result.Add(_question.Result);

        for (int i = 0; i < 3; i++)
        {
            var _tempDataset = _dataset.Questions.Where(x => _result.IndexOf(x.Result) == -1).Select(x => x.Result).ToList();
            _result.Add(_tempDataset[random.Next(0, _tempDataset.Count)]);
        }

        _quizStorage.transform.parent.parent.gameObject.SetActive(true);
        while (_result.Count > 0)
        {
            int index = random.Next(0, _result.Count);
            var button = Instantiate(_buttonTemplate, _quizStorage.transform);
            button.GetComponent<ResultTemplate>().Init(_result[index]);
            _result.RemoveAt(index);
        }
    }

    public void selectResult(string result)
    {
        if (_question.Result == result)
        {
            UIController.correctAnswerInfo++;
            Debug.Log("Правильно");

            TowerStats.setAttackMultiplier(0.05f);
            TowerStats.setAttackSpeedMultiplier(0.1f);
        }
        else
        {
            UIController.wrongAnswersInfo++;
            Debug.Log("Неправильно");

            MonsterStats.setHpMultiplier(0.1f);
            MonsterStats.setSpeedMultiplier(0.5f);
            _starsCount--;
            if (_stars.transform.childCount > 1)
                Destroy(_stars.transform.GetChild(0).gameObject);
            else
            {
                UIController.Instance.EndGame();
                Debug.Log("Ви програли");
            }
        }

        List<Transform> toDelete = new List<Transform>();
        foreach (Transform child in _quizStorage.transform)
            toDelete.Add(child);

        toDelete.ForEach(x => Destroy(x.gameObject));
        _quizStorage.transform.parent.parent.gameObject.SetActive(false);
    }
}
