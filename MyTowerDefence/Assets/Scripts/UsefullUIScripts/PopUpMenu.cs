using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMenu : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10;
    
    public bool isOpen = false;
    private Vector3 _startPosition;
    

    private void Start()
    {
        _startPosition = transform.position;
    }

    private IEnumerator waitAndMove()
    {
        yield return new WaitForSeconds(0.01f);
        if (!isOpen)
        {
            if (transform.position.x < 0)
            {
                transform.position = new Vector2(transform.position.x + _speed, transform.position.y);
                StartCoroutine(waitAndMove());
            }
            else
            {
                isOpen = true;
            }
        }
        else
        {
            if (transform.position.x > _startPosition.x)
            {
                transform.position = new Vector2(transform.position.x - _speed * 1.5f, transform.position.y);
                StartCoroutine(waitAndClose());
                
            }
            else
            {
                isOpen = false;
            }
        }
    }

    private IEnumerator waitAndClose()
    {
        yield return new WaitForSeconds(0.01f);
        if (transform.position.x > _startPosition.x)
        {
            transform.position = new Vector2(transform.position.x - _speed * 1.5f, transform.position.y);
            StartCoroutine(waitAndClose());
        }
        else
        {
            isOpen = false;
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
