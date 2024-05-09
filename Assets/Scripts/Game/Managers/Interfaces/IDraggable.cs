using System;

public interface IDraggable
{
    public event Action onDrag;
    public DragType DragType { set; }
    public RectangleZone DragZone { set; }
    public RectangleZone DragTriggerZone { set; }
}
