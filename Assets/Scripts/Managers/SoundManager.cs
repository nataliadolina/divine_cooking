using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _cutSound;
    [SerializeField]
    private AudioSource _swordSound;
    [SerializeField]
    private AudioSource _explosionSound;

    public void PlayCutSound()
    {
        _cutSound.Play();
    }

    public void PlaySwordSound()
    {
        _swordSound.Play();
    }

    public void PlayExplosionSound()
    {
        _explosionSound.Play();
    }
}
