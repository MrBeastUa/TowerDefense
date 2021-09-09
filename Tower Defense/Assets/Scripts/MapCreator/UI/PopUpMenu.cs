using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMenu : MonoBehaviour
{
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private IEnumerator waitAndMove()
    {
        yield return new WaitForSeconds(0.01f);
        if (transform.position.x < 0)
        {
            transform.position = new Vector2(transform.position.x + 10, transform.position.y);
            StartCoroutine(waitAndMove());
        }
    }

    private IEnumerator waitAndClose()
    {
        yield return new WaitForSeconds(0.01f);
        if (transform.position.x > _startPosition.x)
        {
            transform.position = new Vector2(transform.position.x - 15, transform.position.y);
            StartCoroutine(waitAndClose());
        }
    } 

    public void StartPopUp()
    {
        StartCoroutine(waitAndMove());
    }

    public void CloseMenu()
    {
        StartCoroutine(waitAndClose());
    }
}
