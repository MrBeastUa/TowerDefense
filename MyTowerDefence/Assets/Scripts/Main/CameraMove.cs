using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{
    public static CameraMove instance;
    [SerializeField]
    private float _speed;

    private int _motion_limiter = 0;
    private Vector3 _startPos;
    private Vector3 _startScrollPoint;
    private Vector3 _direction;
    private Camera _camera;

    public Vector3 CameraStartPosition
    {
        set
        {
            _startPos = value;
            transform.position = new Vector3(_startPos.x, _startPos.y, transform.position.z);
            if (_motion_limiter == 0)
                _motion_limiter = (int)(_startPos.x * 2);
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
                Vector3 nextScrollPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
                _direction = new Vector3(nextScrollPoint.x - _startScrollPoint.x, nextScrollPoint.y - _startScrollPoint.y, _startScrollPoint.z).normalized;

                if ((_startPos.x - (_camera.ScreenToWorldPoint(_camera.sensorSize).x * _camera.orthographicSize) / 2) + (_direction * _speed).x < 0 ||
                            (_startPos.x + (_camera.ScreenToWorldPoint(_camera.sensorSize).x * _camera.orthographicSize) / 2) + (_direction * _speed).x > _motion_limiter)
                    _direction.x = 0;

                if ((_startPos.y - (_camera.ScreenToWorldPoint(_camera.sensorSize).y * _camera.orthographicSize) / 2) + (_direction * _speed).y < 0 ||
                           (_startPos.y + (_camera.ScreenToWorldPoint(_camera.sensorSize).y * _camera.orthographicSize) / 2) + (_direction * _speed).y > _motion_limiter)
                    _direction.y = 0;

                CameraStartPosition = _startPos + _direction * _speed;
                _startScrollPoint = nextScrollPoint;
            }
        }
    }

}
