using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActorEmptyPhysics : ActorPhysicsBase
{
    public override PhysicsType PhysicsType => PhysicsType.Empty;

    private ActorEmptyPhysics(IActor actor) : base(actor) { }
    public class Factory : PlaceholderFactory<ActorEmptyPhysics>
    {

    }
}
