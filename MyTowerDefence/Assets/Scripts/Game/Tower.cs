using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private TowerStats _stats;
    //[SerializeField]
    //private Monster _target;
    private bool _isChangedTarget = false;
    private bool _isStarted = false;
    List<Monster> _monsters = new List<Monster>();


    private void Awake()
    {
        GetComponent<CircleCollider2D>().radius = _stats.AttackRadius;
    }

    private void Update()
    {
        if (_monsters.Count != 0)
        {
            var vector = new Vector2(_monsters[0].transform.position.x - transform.position.x, _monsters[0].transform.position.y - transform.position.y).normalized;
            float z = -Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.transform.GetComponent<Monster>() != null)
        {
            _monsters.Add(collider.transform.GetComponent<Monster>());
            if (!_isStarted)
            {
                startAttack();
                _isStarted = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (_monsters.Count != 0 && _monsters.IndexOf(collider.transform.GetComponent<Monster>()) != -1)
        {
            _monsters.Remove(collider.transform.GetComponent<Monster>());
        }
    }

    private void startAttack()
    {
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        if (_monsters.Count != 0)
        {
            var bullet = Instantiate(_stats.Bullet);
            bullet.transform.localRotation = transform.localRotation;
            bullet.GetComponent<IBullet>().SetTarget(_stats.AttackDamage, _stats.BulletsSpeed,_stats.AttackRadius ,transform.position, _monsters.First(), _stats.IsAOE, _stats.Radius);
            if (!_isChangedTarget)
            {
                yield return new WaitForSeconds(_stats.AttackSpeed);
                yield return StartCoroutine(Attack());
            }
        }
        else
        {
            _isStarted = false;
            yield break;
        }
    }
}
