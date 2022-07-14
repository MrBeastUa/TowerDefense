using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTemplate : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    private string _result;
    public void Init(string text)
    {
        _result = text;
        _text.text = text.Substring(0, (text.Length <= 50) ? text.Length : 50) + "...";
    }

    public void openResult()
    {
        TowerUpdate.Instance.openResult(_result);
    }
}
