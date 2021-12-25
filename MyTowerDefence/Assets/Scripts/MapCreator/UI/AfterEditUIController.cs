using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterEditUIController : MonoBehaviour
{
    private static AfterEditUIController _instance;
    public static AfterEditUIController Instance => _instance;

    private AfterEditController _mainController;

    [SerializeField]
    private GameObject _portalMenu;
    [SerializeField]
    private PopUpMenu _monstersStorage;
    [SerializeField]
    private GameObject _monstersPanel;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _mainController = AfterEditController.Instance;
    }

    public void openPortalMenu(EnemyPortal portal)
    {
        _portalMenu.GetComponent<PortalMenuController>().CurrentPortal = portal;
        _portalMenu.SetActive(true);
    }
    public void closePortalMenu()
    {
        _portalMenu.SetActive(false);
    }

    public void openMonsterPanel()
    {
        _monstersPanel.SetActive(true);
    }

    public void closeMonstersPanel()
    {
        _monstersPanel.SetActive(false);
    }

    public void openMonsterStorage()
    {
        _monstersStorage.StartPopUp();
    }

    public void closeMonsterStorage()
    {
        _monstersStorage.CloseMenu();
    }
}
