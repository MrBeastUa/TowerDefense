using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorOutput : MonoBehaviour
{
    public static ErrorOutput instance;
    [SerializeField]
    private Text errorText;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void OutputError(string error)
    {
        Color color = errorText.color;
        errorText.color = new Color(color.r, color.g, color.b, 1);
        errorText.gameObject.SetActive(true);
        errorText.text = $"Error:{error}";
    }

    
}
