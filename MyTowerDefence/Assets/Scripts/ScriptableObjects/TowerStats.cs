using UnityEngine;

[CreateAssetMenu(fileName = "new TowerStats", menuName = "Data/Tower Stats")]
public class TowerStats : ScriptableObject
{
    private static float _attackDamageMultiplier = 0;
    private static float _attackSpeedMultiplier = 0;

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
    public float AttackSpeed => 60 / _attackSpeed + 60 / _attackSpeed * _attackSpeedMultiplier * _attackSpeedStartMultiplier;

    [Header("Tower attack radius (in cells)")]
    [Min(0)][SerializeField]
    private float _radius;
    public float Radius => _radius;

    [Header("")]
    [Min(0)][SerializeField]
    private float _attackDamageStartMultiplier = 0;
    [Min(0)][SerializeField]
    private float _attackSpeedStartMultiplier = 0;

    public static void setAttackMultiplier(float value)
    {
        _attackDamageMultiplier += value;
    }

    public static void setAttackSpeedMultiplier(float value)
    {
        _attackSpeedMultiplier += value;
    }
}
