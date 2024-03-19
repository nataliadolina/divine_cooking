using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorPhysicsBase : IActorPhysics
{
    protected IActor _actor;
    public ActorPhysicsBase(IActor actor)
    {
        _actor = actor;
    }
    public abstract PhysicsType PhysicsType { get; }
    public virtual void OnDispose() { }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }
    public virtual void OnDestroy() { }

    public virtual void MoveToAim(Vector3 aimPosition, float speed, UnityEngine.Object ricochet=null) { }
    public virtual void Ricochet(Vector3 ricochetDirection, UnityEngine.Object ricochet = null) { }

    public virtual void AddForce(Vector3 direction, float force) { }
}
