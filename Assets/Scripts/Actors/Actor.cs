using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

[Flags]
public enum PhysicsType
{
    Springy = 1,
    Transperant = 2
}

public abstract class Actor : MonoBehaviour, IActor
{
    [SerializeField]
    protected ActorType actorType;

    private PhysicsType _targetPhysicsType;
    private Settings _settings;

    protected Rigidbody2D _rigidbody;
    protected Transform _transform;
    protected Collider2D _collider;
    protected int _rootInstanceId;

    protected bool _isSlice = false;

    protected List<IActorPhysics> _actorPhysics = new List<IActorPhysics>();

    [Inject]
    private ActorPhysicsFactory _actorPhysicsFactory;

    public ActorType ActorType { get => actorType; }
    public Rigidbody2D Rigidbody { get => _rigidbody; set => _rigidbody = value; }
    public Transform Transform => transform;

    public Dictionary<PhysicsType, IActorPhysics> ActorPhysicsMap { get; private set; }
    
    public int RootInstanceId { get => _rootInstanceId; }

    public Collider2D Collider => _collider;

    [Inject]
    private void Construct(Settings settings)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        ActorPhysicsMap = new Dictionary<PhysicsType, IActorPhysics>();
        _settings = settings;
    }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();

        _targetPhysicsType = !_isSlice ? _settings.PhysicsType : _settings.PhysicsTypeAfterCut;
        foreach (var actorPhysics in (PhysicsType[])Enum.GetValues(typeof(PhysicsType)))
        {
            if (_targetPhysicsType.HasFlag(actorPhysics))
            {
                IActorPhysics state = _actorPhysicsFactory.CreatePhysics(actorPhysics);
                state.OnStart();
                _actorPhysics.Add(state);
                ActorPhysicsMap.Add(actorPhysics, state);
            }
        }

        StartInternal();
    }

    protected virtual void StartInternal() { }

    private void Update()
    {
        foreach (var physics in _actorPhysics)
        {
            physics.OnUpdate();
        }
    }

    protected void Dispose()
    {
        foreach (var physics in _actorPhysics)
        {
            physics.OnDispose();
        }
    }

    private void OnDestroy()
    {
        foreach (var physics in _actorPhysics)
        {
            physics.OnDestroy();
        }
    }

    [Serializable]
    public struct Settings
    {
        public PhysicsType PhysicsType;
        public PhysicsType PhysicsTypeAfterCut;
    }
}
