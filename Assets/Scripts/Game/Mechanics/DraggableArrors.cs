using UnityEngine;

public class DraggableArrors : MonoBehaviour
{
    [SerializeField]
    private DraggableManager draggableManager;

    [SerializeField]
    private GameObject minArror;
    [SerializeField]
    private GameObject maxArror;

    private RectangleZone _dragZone;

    private float _minValue;
    private float _maxValue;
    
    private IDraggable _draggable;
    
    private void Awake()
    {
        _dragZone = draggableManager.DragZone;
        _dragZone.onLimitsCalculated += OnLimitsCalculated;
        draggableManager.onDraggableSet += OnDraggableSet;
    }

    private void OnDraggableSet(IDraggable draggable)
    {
        _draggable = draggable;
        _draggable.onDrag += OnDrag;
    }
    
    private void OnLimitsCalculated()
    {
        _minValue = draggableManager.DragType == DragType.Vertical ? _dragZone.MinY : _dragZone.MinX;
        _maxValue = draggableManager.DragType == DragType.Vertical ? _dragZone.MaxY : _dragZone.MaxX;
        OnDrag();
    }
    
    private void OnDestroy()
    {
        if (_draggable == null)
        {
            return;
        }
        
        _draggable.onDrag -= OnDrag;
    }

    private void OnDrag()
    {
        float transformPosAxisValue = draggableManager.DragType == DragType.Vertical ? transform.position.y : transform.position.x;

        minArror.SetActive(transformPosAxisValue > _minValue + 0.1f);
        maxArror.SetActive(transformPosAxisValue < _maxValue - 0.1f);
    }
}
