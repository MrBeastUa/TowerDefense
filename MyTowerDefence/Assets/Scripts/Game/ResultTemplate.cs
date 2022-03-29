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
        _text.text = text;
    }

    public void selectResult()
    {
        TowerUpdate.Instance.selectResult(_result);
    }
}
