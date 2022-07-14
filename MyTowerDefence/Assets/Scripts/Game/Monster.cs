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
    public MonsterStats Stats => _stats;

    private Animator animator;
    private float healthPercents = 1;
    private int _nextPoint = 1;
    private bool _isEnd = false;
    private bool _isDead = false;
    private bool _rightRotation = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        move();
    }

    public void TakeDamage(int inputDamage)
    {
        if (hpBar.value == 1)
            hpBar.gameObject.SetActive(true);
        healthPercents -= (float)(inputDamage) / (float)(_stats.MaxHp);
        hpBar.value = healthPercents;
        if (healthPercents <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void changePoint()
    {
        if(Way.Count <= _nextPoint + 1)
        {
            UIController.ChangeHP();
            Destroy(gameObject);
        }
        _nextPoint++;
    }

    private void move()
    {
        if (!_isEnd)
        {
            Vector2 vector = new Vector2(Way[_nextPoint].x - Way[_nextPoint - 1].x, Way[_nextPoint].y - Way[_nextPoint - 1].y).normalized;

            if (vector.x < 0 && _rightRotation)
            {
                _rightRotation = false;
                gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
            }
            else if (vector.x > 0 && !_rightRotation)
            {
                _rightRotation = true;
                gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
            }

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