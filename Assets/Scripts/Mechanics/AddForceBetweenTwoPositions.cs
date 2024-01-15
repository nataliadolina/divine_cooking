using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceBetweenTwoPositions : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform aimTransform;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IActor actor;
        if (collision.TryGetComponent(out actor))
        {
            actor.ActorPhysicsMap[PhysicsType.Springy].MoveToAim(aimTransform.position, speed);
        }
    }
}
