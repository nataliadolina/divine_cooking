using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActorRealisticPhysics : ActorPhysicsBase
{
    private IActor _actor;
    public override PhysicsType PhysicsType => PhysicsType.Realistic;

    public ActorRealisticPhysics(IActor actor)
    {
        _actor = actor;
    }

    public override void OnStart()
    {
        _actor.Rigidbody.isKinematic = false;
        _actor.Transform.Rotate(new Vector3(0, 0, Random.Range(-180, 180)));
        _actor.Rigidbody.constraints = RigidbodyConstraints2D.None;
        _actor.Collider.isTrigger = false;
    }

    public override void MoveToAim(Vector3 aimPosition, float speed)
    {
        Vector3 currentPosition = _actor.Transform.position;
        Vector3 direction = aimPosition - currentPosition;
        _actor.Rigidbody.AddForceAtPosition(direction.normalized * speed, currentPosition, ForceMode2D.Impulse);
    }

    public override void AddForce(Vector3 direction, float force)
    {
        _actor.Rigidbody.velocity = direction * force;
    }

    public class Factory : PlaceholderFactory<ActorRealisticPhysics>
    {

    }
}
