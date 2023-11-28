using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blade : MonoBehaviour
{
    [SerializeField]
    private SliceDirection sliceDirection;
    [SerializeField]
    private float rotateAngleDegrees;

    private Tween _sliceAnimation;
    private Collider2D _collider;
    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
        _sliceAnimation = transform.DORotate(new Vector3(0, 0, transform.rotation.eulerAngles.z + rotateAngleDegrees), 0.5f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad)
            .Pause()
            .SetAutoKill(false)
            .OnPlay(OnStart)
            .OnComplete(OnComplete);
    }

    private void OnStart()
    {
        _collider.enabled = true;
    }

    private void OnComplete()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food food;
        if (collision.TryGetComponent<Food>(out food))
        {
            food.Slice(sliceDirection, this);
        }
    }

    public void Cut()
    {
        _sliceAnimation.Rewind();
        _sliceAnimation.Play();
    }
}
