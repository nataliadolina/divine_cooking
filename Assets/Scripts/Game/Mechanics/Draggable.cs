using System;
using UnityEngine;

[Flags]
public enum DragType
{
    Vertical = 1,
    Horizontal = 2
}

public class Draggable : MonoBehaviour
{
    [SerializeField]
    private DragType dragType;
    [SerializeField]
    private RectangleZone dragZone;

    private Vector3 offset;

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

        if (dragType.HasFlag(DragType.Vertical) && dragType.HasFlag(DragType.Horizontal))
        {
            newPosition = dragZone.ClampPosition(dragType, mousePosition + offset);
        }
        if (dragType.HasFlag(DragType.Horizontal))
        {
            newPosition = dragZone.ClampPosition(dragType, new Vector3(mousePosition.x, position.y, position.z) + new Vector3(offset.x, 0, 0));
        }
        else if (dragType.HasFlag(DragType.Vertical))
        {
            newPosition = dragZone.ClampPosition(dragType, new Vector3(position.x, mousePosition.y, position.z) + new Vector3(0, offset.y, 0));
        }

        if (dragZone.IsPositionInsideZone(newPosition))
        {
            transform.position = newPosition;
        }
    }
}
