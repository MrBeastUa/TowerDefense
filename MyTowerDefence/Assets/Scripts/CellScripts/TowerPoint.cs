using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPoint : Cell, IPointerClickHandler
{
    private bool _isEmpty = true;
    private GameObject _tower;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameState state = GameData.Instance.State;
        switch (state)
        {
            case GameState.Game:
                if(_isEmpty)
                {
                    TowersController.Instance.openShop(this);
                }
                break;
            case GameState.MapCreator:
      
                break;
        }
    }

    public void BuildTower(GameObject tower)
    {
        _tower = tower;
        _isEmpty = false;
        Instantiate(tower).transform.position = transform.position;
    }
}
