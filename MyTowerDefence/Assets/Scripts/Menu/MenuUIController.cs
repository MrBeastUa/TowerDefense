using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _buttonsStorage;
    [SerializeField]
    private GameObject _mapStorage;

    public void OpenScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void openButtons()
    {
        _buttonsStorage.SetActive(true);
    }

    public void closeButtons()
    {
        _buttonsStorage.SetActive(false);
    }

    public void openMapsMenu()
    {
        _mapStorage.SetActive(true);
    }

    public void closeMapsMenu()
    {
        _mapStorage.SetActive(false);
    }
}
