using UnityEngine;

public class CameraZooming : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("�������� ��������������� ������ ������")]
    public float _speed = 1;
    [Tooltip("��������� ������ ��������������� ������")]
    public float _radius = 5;
    [Tooltip("����� �� ������� ��������� ��������� ������ ����������� ������ (���� ��� �� ������ � ����������, �� �� ��������� �� ������������ ��� ���������, �� ������� ��������� ��������)")]
    public Transform _target;

    private Touch _touchStart;
    private Touch _touchEnd;
    private Vector3 _targetPos;

    private void Start()
    {
        if (_target == null)
        {
            _target = transform;
        }

        _targetPos = _target.position;
    }

    void Update()
    {
        if (Input.touchCount == 2 && BuildingsGrid.maymove)
        {
            _touchStart = Input.GetTouch(0);
            _touchEnd = Input.GetTouch(1);

            Vector2 touchStartDeltaPos = _touchStart.position - _touchStart.deltaPosition;
            Vector2 touchEndDeltaPos = _touchEnd.position - _touchEnd.deltaPosition;

            float distDeltaTouches = (touchStartDeltaPos - touchEndDeltaPos).magnitude;
            float currentDistTouchesPos = (_touchStart.position - _touchEnd.position).magnitude;

            float distance = distDeltaTouches - currentDistTouchesPos;

            


            Zooming(distance);
        }
    }

    private void Zooming(float value)
    {
        float height = (value * _speed * Time.deltaTime);
        float delta = Mathf.Abs(value);

        if (Camera.main.orthographicSize > -value && Camera.main.orthographicSize > 3)
            Camera.main.orthographicSize += value ;
    }
}