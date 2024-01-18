using UnityEngine;
using Zenject;

public class Bomb : Actor
{
    private LivesManager _livesManager;
    private SoundManager _soundManager;
    private ExplosionParticles.Pool _explodePool;

    private Pool _pool;

    [Inject]
    private void Construct(SoundManager soundManager, LivesManager livesManager, ExplosionParticles.Pool explodePool, Pool pool)
    {
        _soundManager = soundManager;
        _livesManager = livesManager;
        _explodePool = explodePool;
        _pool = pool;
    }

    public void ZeroVelocity()
    {
        _rigidbody.velocity = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Blade>())
        {
            _soundManager.PlayExplosionSound();
            _livesManager.SubLife();
            ExplosionParticles effect = _explodePool.Spawn();
            effect.transform.position = transform.position;
            _pool.Despawn(this);
        }
    }

    public class Pool : MemoryPool<Bomb>
    {
        protected override void OnCreated(Bomb item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(Bomb item)
        {
            item.ZeroVelocity();
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Bomb item)
        {
            item.gameObject.SetActive(false);
        }
    }
}
