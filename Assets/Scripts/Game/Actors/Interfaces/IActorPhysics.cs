using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorPhysics
{
    public PhysicsType PhysicsType { get; }
    public void OnStart();
    public void OnDispose();
    
    public void OnUpdate();
    public void OnDestroy();
    public void MoveToAim(Vector3 aimPosition, float speed, UnityEngine.Object ricochet = null);
    public void AddForce(Vector3 direction, float force);
    public void Ricochet(Vector3 ricochetDirection, UnityEngine.Object ricochet = null);
}
