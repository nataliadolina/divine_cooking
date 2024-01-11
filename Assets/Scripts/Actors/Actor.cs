using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public abstract class Actor : MonoBehaviour, IActor
{
    [SerializeField]
    protected ActorType actorType;

    protected Rigidbody2D _rigidbody;
    protected Transform _transform;

    public ActorType ActorType { get => actorType; }
    public Rigidbody2D Rigidbody { get => _rigidbody; set => _rigidbody = value; }
    public Transform Transform => _transform;

    [Inject]
    private void Construct(ActorInstaller.Settings settings)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = transform;
        switch (settings.PhysicsType)
        {
            case PhysicsType.Springy:
                gameObject.AddComponent<ActorSpringyPhysics>();
                break;
        }
    }
}
