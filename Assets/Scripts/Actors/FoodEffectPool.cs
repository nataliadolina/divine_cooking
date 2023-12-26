using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEffectPool : MonoBehaviour
{
    [SerializeField]
    private int poolSize;
    [SerializeField]
    private ParticleSystemPoolObject splashParticles;

    private List<ParticleSystemPoolObject> _freeSplashParticles = new List<ParticleSystemPoolObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            ParticleSystemPoolObject splash = Instantiate(splashParticles, transform);
            splash.Init();
            splash.gameObject.SetActive(false);
            _freeSplashParticles.Add(splash);
            splash.onReleaseElement += ReleaseElement;
        }
    }

    private ParticleSystemPoolObject TakeElement(Color color)
    {
        if (_freeSplashParticles.Count == 0)
        {
            var newSplash = Instantiate(splashParticles, transform);
            newSplash.Init();
            newSplash.onReleaseElement += ReleaseElement;
            _freeSplashParticles.Add(newSplash);
        }

        var splash = _freeSplashParticles[0];
        splash.gameObject.SetActive(true);
        _freeSplashParticles.Remove(splash);
        return splash;
    }

    private void ReleaseElement(ParticleSystemPoolObject element)
    {
        if (_freeSplashParticles.Contains(element))
        {
            return;
        }

        _freeSplashParticles.Add(element);
    }

    public void PlaySplashParticles(Color color, Vector3 position)
    {
        ParticleSystemPoolObject splash = TakeElement(color);
        splash.transform.position = position;
        splash.SetColor(color);
    }
}
