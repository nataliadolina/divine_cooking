using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{
    private Slicer _slicer;

    private void Start()
    {
        _slicer = new Slicer(transform.localScale, transform.rotation, gameObject);
    }

    public void Rotate(float rotateDegrees)
    {
        transform.DORotate(new Vector3(0, 0, rotateDegrees), 0.5f);
    }

    public void Slice(SliceDirection direction)
    {
        _slicer.Slice(direction);
    }
}
