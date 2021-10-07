using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorStringScript : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(changeOpacity());
              
    }

    private IEnumerator changeOpacity()
    {
        while (GetComponent<Text>().color.a > 0)
        {
            Color color = GetComponent<Text>().color;
            GetComponent<Text>().color = new Color(color.r, color.g, color.b, color.a - 0.05f);
            yield return new WaitForSeconds(0.1f);
            
        }
        gameObject.SetActive(false);
    }
}
