using UnityEngine;
using Zenject;

public class ActorSpringyPhysics : ActorPhysicsBase
{
    private float _speed = 0;
    private Vector3 _direction = Vector3.zero;
    private IActor _actor;
    private Transform _transform;

    public override PhysicsType PhysicsType { get => PhysicsType.Springy; }

    private ActorSpringyPhysics(IActor actor)
    {
        _actor = actor;
        _transform = _actor.Transform;
    }

    public override void OnStart()
    {
        _actor.Rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        _direction = _actor.Direction;
        _speed = _actor.Speed;
    }

    public override void Ricochet(Vector3 ricochetDirection)
    {
        _transform.RotateAround(_transform.position, Vector3.forward, 360 - _transform.eulerAngles.z * 2);
        _direction += ricochetDirection;
        _direction = _direction.normalized;
        _actor.Direction = _direction;
        _actor.Transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg);
    }

    public override void MoveToAim(Vector3 aimPosition, float speed)
    {
        _speed = speed;
        _direction = new Vector3(aimPosition.x - _transform.position.x, aimPosition.y - _transform.position.y, 0).normalized;
        _actor.Speed = speed;
        _actor.Direction = _direction;
        _actor.Transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg);
    }

    public override void OnUpdate()
    {
        _transform.position += _direction * Time.deltaTime * _speed;
    }

    public override void OnDispose()
    {
        _actor.Speed = 0f;
        _actor.Direction = Vector3.zero;
        _speed = 0;
        _direction = Vector3.zero;
}

    public struct Settings
    {
        public Vector3 Direction;
        public float Speed;

        public Settings(Vector3 direction, float speed)
        {
            Direction = direction;
            Speed = speed;
        }
    }

    public class Factory : PlaceholderFactory<ActorSpringyPhysics>
    {

    }
}
