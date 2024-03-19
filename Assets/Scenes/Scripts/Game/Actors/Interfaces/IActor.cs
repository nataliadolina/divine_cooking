using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IActor
{
    public ActorType ActorType { get; }
    public Rigidbody2D Rigidbody { get; set; }
    public Transform Transform { get; }
    public Collider2D Collider { get; }
    public List<UnityEngine.Object> Ricochets { get; }
    public Vector3 Direction { get; set; }
    public float Speed { get; set; }

    public Dictionary<PhysicsType, IActorPhysics> ActorPhysicsMap { get; }
    public HashSet<IActorPhysics> CurrentActorPhysics { get; }
    public void ChangePhysics(PhysicsType physics);
    public int RootInstanceId { get; }
    public float PartOfOne { get; }
}
