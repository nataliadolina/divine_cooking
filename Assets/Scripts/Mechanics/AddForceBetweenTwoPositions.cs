using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceBetweenTwoPositions : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform aimTransform;

    private Vector2 _aimPosition;

    private void Start()
    {
        _aimPosition = aimTransform.position;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        ISpringy actor;
        if (collision.TryGetComponent(out actor))
        {
            actor.MoveToAim(_aimPosition, speed);
        }
    }
}
