using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PauseSpawningActorsProcessTrigger : MonoBehaviour
{
    [Inject]
    private GameManager _gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Actor>())
        {
            _gameManager.IsSpawnActorsCoroutinePaused = true;
            Destroy(gameObject);
        }
    }
}
