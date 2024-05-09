using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePhysicsTypeOnExitTrigger : MonoBehaviour
{
    [SerializeField]
    private PhysicsType toType;
    private void OnTriggerExit2D(Collider2D collision)
    {
        Actor actor;
        if (collision.TryGetComponent<Actor>(out actor))
        {
            actor.ChangePhysics(toType);
        }
    }
}
