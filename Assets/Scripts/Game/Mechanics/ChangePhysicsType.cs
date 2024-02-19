using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePhysicsType : MonoBehaviour
{
    [SerializeField]
    private PhysicsType toType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IActor actor;
        if (collision.TryGetComponent<IActor>(out actor))
        {
            actor.ChangePhysics(toType);
        }
    }
}
