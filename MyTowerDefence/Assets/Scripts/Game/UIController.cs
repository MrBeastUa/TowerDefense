using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController _instance;
    public static UIController Instance => _instance;

    public static int wrongAnswersInfo = 0, correctAnswerInfo = 0, livesInfo = 3, wavesInfo = 0;

    [SerializeField]
    private GameObject _results;
    [SerializeField]
    private Text _wrongText, _correctText, _livesText, _wavesText, _score;

    [SerializeField]
    private Transform _map;
    public static Transform Map { get; private set; }

    [SerializeField]
    private GameObject _healthStorage;
    private static GameObject _health;

    [SerializeField]
    private Text _waveNum;
    private static Text _waveNumber;

    private bool _isPaused = false;

    private void Awake()
    {
        _instance = this;
        _health = _healthStorage;
        _waveNumber = _waveNum;
        Map = _map;
    }

    public void Pause()
    {
        Time.timeScale = (_isPaused) ? 1f : 0f;
        _isPaused = (_isPaused) ? false : true;
    }

    public void UpdateTowers()
    {
        TowerUpdate.Instance.FindQuestions();
    }

    public void EndGame()
    {
        _results.SetActive(true);
        _score.text = (5 - wrongAnswersInfo).ToString();

        _correctText.text = correctAnswerInfo.ToString();
        _wrongText.text = wrongAnswersInfo.ToString();
        _wavesText.text = wavesInfo.ToString();
        _livesText.text = livesInfo.ToString();

        if (livesInfo == 0)
            _score.text = "0";

        Pause();
    }

    public static void ChangeHP()
    {
        if (_health.transform.childCount > 1)
        {
            Destroy(_health.transform.GetChild(0).gameObject);
            livesInfo--;
        }
        else
        {
            livesInfo--;
            Debug.Log("Ви програли");
            _instance.EndGame();
        }
    }

    public static void ChangeWave(int num)
    {
        _waveNumber.text = num.ToString();
        wavesInfo++;
    }
}
