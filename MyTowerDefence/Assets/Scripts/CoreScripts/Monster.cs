using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamagable
{
    [SerializeField]
    protected MonsterStats _stats;
    [SerializeField]
    protected Way way;

    protected int currentHp;
    protected int currentArmor;
    protected ElementType _currentType;

    public void Dead()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage()
    {
        throw new System.NotImplementedException();
    }
}

public interface IDamagable
{
    void TakeDamage();
}

public enum ElementType
{
    Fire,
    Water,
    Wind,
    Earth,
    Death
}