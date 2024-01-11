using UnityEngine;
using Zenject;

public class ActorSpringyPhysics : MonoBehaviour, ISpringy
{
    private bool _canRicochet = true;
    private bool _canMoveToAim = true;
    private float _speed = 0;
    private Vector3 _direction = Vector3.zero;
    private IActor _actor;
    private Transform _transform;

    [Inject]
    private void Construct(IActor actor)
    {
        _actor = actor;  
    }

    private void Start()
    {
        _actor.Rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        _transform = _actor.Transform;
    }

    public void Ricochet(Vector3 ricochetDirection)
    {
        if (!_canRicochet)
        {
            return;
        }

        _canRicochet = false;
        _transform.RotateAround(_transform.position, Vector3.forward, 360 - _transform.eulerAngles.z * 2);
        _direction += ricochetDirection;
        _direction = _direction.normalized;
    }

    public void MoveToAim(Vector3 aimPosition, float speed)
    {
        if (!_canMoveToAim)
        {
            return;
        }

        _speed = speed;
        _direction = new Vector3(aimPosition.x - _transform.position.x, aimPosition.y - _transform.position.y, 0).normalized;
        _canMoveToAim = false;
    }

    private void Update()
    {
        _transform.position += _direction * Time.deltaTime * _speed;
    }
}
