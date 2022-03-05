using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new TowerStats", menuName = "Data/Tower Stats")]
public class TowerStats : ScriptableObject
{
    private static float _attackDamageMultiplier = 1;
    private static float _attackSpeedMultiplier = 1;
    private static float _aoeRadiusMultiplier = 1;

    [SerializeField]
    GameObject _bullet;
    public GameObject Bullet => _bullet;

    [SerializeField]
    private float _bulletFlyightSpeed = 0.1f;
    public float BulletsSpeed => _bulletFlyightSpeed;

    [SerializeField][Min(0)]
    private float _attackRadius = 1;
    public float AttackRadius => _attackRadius;

    [Min(0)][SerializeField]
    private int _attackDamage;
    public int AttackDamage => (int)(_attackDamage + _attackDamage * _attackDamageMultiplier * _attackDamageStartMultiplier);

    [Header("Number of attacks per second")]
    [Min(0)][SerializeField]
    private float _attackSpeed;
    public float AttackSpeed => 60/_attackSpeed * _attackSpeedMultiplier * _attackSpeedStartMultiplier;

    [Header("Projectile explosion radius (in cells)")]
    [SerializeField]
    private bool _isAOE;
    public bool IsAOE => _isAOE;

    [Header("Tower attack radius (in cells)")]
    [Min(0)][SerializeField]
    private float _radius;
    public float Radius => _radius;

    [Header("")]
    [Min(0)][SerializeField]
    private float _attackDamageStartMultiplier = 1;
    [Min(0)][SerializeField]
    private float _attackSpeedStartMultiplier = 1;
    [Min(0)][SerializeField]
    private float _radiusStartMultiplier = 1;

    public static void setAttackMultiplier(float value)
    {
        _attackDamageMultiplier += value;
    }

    public static void setAoeRadiusMultiplier(float value)
    {
        _aoeRadiusMultiplier += value;
    }

    public static void setAttackSpeedMultiplier(float value)
    {
        _attackSpeedMultiplier += value;
    }
}
