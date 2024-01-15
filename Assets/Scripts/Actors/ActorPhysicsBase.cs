using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPhysicsBase : IActorPhysics
{
    public virtual void OnDispose() { }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }

    public virtual void MoveToAim(Vector3 aimPosition, float speed) { }
    public virtual void Ricochet(Vector3 ricochetDirection) { }
}
