using System.Collections;
using UnityEngine;
using Zenject;


public class TouchController : MonoBehaviour
{
    [SerializeField]
    private RectangleZone rectangleZone;
    [SerializeField]
    private Transform aimTransform;

    private float? _lastMousePosition;
    private bool _shouldMove;

    [HideInInspector]
    public bool IsLastMouseOperationDown;

    private void Update()
    {
        if (!rectangleZone.IsPositionInsideZone(Input.mousePosition))
        {
            return;
        }

        if (!_shouldMove && Input.GetMouseButtonDown(0))
        {
            IsLastMouseOperationDown = true;
            _shouldMove = true;
            return;
        }

        if (!_shouldMove)
        {
            return;
        }

        if (_shouldMove && IsLastMouseOperationDown)
        {
            Move();
        }

        if (Input.GetMouseButtonUp(0) && _shouldMove)
        {
            IsLastMouseOperationDown = false;
            _shouldMove = false;
            _lastMousePosition = null;
        }
    }


    private void Move()
    {
        Debug.Log("Move");
        float mousePosition = Input.mousePosition.y;
        if (_lastMousePosition == null)
        {
            _lastMousePosition = mousePosition;
            return;
        }

        float delta = (float)_lastMousePosition - mousePosition;
        aimTransform.position += Vector3.up * delta * Time.deltaTime;
        _lastMousePosition = mousePosition;
    }
}
