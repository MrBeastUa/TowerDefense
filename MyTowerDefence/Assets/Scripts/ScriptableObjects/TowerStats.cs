using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : ScriptableObject
{
    [SerializeField]
    private int _attackDamage;
    public int AttackDamage => _attackDamage;

    [SerializeField]
    private float _attackSpeed;
    public float AttackSpeed => _attackSpeed;

    [SerializeField]
    private ElementType _towerElement;
    public ElementType TowerElement => _towerElement;

    [SerializeField]
    private bool _isAOE;
    public bool IsAOE => _isAOE;

    [SerializeField]
    private float _radius;
    public float Radius => _radius;
}
