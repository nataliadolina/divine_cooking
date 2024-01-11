using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleZone : MonoBehaviour
{
    [SerializeField]
    private float xLimitOffset;
    [SerializeField]
    private float yLimitOffset;

    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;

    private void Start()
    {
        Vector3 position = transform.position;
        _minX = position.x - xLimitOffset;
        _maxX = position.x + xLimitOffset;
        _minY = position.y - yLimitOffset;
        _maxY = position.y + yLimitOffset;
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
