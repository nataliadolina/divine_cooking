using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePhysicsType : MonoBehaviour
{
    [SerializeField]
    private PhysicsType toType;
    [SerializeField]
    private bool changeTypeOnlyOnce;
    [SerializeField]
    private bool changeTypeOnlyForFood;

    private List<int> _actorIdes = new List<int>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IActor actor;
        if (collision.TryGetComponent<IActor>(out actor))
        {
            if (changeTypeOnlyForFood && actor.ActorType == ActorType.Bomb)
            {
                return;
            }

            if (!changeTypeOnlyOnce || changeTypeOnlyOnce && !_actorIdes.Contains(actor.RootInstanceId))
            {
                actor.ChangePhysics(toType);
                _actorIdes.Add(actor.RootInstanceId);
            }
        }
    }
}
