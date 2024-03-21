using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActorTransparentPhysics: ActorPhysicsBase
{
    public override PhysicsType PhysicsType { get => PhysicsType.Transperant; }

    public ActorTransparentPhysics(IActor actor) : base(actor)
    {

    }

    public override void OnStart()
    {
        _actor.Rigidbody.isKinematic = true;
        _actor.Collider.isTrigger = true;
    }

    public override void OnDispose()
    {
        _actor.Rigidbody.isKinematic = false;
        _actor.Collider.isTrigger = false;
    }

    public class Factory : PlaceholderFactory<ActorTransparentPhysics>
    {

    }
}
