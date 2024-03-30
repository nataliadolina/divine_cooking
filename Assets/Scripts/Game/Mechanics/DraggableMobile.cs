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

public class DraggableMobile : MonoBehaviour, IDraggable
{
    private RectangleZone _dragTriggerZone;
    private DragType _dragType;
    private RectangleZone _dragZone;

    private MobileTouchManager _mobileTouchManager;

    private List<int> _touchIndexShouldMove = new List<int>();

    public DragType DragType { set => _dragType = value; }
    public RectangleZone DragZone { set => _dragZone = value; }
    public RectangleZone DragTriggerZone { set => _dragTriggerZone = value; }
    public MobileTouchManager MobileTouchManager { set => _mobileTouchManager = value; }

    private void Start()
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

        if (_dragType.HasFlag(DragType.Vertical) && _dragType.HasFlag(DragType.Horizontal))
        {
            newPosition = _dragZone.ClampPosition(_dragType, new Vector3(mousePosition.x, mousePosition.y, position.z));
        }
        if (_dragType.HasFlag(DragType.Horizontal))
        {
            newPosition = _dragZone.ClampPosition(_dragType, new Vector3(mousePosition.x, position.y, position.z));
        }
        else if (_dragType.HasFlag(DragType.Vertical))
        {
            newPosition = _dragZone.ClampPosition(_dragType, new Vector3(position.x, mousePosition.y, position.z));
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

            else if (_dragTriggerZone.IsPositionInsideZone(value.StartPosition))
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
