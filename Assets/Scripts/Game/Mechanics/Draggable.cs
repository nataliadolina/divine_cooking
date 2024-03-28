using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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
    [SerializeField]
    private RectangleZone dragTriggerZone;

    [Inject]
    private MobileTouchManager _mobileTouchManager;

    private List<int> _touchIndexShouldMove = new List<int>();

    [Inject]
    private void Construct()
    {
        _mobileTouchManager.onMovedFinger += MoveToPosition;
        _mobileTouchManager.oneEndedFingerTouch += RemoveFingerId;
    }

    private void OnDestroy()
    {
        _mobileTouchManager.onMovedFinger -= MoveToPosition;
        _mobileTouchManager.oneEndedFingerTouch -= RemoveFingerId;
    }

    private void Drag(MoveTouchArgs moveTouchArgs)
    {
        Vector3 position = transform.position;
        Vector3 mousePosition = new Vector2(position.x, position.y) + moveTouchArgs.Offset;
        Vector3 newPosition = new Vector3();

        if (dragType.HasFlag(DragType.Vertical) && dragType.HasFlag(DragType.Horizontal))
        {
            newPosition = dragZone.ClampPosition(dragType, new Vector3(mousePosition.x, mousePosition.y, position.z));
        }
        if (dragType.HasFlag(DragType.Horizontal))
        {
            newPosition = dragZone.ClampPosition(dragType, new Vector3(mousePosition.x, position.y, position.z));
        }
        else if (dragType.HasFlag(DragType.Vertical))
        {
            newPosition = dragZone.ClampPosition(dragType, new Vector3(position.x, mousePosition.y, position.z));
        }

        transform.position = newPosition;
    }

    private void MoveToPosition(Dictionary<int, MoveTouchArgs> positions)
    {
        foreach (int id in positions.Keys)
        {
            MoveTouchArgs value = positions[id];

            if (_touchIndexShouldMove.Contains(value.TouchIndex))
            {
                Drag(value);
            }

            else if (dragTriggerZone.IsPositionInsideZone(value.StartPosition))
            {
                _touchIndexShouldMove.Add(value.TouchIndex);
                Drag(value);
            }
        }
    }

    private void RemoveFingerId(int fingerId)
    {
        if (_touchIndexShouldMove.Contains(fingerId))
        {
            _touchIndexShouldMove.Remove(fingerId);
        }
    }
}
