using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IActor
{
    public Rigidbody2D Rigidbody { get; set; }
    public Transform Transform { get; }
    public ActorType ActorType { get; }
    public Collider2D Collider { get; }
    public Dictionary<PhysicsType, IActorPhysics> ActorPhysicsMap { get; }
}
