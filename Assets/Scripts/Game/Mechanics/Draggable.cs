using System;
using UnityEngine;
using UnityEngine.EventSystems;

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

        if (dragType.HasFlag(DragType.Vertical) && dragType.HasFlag(DragType.Horizontal))
        {
            transform.position = mousePosition + offset;
        }
        if (dragType.HasFlag(DragType.Horizontal))
        {
            transform.position = new Vector3(mousePosition.x, position.y, position.z) + new Vector3(offset.x, 0, 0);
        }
        else if (dragType.HasFlag(DragType.Vertical))
        {
            transform.position = new Vector3(position.x, mousePosition.y, position.z) + new Vector3(0, offset.y, 0);
        }
    }
}
