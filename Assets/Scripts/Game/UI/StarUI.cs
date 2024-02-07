using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;
using UnityEngine.UI;
using System;

public struct StarUIArgs
{
    public Vector2 Size;
    public Quaternion Rotation;
    public Vector3 StartPosition;
    public Vector3 EndPosition;
    public Action Callback;

    public StarUIArgs(Vector2 size, Quaternion rotation, Vector3 startPosition, Vector3 endPosition, Action callback)
    {
        Size = size;
        Rotation = rotation;
        StartPosition = startPosition;
        EndPosition = endPosition;
        Callback = callback;
    }
}
public class StarUI : MonoBehaviour
{
    private Pool _pool;
    private RectTransform _rectTransform;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Action _callback;

    [Inject]
    private void Construct(Pool pool)
    {
        _pool = pool;
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Animate(float time) 
    {
        _rectTransform.position = _startPosition;
        transform.DOMove(_endPosition, time).OnKill(Despawn).SetAutoKill(true);
    }

    public void Init(StarUIArgs args)
    {
        Vector2 size = args.Size;
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        _rectTransform.rotation = args.Rotation;
        _startPosition = args.StartPosition;
        _endPosition = args.EndPosition;
        _callback = args.Callback;
    }

    private void Despawn()
    {
        _pool.Despawn(this);
        _callback.Invoke();
    }

    public class Pool : MemoryPool<StarUI>
    {
        protected override void OnCreated(StarUI item)
        {
            item.gameObject.SetActive(false);
        }
        protected override void OnSpawned(StarUI item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(StarUI item)
        {
            item.gameObject.SetActive(false);
        }
    }
}
