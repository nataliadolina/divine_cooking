using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorPhysicsBase : IActorPhysics
{
    public abstract PhysicsType PhysicsType { get; }
    public virtual void OnDispose() { }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }
    public virtual void OnDestroy() { }

    public virtual void MoveToAim(Vector3 aimPosition, float speed) { }
    public virtual void Ricochet(Vector3 ricochetDirection) { }
}
