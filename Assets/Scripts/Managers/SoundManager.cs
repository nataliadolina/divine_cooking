using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource cutSound;
    [SerializeField]
    private AudioSource swordSound;
    [SerializeField]
    private AudioSource explosionSound;

    public void PlayCutSound()
    {
        cutSound.Play();
    }

    public void PlaySwordSound()
    {
        swordSound.Play();
    }

    public void PlayExplosionSound()
    {
        explosionSound.Play();
    }

}
