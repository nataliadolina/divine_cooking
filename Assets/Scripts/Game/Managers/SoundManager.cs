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
    [SerializeField]
    private AudioSource _music;

    [Inject]
    private GameData _gameData;

    private bool _muteSound;

    public bool MuteSound
    {
        get
        {
            return _muteSound;
        }

        set
        {
            _cutSound.mute = value;
            _swordSound.mute = value;
            _explosionSound.mute = value;
            _music.mute = value;
            _muteSound = value;
            _gameData.MuteSound = value;
        }
    }

    [Inject]
    private void Construct()
    {
        _gameData.onGameDataLoaded += SetMuteSound;
    }

    private void OnDestroy()
    {
        _gameData.onGameDataLoaded -= SetMuteSound;
    }

    private void Start()
    {
        SetMuteSound();
    }

    private void SetMuteSound()
    {
        MuteSound = _gameData.MuteSound;
    }

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
