using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new MonsterStats", menuName = "Data/Monster Stats")]
public class MonsterStats: ScriptableObject
{
    [SerializeField]
    private int _maxHp;
    public int MaxHp => _maxHp;

    [SerializeField]
    private int _moveSpeed;
    public int MoveSpeed => _moveSpeed;

    [SerializeField]
    private ElementType _startMonsterType;
    public ElementType StartMonsterType => _startMonsterType;

    [SerializeField]
    private bool _isArmored;
    public bool IsArmored => _isArmored;

    [SerializeField]
    private int _maxArmor;
    public int MaxArmor => _maxArmor;

    [Header("Відсоток шкоди, який приходиться на броню")]
    [SerializeField]
    [Range(0, 1)]
    private float _protectionFactor;


}
