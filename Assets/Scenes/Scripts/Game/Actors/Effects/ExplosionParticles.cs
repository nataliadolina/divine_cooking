using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ExplosionParticles : MonoBehaviour, IDespawnable
{
    [Inject]
    private Pool _pool;

    public void Despawn()
    {
        _pool.Despawn(this);
    }

    public class Pool : MemoryPool<ExplosionParticles>
    {
        protected override void OnCreated(ExplosionParticles item)
        {
            item.gameObject.SetActive(false);
        }
        protected override void OnSpawned(ExplosionParticles item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(ExplosionParticles item)
        {
            item.gameObject.SetActive(false);
        }
    }
}
