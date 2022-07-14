using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{
    public static CameraMove instance;
    [SerializeField]
    private float _speed;

    private float _motionLimiterX = 0, _motionLimiterY = 0;
    private Vector3 _startPos;
    private Vector3 _startScrollPoint;
    private Vector3 _direction;
    private Camera _camera;
    private Vector2 _mapCenter;
    public Vector3 CameraStartPosition
    {
        set
        {
            _startPos = value;
            transform.position = new Vector3(_startPos.x, _startPos.y, transform.position.z);
            //if (_motion_limiter == 0)
            //    _motion_limiter = (int)(_startPos.x * 2);
        }
    }

    public Vector2 MapSize
    {
        set
        {
            _motionLimiterX = value.x/2;
            _motionLimiterY = value.y/2;

            _mapCenter = new Vector2(_motionLimiterX, _motionLimiterY);
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        _camera = GetComponent<Camera>();
        _startScrollPoint = _startPos;
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startScrollPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                float height = _camera.orthographicSize;
                float width = _camera.aspect * height;

                Vector3 nextScrollPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
                _direction = new Vector3(nextScrollPoint.x - _startScrollPoint.x, nextScrollPoint.y - _startScrollPoint.y, 0).normalized;

                Debug.Log(_startPos.x + (_direction * _speed).x);
                Debug.Log(_startPos.x);
                if (
                        (_startPos.x + width + (_direction * _speed).x >= _mapCenter.x + _motionLimiterX - 0.5f &&
                        _startPos.x + (_direction * _speed).x > _startPos.x) ||
                        (_startPos.x - width + (_direction * _speed).x <= _mapCenter.x - _motionLimiterX - 0.5f &&
                        _startPos.x + (_direction * _speed).x < _startPos.x))
                    _direction.x = 0;

                if (_startPos.y + height + (_direction * _speed).y >= _mapCenter.y + _motionLimiterY - 0.5f ||
                        _startPos.y - height + (_direction * _speed).y <= _mapCenter.y - _motionLimiterY - 0.5f)
                    _direction.y = 0;

                CameraStartPosition = _startPos + _direction * _speed;
                _startScrollPoint = nextScrollPoint;
            }
        }
    }

}
