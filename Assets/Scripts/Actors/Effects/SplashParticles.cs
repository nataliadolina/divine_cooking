using UnityEngine;
using Zenject;

public class SplashParticles : MonoBehaviour, IDespawnable
{
    private ParticleSystem[] _ps;
    [Inject]
    private Pool _pool;

    public void Init()
    {
        _ps = GetComponentsInChildren<ParticleSystem>();
    }

    public void SetColor(Color color)
    {
        foreach (var ps in _ps)
        {
            ParticleSystem.MainModule main = ps.main;
            main.startColor = color;
        }
    }

    public void Despawn()
    {
        _pool.Despawn(this);
    }

    public class Pool : MemoryPool<Color, SplashParticles>
    {
        protected override void Reinitialize(Color color, SplashParticles item)
        {
            item.SetColor(color);
        }

        protected override void OnCreated(SplashParticles item)
        {
            item.Init();
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(SplashParticles item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void OnDespawned(SplashParticles item)
        {
            item.gameObject.SetActive(false);
        }
    }
}
