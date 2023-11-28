using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class Food : MonoBehaviour
{
    [SerializeField]
    private bool adjustScale;

    private List<Blade> _blades = new List<Blade>();
    private Slicer _slicer;
    private Collider2D _collider;
    public List<Blade> Blades { set => _blades = value; }
    public bool AdjustScale { set => adjustScale = value; }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _slicer = new Slicer(transform.rotation, gameObject, adjustScale);
    }

    public void Rotate(float rotateDegrees)
    {
        if (transform == null)
        {
            return;
        }

        transform.DORotate(new Vector3(0, 0, rotateDegrees), 0.5f);
    }

    public void Slice(SliceDirection direction, Blade blade)
    {
        if (_blades.Contains(blade))
        {
            return;
        }

        _blades.Add(blade);
        _slicer.Slice(direction, _blades);
        _collider.enabled = false;

        StartCoroutine(WaitToDestroyGameObject());
    }

    private IEnumerator WaitToDestroyGameObject()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
