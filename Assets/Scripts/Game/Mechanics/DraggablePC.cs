using System;
using UnityEngine;

public class DraggablePC : MonoBehaviour, IDraggable
{
    public event Action onDrag;
    
    private DragType _dragType;
    private RectangleZone _dragZone;
    private RectangleZone _dragTriggerZone;

    private Vector3 offset;

    public DragType DragType { set => _dragType = value; }
    public RectangleZone DragZone { set => _dragZone = value; }
    public RectangleZone DragTriggerZone { set => _dragTriggerZone = value; }

    void OnMouseDown()
    {
        offset = transform.position -
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        Vector3 position = transform.position;
        Vector3 newPosition = new Vector3();

        if (_dragType.HasFlag(DragType.Vertical) && _dragType.HasFlag(DragType.Horizontal))
        {
            newPosition = _dragZone.ClampPosition(_dragType, mousePosition + offset);
        }
        if (_dragType.HasFlag(DragType.Horizontal))
        {
            newPosition = _dragZone.ClampPosition(_dragType, new Vector3(mousePosition.x, position.y, position.z) + new Vector3(offset.x, 0, 0));
        }
        else if (_dragType.HasFlag(DragType.Vertical))
        {
            newPosition = _dragZone.ClampPosition(_dragType, new Vector3(position.x, mousePosition.y, position.z) + new Vector3(0, offset.y, 0));
        }

        if (_dragZone.IsPositionInsideZone(newPosition))
        {
            transform.position = newPosition;
        }
        
        onDrag?.Invoke();
    }
}
