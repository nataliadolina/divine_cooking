using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggable 
{
    public DragType DragType { set; }
    public RectangleZone DragZone { set; }
    public RectangleZone DragTriggerZone { set; }
}
