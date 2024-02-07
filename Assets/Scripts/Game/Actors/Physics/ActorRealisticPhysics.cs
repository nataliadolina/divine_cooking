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

    public class Factory : PlaceholderFactory<ActorRealisticPhysics>
    {

    }
}
