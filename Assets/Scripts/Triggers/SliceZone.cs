using System.Collections;
using UnityEngine;
using DG.Tweening;

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

    private SoundManager _soundManager;
    private Tween _rotateTween;
    private void Start()
    {
        _rotateTween = gearWheel.DORotate(new Vector3(0, 0, gearWheel.rotation.eulerAngles.z + rotateAngleDegrees), rotateTime)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad)
            .Pause()
            .SetAutoKill(false)
            .OnPlay(OnStart)
            .OnComplete(OnComplete);
        _soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnStart()
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
