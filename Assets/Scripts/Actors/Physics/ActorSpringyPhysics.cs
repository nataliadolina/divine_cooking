using UnityEngine;
using Zenject;

public class ActorSpringyPhysics : ActorPhysicsBase
{
    private float _speed = 0;
    private Vector3 _direction = Vector3.zero;
    private IActor _actor;
    private Transform _transform;

    private ActorSpringyPhysics(IActor actor)
    {
        _actor = actor;
        _transform = _actor.Transform;
    }

    public override void OnStart()
    {
        _actor.Rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public override void Ricochet(Vector3 ricochetDirection)
    {
        _transform.RotateAround(_transform.position, Vector3.forward, 360 - _transform.eulerAngles.z * 2);
        _direction += ricochetDirection;
        _direction = _direction.normalized;
    }

    public override void MoveToAim(Vector3 aimPosition, float speed)
    {
        _speed = speed;
        _direction = new Vector3(aimPosition.x - _transform.position.x, aimPosition.y - _transform.position.y, 0).normalized;
    }

    public override void OnUpdate()
    {
        _transform.position += _direction * Time.deltaTime * _speed;
    }

    public class Factory : PlaceholderFactory<ActorSpringyPhysics>
    {

    }
}
