using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RectangleZone : MonoBehaviour
{
    public event Action onLimitsCalculated;
    [SerializeField]
    private float xLimitOffset;
    [SerializeField]
    private float yLimitOffset;

    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;

    public float MinX => _minX;
    public float MaxX => _maxX;
    public float MinY => _minY;
    public float MaxY => _maxY;

    private void Start()
    {
        Vector3 position = transform.position;
        _minX = position.x - xLimitOffset;
        _maxX = position.x + xLimitOffset;
        _minY = position.y - yLimitOffset;
        _maxY = position.y + yLimitOffset;

        onLimitsCalculated?.Invoke();
    }

    private Vector2 GetUpLeft()
    {
        Vector3 position = transform.position;
        float x = position.x - xLimitOffset;
        float y = position.y + yLimitOffset;
        return new Vector3(x, y);
    }

    private Vector2 GetDownLeft()
    {
        Vector3 position = transform.position;
        float x = position.x - xLimitOffset;
        float y = position.y - yLimitOffset;
        return new Vector3(x, y);
    }

    private Vector2 GetUpRight()
    {
        Vector3 position = transform.position;
        float x = position.x + xLimitOffset;
        float y = position.y + yLimitOffset;
        return new Vector3(x, y);
    }

    private Vector3 GetDownRight()
    {
        Vector3 position = transform.position;
        float x = position.x + xLimitOffset;
        float y = position.y - yLimitOffset;
        return new Vector3(x, y);
    }

    public bool IsPositionInsideZone(Vector2 position)
    {
        return position.x >= _minX && position.x <= _maxX && position.y >= _minY && position.y <= _maxY;
    }

    public Vector3 ClampPosition(DragType dragType, Vector3 position)
    {
        if (dragType.HasFlag(DragType.Horizontal) && dragType.HasFlag(DragType.Vertical))
        {
            return new Vector3(Mathf.Clamp(position.x, _minX, _maxX), Mathf.Clamp(position.y, _minY, _maxY), position.z);
        }
        else if (dragType.HasFlag(DragType.Vertical))
        {
            return new Vector3(position.x, Mathf.Clamp(position.y, _minY, _maxY), position.z);
        }
        else if (dragType.HasFlag(DragType.Horizontal))
        {
            return new Vector3(Mathf.Clamp(position.x, _minX, _maxX), position.y, position.z);
        }

        return position;
    }

#if UNITY_EDITOR

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(GetUpLeft(), GetDownLeft());
        Gizmos.DrawLine(GetUpRight(), GetDownRight());
        Gizmos.DrawLine(GetUpLeft(), GetUpRight());
        Gizmos.DrawLine(GetDownLeft(), GetDownRight());
    }

#endif
}
