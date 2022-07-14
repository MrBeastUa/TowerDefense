using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommonBullet : MonoBehaviour, IBullet
{
    TowerStats _stats;
    private Vector2 _startPoint, _targetPosition;
    private Monster _target;

    private Vector2 _lastVector = new Vector2(0, 0);

    private void FixedUpdate()
    {
        BulletFlight();
        Hit();
    }

    private void Hit()
    {

        if (Vector2.Distance(_startPoint, _targetPosition) <= Vector2.Distance(_startPoint, transform.position))
        {
            if (_target != null)
                _target.TakeDamage(_stats.AttackDamage);
            Destroy(gameObject);
        }
    }

    private void BulletFlight()
    {
        Vector2 vector;
        if (_target != null)
            vector = new Vector2(_target.transform.position.x - _startPoint.x, _target.transform.position.y - _startPoint.y).normalized;
        else
            vector = new Vector2(_targetPosition.x - _startPoint.x, _targetPosition.y - _startPoint.y).normalized;

        if (vector.x != _lastVector.x || vector.y != _lastVector.y)
        {
            _startPoint = transform.position;
            _lastVector = vector;
            if(_target != null)
                _targetPosition = _target.transform.position;
        }

        transform.position = new Vector2(transform.position.x, transform.position.y) + vector * _stats.BulletsSpeed;

    }

    public void SetTarget(TowerStats stats, Vector3 startPosition, IEnumerable<Monster> targets)
    {
        _stats = stats;
        transform.position = startPosition;
        _startPoint = startPosition;
        _target = targets.First();
    }
}

public interface IBullet
{
    void SetTarget(TowerStats stats, Vector3 startPosition, IEnumerable<Monster> targets);
}
