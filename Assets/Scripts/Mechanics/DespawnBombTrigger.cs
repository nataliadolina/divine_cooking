using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class DespawnBombTrigger : MonoBehaviour
{
    [Inject]
    private Bomb.Pool _bombPool;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bomb bomb;
        if (collision.TryGetComponent(out bomb))
        {
            _bombPool.Despawn(bomb);
        }
    }
}
