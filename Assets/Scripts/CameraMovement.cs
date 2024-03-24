using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Скорость передвижения камеры")]
    public float _speed = 1;
    [Tooltip("Доступный радиус перемещения камеры")]
    public float _radius = 10;
    [Tooltip("Точка от которой считается доступный радиус перемещения камеры (если она не задана в инспекторе, то по умолчанию он определяется как компонент, на котором находится сценарий)")]
    public Transform _target;

    private Touch _touch;
    private Vector3 _targetPos;

    private void Start()
    {
        if (_target == null)
        {
            _target = this.transform;
        }

        _targetPos = _target.position;
    }

    private void Update()
    {
        if (Input.touchCount == 1 && BuildingsGrid.maymove)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                Vector3 movePos = new Vector3(
                    transform.position.x + _touch.deltaPosition.x * _speed * -1 * Time.deltaTime,
                    transform.position.y + _touch.deltaPosition.y * _speed * -1 * Time.deltaTime,
                    transform.position.z - _touch.deltaPosition.x * _speed * -1 * Time.deltaTime);

                Vector3 distance = movePos - _targetPos;

                if (distance.magnitude < _radius)
                    transform.position = movePos;

                if (BuildingsGrid.CanvasFather.childCount != 0)
                {
                    Destroy(BuildingsGrid.CanvasFather.GetChild(0).gameObject);
                }
            }
        }
    }
}
