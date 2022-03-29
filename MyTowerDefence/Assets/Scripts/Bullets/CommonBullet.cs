using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBullet : MonoBehaviour, IBullet
{
    private float _flightSpeed = 0, _attackDistance = 0;
    private int _damage = 0;
    private Vector2 _startPoint, _targetPosition;
    private Monster _target;

    private Vector2 _lastVector = new Vector2(0, 0);
    private bool _isInited = false, _targetHitted = false;

    private int count = 0;

    private void FixedUpdate()
    {
        if (_isInited)
        {
            BulletFlight();
            Hit();
        }
    }


    public void Hit()
    {
        
        if (Vector2.Distance(_startPoint, _targetPosition) <= Vector2.Distance(_startPoint, transform.position) && !_targetHitted)
        {
            _targetHitted = true;
            if (_target != null)
                _target.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void BulletFlight()
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

        transform.position = new Vector2(transform.position.x, transform.position.y) + vector * _flightSpeed;

    }

    public void SetTarget(int damage, float flightSpeed, float attackDisrance, Vector3 startPosition, Monster target, bool isAOE = false, float attackRadius = 0)
    {
        _flightSpeed = flightSpeed;
        _damage = damage;
        transform.position = startPosition;
        _startPoint = startPosition;
        _target = target;
        _isInited = true;
    }
}

public interface IBullet
{
    void SetTarget(int damage, float flightSpeed, float attackDistance, Vector3 startPosition, Monster target, bool isAOE = false, float attackRadius = 0);
    void Hit();
}
