using System;
using UnityEngine;

public class ArcherMouseInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _inputForceSensivity = 1;

    public event Action InputStart;
    public event Action InputAngle;
    public event Action InputEnd;

    public float Angle { get; private set; }
    public float Speed { get; private set; }

    private Vector2 _originPosition;

    private void Start()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
            OnInputStart();

        if (Input.GetMouseButton(0) == true)
            OnInput();

        if (Input.GetMouseButtonUp(0) == true)
            OnInputEnd();
    }

    private void OnInputStart()
    {
        _originPosition = Input.mousePosition;
        _lineRenderer.SetPosition(0, (Vector2)_camera.ScreenToWorldPoint(_originPosition));
        _lineRenderer.enabled = true;
        InputStart?.Invoke();
    }

    private void OnInput()
    {
        Vector2 direction = (Vector2)Input.mousePosition - _originPosition;
        Angle = -Vector2.SignedAngle(direction, Vector2.left);

        Vector3 originViewport = GetViewportPosition(_originPosition);
        Vector3 mouseViewPort = GetViewportPosition(Input.mousePosition);
        Speed = Vector2.Distance(originViewport, mouseViewPort) * _inputForceSensivity;

        Vector2 pointPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _lineRenderer.SetPosition(1, pointPosition);
        InputAngle?.Invoke();
    }

    private void OnInputEnd()
    {
        Angle = 0;
        Speed = 0;

        _lineRenderer.enabled = false;
        InputEnd?.Invoke();
    }

    private Vector2 GetViewportPosition(Vector2 screenPosition)
    {
        return _camera.ScreenToViewportPoint(screenPosition);
    }
}