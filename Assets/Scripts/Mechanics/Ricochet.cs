using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    [SerializeField]
    private float ricochetSpeed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IActor actor;
        if (collision.collider.TryGetComponent(out actor))
        {
            Vector3 contactPoint = collision.GetContact(0).point;
            Vector3 direction = new Vector3(contactPoint.x - transform.position.x, contactPoint.y - transform.position.y, 0).normalized;
            actor.ActorPhysicsMap[PhysicsType.Springy].Ricochet(direction);
            actor.Rigidbody.isKinematic = true;
            actor.Collider.isTrigger = true;
        }
    }
}
