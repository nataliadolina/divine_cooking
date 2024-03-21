using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IActor food;
        if (collision.TryGetComponent(out food) && food.ActorType != ActorType.Bomb)
        {
            food.ChangePhysics(PhysicsType.Realistic);
        }
    }
}
