using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

public class Bomb : Actor
{
    [SerializeField]
    private GameObject explosion;

    private LivesManager _livesManager;
    private SoundManager _soundManager;

    private void Start()
    {
        _livesManager = FindObjectOfType<LivesManager>();
        _soundManager = FindObjectOfType<SoundManager>();
        explosion.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Blade>())
        {
            _soundManager.PlayExplosionSound();
            _livesManager.SubLife();
            explosion.transform.parent = null;
            explosion.SetActive(true);
            Destroy(gameObject);
        }
    }
}
