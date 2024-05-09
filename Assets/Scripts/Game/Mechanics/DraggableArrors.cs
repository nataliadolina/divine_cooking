using UnityEngine;

public class DraggableArrors : MonoBehaviour
{
    [SerializeField]
    private Draggable draggable;

    [SerializeField]
    private GameObject minArror;
    [SerializeField]
    private GameObject maxArror;

    private RectangleZone _dragZone;

    private float _minValue;
    private float _maxValue;

    private void Awake()
    {
        draggable.onDrag += OnDrag;
        _dragZone = draggable.DragZone;
        _dragZone.onLimitsCalculated += OnLimitsCalculated;
    }

    private void OnLimitsCalculated()
    {
        _minValue = draggable.DragType == DragType.Vertical ? _dragZone.MinY : _dragZone.MinX;
        _maxValue = draggable.DragType == DragType.Vertical ? _dragZone.MaxY : _dragZone.MaxX;
        OnDrag();
    }
    
    private void OnDestroy()
    {
        draggable.onDrag -= OnDrag;
    }

    private void OnDrag()
    {
        float transformPosAxisValue = draggable.DragType == DragType.Vertical ? transform.position.y : transform.position.x;

        minArror.SetActive(transformPosAxisValue > _minValue + 0.1f);
        maxArror.SetActive(transformPosAxisValue < _maxValue - 0.1f);
    }
}
