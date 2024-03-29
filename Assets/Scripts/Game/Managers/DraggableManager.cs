using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

public class DraggableManager : MonoBehaviour
{
    [SerializeField]
    private DragType dragType;
    [SerializeField]
    private RectangleZone dragZone;
    [SerializeField]
    private RectangleZone dragTriggerZone;

    [Inject]
    private Device _device;

    private void Start()
    {
        string platform = _device.Platform;
        IDraggable draggable = null;
        switch (platform)
        {
            case "mobile":
                draggable = gameObject.AddComponent<DraggableMobile>();
                break;
            case "desktop":
                draggable = gameObject.AddComponent<DraggablePC>();
                break;
            default:
                draggable = gameObject.AddComponent<DraggablePC>();
                break;
        }

        draggable.DragTriggerZone = dragTriggerZone;
        draggable.DragZone = dragZone;
        draggable.DragType = dragType;
    }
}
