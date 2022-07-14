using UnityEngine;

[CreateAssetMenu(fileName = "new MonsterStats", menuName = "Data/Monster Stats")]
public class MonsterStats: ScriptableObject
{
    public static float _speedMultiplier = 0;
    public static float _hpMultiplier = 0;

    [SerializeField]
    private int _maxHp;
    public int MaxHp => (int)(_maxHp + _maxHp* _multiplierEfficiencyHp * _hpMultiplier);

    [Range(0,99)]
    [SerializeField]
    private float _moveSpeed = 1;
    public float MoveSpeed => ((float)_moveSpeed / 100 + (float)_moveSpeed/100 * _multiplierEfficiencySpeed * _speedMultiplier);

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
