using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ParticleSystemPoolObject : MonoBehaviour
{
    public event Action<ParticleSystemPoolObject> onReleaseElement;
    private ParticleSystem[] _ps;

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

    private void OnDisable()
    {
        onReleaseElement?.Invoke(this);
    }
}
