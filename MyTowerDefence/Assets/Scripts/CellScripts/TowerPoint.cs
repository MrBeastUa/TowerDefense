using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPoint : Cell, IPointerClickHandler
{
    private bool _isEmpty = false;
    private GameObject _tower;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameState state = GameData.Instance.State;
        switch (state)
        {
            case GameState.Game:
                if(!_isEmpty)
                {
                    TowersController.Instance.openShop(this);
                    _isEmpty = true;
                }
                break;
            case GameState.MapCreator:
      
                break;
        }
    }

    public void BuildTower(GameObject tower)
    {
        _tower = tower;
        Instantiate(tower).transform.position = transform.position;
    }
}
