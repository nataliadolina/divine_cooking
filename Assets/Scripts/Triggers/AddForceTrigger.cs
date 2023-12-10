using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceTrigger : MonoBehaviour
{
    [SerializeField]
    private Direction direction;
    [SerializeField]
    private float force;

    private Dictionary<Direction, Vector3> _directionVector3Map;

    private void Start()
    {
        _directionVector3Map = new Dictionary<Direction, Vector3>()
        {
            { Direction.Up, Vector3.up },
            { Direction.Right, Vector3.right }
        };
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Food food;
        if (collision.TryGetComponent(out food))
        {
            food.AddForce(_directionVector3Map[direction], force);
        }
    }
}
