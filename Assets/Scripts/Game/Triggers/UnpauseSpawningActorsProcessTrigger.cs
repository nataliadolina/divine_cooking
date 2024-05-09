using UnityEngine;
using Zenject;

public class UnpauseSpawningActorsProcessTrigger : MonoBehaviour
{
    [SerializeField] private int numTriggersBeforeDestroy;

    private int _currentNumTriggers = 0;

    [Inject]
    private GameManager _gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Actor>())
        {
            _gameManager.IsSpawnActorsCoroutinePaused = false;

            if (_currentNumTriggers == numTriggersBeforeDestroy)
            {
                Destroy(gameObject);
            }
            _currentNumTriggers++;
        }
    }
}
