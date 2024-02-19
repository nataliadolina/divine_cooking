using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using System.Linq;

[Flags]
public enum PhysicsType
{
    Springy = 1,
    Transperant = 2,
    Realistic = 4,
    FreezeRotation = 8,
}

public abstract class Actor : MonoBehaviour, IActor
{
    [SerializeField]
    protected ActorType actorType;

    protected Settings _settings;

    protected Rigidbody2D _rigidbody;
    protected Transform _transform;
    protected Collider2D _collider;
    protected int _rootInstanceId;
    protected Vector3 _direction;
    protected float _speed;

    protected bool _isSlice = false;

    protected HashSet<IActorPhysics> _currentActorPhysics = new HashSet<IActorPhysics>();

    private PhysicsType[] _physicsTypes;

    [Inject]
    private ActorPhysicsFactory _actorPhysicsFactory;

    protected float _partOfOne = 1;

    public ActorType ActorType { get => actorType; }
    public Rigidbody2D Rigidbody { get => _rigidbody; set => _rigidbody = value; }
    public Transform Transform => transform;
    public float Speed { get => _speed; set => _speed = value; }
    public Vector3 Direction { get => _direction; set => _direction = value; }
    public Dictionary<PhysicsType, IActorPhysics> ActorPhysicsMap { get; private set; }

    public int RootInstanceId { get => _rootInstanceId; }
    public HashSet<IActorPhysics> CurrentActorPhysics { get => _currentActorPhysics; }

    public Collider2D Collider => _collider;

    public float PartOfOne => _partOfOne;
    private float _scale = 1;

    [Inject]
    private void Construct(Settings settings, LevelSettingsInstaller.Settings levelSettings)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        ActorPhysicsMap = new Dictionary<PhysicsType, IActorPhysics>();
        _settings = settings;
        _physicsTypes = (PhysicsType[])Enum.GetValues(typeof(PhysicsType));
        _scale = levelSettings.ActorScale;
        
    }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();

        if (!_isSlice)
        {
            transform.localScale = new Vector3(_scale, _scale, 1);
        }

        PhysicsType targetInitialPhysicsType = !_isSlice ? _settings.InitialPhysicsType : _settings.InitialPhysicsTypeAfterCut;
        PhysicsType targetPhysicsType = !_isSlice ? _settings.PhysicsType : _settings.PhysicsTypeAfterCut;
        foreach (var actorPhysics in targetPhysicsType.EnumToArray(_physicsTypes))
        {
            IActorPhysics state = _actorPhysicsFactory.CreatePhysics(actorPhysics);
            ActorPhysicsMap.Add(actorPhysics, state);
            if (targetInitialPhysicsType.HasFlag(actorPhysics))
            {
                _currentActorPhysics.Add(state);
                state.OnStart();
            }
        }

        StartInternal();
    }

    protected virtual void StartInternal() { }

    private void Update()
    {
        foreach (var physics in _currentActorPhysics)
        {
            physics.OnUpdate();
        }
    }

    protected void Dispose()
    {
        foreach (var physics in _currentActorPhysics)
        {
            physics.OnDispose();
        }
    }

    private void OnDestroy()
    {
        foreach (var physics in _currentActorPhysics)
        {
            physics.OnDestroy();
        }
    }

    public void ChangePhysics(PhysicsType physicsType)
    {
        IActorPhysics[] physicsToDelete = _currentActorPhysics.Where(x => !physicsType.HasFlag(x.PhysicsType)).ToArray();
        foreach (var physics in physicsToDelete)
        {
            physics.OnDispose();
            _currentActorPhysics.Remove(physics);
        }

        foreach (var actorPhysics in physicsType.EnumToArray(_physicsTypes))
        {
            if (_currentActorPhysics.Where(x => x.PhysicsType == actorPhysics).ToArray().Count() == 0)
            {
                if (!ActorPhysicsMap.ContainsKey(actorPhysics))
                {
                    ActorPhysicsMap.Add(actorPhysics, _actorPhysicsFactory.CreatePhysics(actorPhysics));
                }

                IActorPhysics physics = ActorPhysicsMap[actorPhysics];
                physics.OnStart();
                _currentActorPhysics.Add(physics);
            }
        } 
    }

    [Serializable]
    public struct Settings
    {
        public PhysicsType PhysicsType;
        public PhysicsType PhysicsTypeAfterCut;
        public PhysicsType InitialPhysicsType;
        public PhysicsType InitialPhysicsTypeAfterCut;
    }
}
