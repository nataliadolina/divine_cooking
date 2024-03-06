using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActorFreezeRotationPhysics : ActorPhysicsBase
{
    public override PhysicsType PhysicsType => PhysicsType.FreezeRotation;

    public ActorFreezeRotationPhysics(IActor actor) : base(actor)
    {

    }

    public override void OnStart()
    {
        if (_actor.ActorType != ActorType.Bomb)
        {
            _actor.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public override void MoveToAim(Vector3 aimPosition, float speed, UnityEngine.Object ricochet = null)
    {
        if (ricochet != null && _actor.Ricochets.Contains(ricochet))
        {
            return;
        }

        Vector3 currentPosition = _actor.Transform.position;
        Vector3 direction = aimPosition - currentPosition;
        _actor.Rigidbody.AddForceAtPosition(direction.normalized * speed, currentPosition, ForceMode2D.Impulse);
    }

    public override void AddForce(Vector3 direction, float force)
    {
        _actor.Rigidbody.velocity = direction * force;
    }

    public class Factory : PlaceholderFactory<ActorFreezeRotationPhysics>
    {

    }
}
