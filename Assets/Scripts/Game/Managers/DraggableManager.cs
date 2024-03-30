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

    [Inject]
    private MobileTouchManager _mobileTouchManager;

    private void Start()
    {
        string platform = _device.Platform;
        IDraggable draggable = null;
        DraggableMobile draggableMobile = null;
        switch (platform)
        {
            case "mobile":
                draggableMobile = gameObject.AddComponent<DraggableMobile>();
                draggableMobile.MobileTouchManager = _mobileTouchManager;
                draggable = draggableMobile;
                break;
            case "tablet":
                draggableMobile = gameObject.AddComponent<DraggableMobile>();
                draggableMobile.MobileTouchManager = _mobileTouchManager;
                draggable = draggableMobile;
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
