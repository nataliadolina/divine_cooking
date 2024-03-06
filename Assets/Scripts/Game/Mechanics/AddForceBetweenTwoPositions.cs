using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceBetweenTwoPositions : MonoBehaviour
{
    [SerializeField]
    private PhysicsType targetType;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform aimTransform;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IActor actor;
        if (collision.TryGetComponent(out actor))
        {
            if (targetType == 0)
            {
                foreach (var physics in actor.CurrentActorPhysics)
                {
                    physics.MoveToAim(aimTransform.position, speed, this);
                }
            }

            else
            {
                actor.ActorPhysicsMap[targetType].MoveToAim(aimTransform.position, speed, this);
            }

            actor.Ricochets.Add(this);
        }
    }
}
