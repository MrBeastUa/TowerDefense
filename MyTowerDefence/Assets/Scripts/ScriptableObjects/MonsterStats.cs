using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new MonsterStats", menuName = "Data/Monster Stats")]
public class MonsterStats: ScriptableObject
{
    public static float _speedMultiplier = 1;
    public static float _hpMultiplier = 1;

    [SerializeField]
    private int _maxHp;
    public int MaxHp => (int)(_maxHp* _multiplierEfficiencyHp * _hpMultiplier);

    [Range(1,99)]
    [SerializeField]
    private int _moveSpeed = 1;
    public float MoveSpeed => ((float)_moveSpeed/100 * _multiplierEfficiencySpeed * _speedMultiplier);

    [Header("Standart HP multiplier")]
    [SerializeField]
    private float _multiplierEfficiencyHp;

    [Header("Standart move speed multiplier")]
    [SerializeField]
    private float _multiplierEfficiencySpeed;
   
    public static void setHpMultiplier(float value)
    {
        _hpMultiplier += value;
    }

    public static void setSpeedMultiplier(float value)
    {
        _speedMultiplier += value;
    }
}
