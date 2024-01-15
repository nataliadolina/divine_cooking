using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorPhysics
{
    public void OnStart();
    public void OnDispose();
    
    public void OnUpdate();

    public void MoveToAim(Vector3 aimPosition, float speed);

    public void Ricochet(Vector3 ricochetDirection);
}
