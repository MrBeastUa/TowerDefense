using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour, IDamagable
{
    [SerializeField]
    private Slider hpBar; 
    [SerializeField]
    private MonsterStats _stats;
    [SerializeField]
    public List<Vector2Int> Way { private get; set; }
    

    private float healthPercents = 1;
    private int _nextPoint = 1;
    private bool _isEnd = false;
    private bool _isDead = false;

    private void FixedUpdate()
    {
        move();
    }

    public void TakeDamage(int inputDamage)
    {
        if (!_isDead)
        {
            if (hpBar.value == 1)
                hpBar.gameObject.SetActive(true);
            //Debug.Log("Hit");
            healthPercents -= (float)(inputDamage) / (float)(_stats.MaxHp);
            hpBar.value = healthPercents;
            ///Debug.Log(healthPercents);
            if (healthPercents <= 0)
            {
                _isDead = true;
                Destroy(gameObject);
            }
        }
    }

    public void changePoint()
    {
        if(Way.Count <= _nextPoint + 1)
        {
            Destroy(gameObject);
        }
        _nextPoint++;
    }

    private void move()
    {
        if (!_isEnd)
        {
            Vector2 vector = new Vector2(Way[_nextPoint].x - Way[_nextPoint - 1].x, Way[_nextPoint].y - Way[_nextPoint - 1].y).normalized;
            if (Vector2.Distance(Way[_nextPoint - 1], new Vector2(transform.position.x, transform.position.y) + vector * _stats.MoveSpeed) <= Vector2.Distance(Way[_nextPoint - 1], Way[_nextPoint]))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y) + vector * _stats.MoveSpeed;
                hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            }
            else
                changePoint();
        }
    }
}

public interface IDamagable
{
    void TakeDamage(int inputDamage);
}