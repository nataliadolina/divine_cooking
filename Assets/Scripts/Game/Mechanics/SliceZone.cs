using System.Collections;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class SliceZone : MonoBehaviour
{
    [SerializeField]
    private Transform gearWheel;
    [SerializeField]
    private Rigidbody2D _chainRigidbody;
    [SerializeField]
    private Collider2D _bladeCollider;
    [SerializeField]
    private RectangleZone _rectZone;
    [SerializeField]
    private float rotateAngleDegrees;
    [SerializeField]
    private float rotateTime;

    [Inject]
    private SoundManager _soundManager;
    private Tween _rotateTween;
    private void Start()
    {
        _bladeCollider.enabled = false;
        _rotateTween = gearWheel.DORotate(new Vector3(0, 0, gearWheel.rotation.eulerAngles.z + rotateAngleDegrees), rotateTime)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad)
            .Pause()
            .SetAutoKill(false)
            .OnPlay(OnPlay)
            .OnComplete(OnComplete);
    }

    private void OnPlay()
    {
        _bladeCollider.enabled = true;
    }

    private void OnComplete()
    {
        _bladeCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_rectZone.IsPositionInsideZone(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                _soundManager.PlaySwordSound();
                _rotateTween.Rewind();
                _rotateTween.Play();
            }
        }
    }
}
