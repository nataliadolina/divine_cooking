using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public enum PhysicsType
{
    None = 1,
    Springy = 2
}

public abstract class Actor : MonoBehaviour, IActor
{
    [SerializeField]
    protected ActorType actorType;

    protected Rigidbody2D _rigidbody;
    protected Transform _transform;
    protected Collider2D _collider;

    protected List<IActorPhysics> _actorPhysics = new List<IActorPhysics>();
    private bool _firstEnable = true;

    [Inject]
    private ActorPhysicsFactory _actorPhysicsFactory;

    public ActorType ActorType { get => actorType; }
    public Rigidbody2D Rigidbody { get => _rigidbody; set => _rigidbody = value; }
    public Transform Transform => transform;

    public Dictionary<PhysicsType, IActorPhysics> ActorPhysicsMap { get; private set; }

    public Collider2D Collider => _collider;

    [Inject]
    private void Construct(Settings settings)
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        ActorPhysicsMap = new Dictionary<PhysicsType, IActorPhysics>();

        foreach (var actorPhysics in (PhysicsType[])Enum.GetValues(typeof(PhysicsType)))
        {
            if (settings.PhysicsType.HasFlag(actorPhysics))
            {
                IActorPhysics state = _actorPhysicsFactory.CreatePhysics(actorPhysics);
                _actorPhysics.Add(state);
                ActorPhysicsMap.Add(actorPhysics, state);
            }
        }
    }

    private void Update()
    {
        foreach (var physics in _actorPhysics)
        {
            physics.OnUpdate();
        }
    }

    [Serializable]
    public struct Settings
    {
        public PhysicsType PhysicsType;
    }
}
