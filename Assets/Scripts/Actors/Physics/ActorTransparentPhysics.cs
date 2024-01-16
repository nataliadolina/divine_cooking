using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActorTransparentPhysics: ActorPhysicsBase
{
    private IActor _actor;
    public ActorTransparentPhysics(IActor actor)
    {
        _actor = actor;
    }

    public override void OnStart()
    {
        _actor.Rigidbody.isKinematic = true;
        _actor.Collider.isTrigger = true;
    }

    public class Factory : PlaceholderFactory<ActorTransparentPhysics>
    {

    }
}
