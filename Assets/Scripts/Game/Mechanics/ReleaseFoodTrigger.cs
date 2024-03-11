using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ReleaseFoodTrigger : MonoBehaviour
{
    [Inject]
    private CurrentFoodCookingProgressPool _currentFoodProgress;

    [Inject]
    private GameManager _gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IFood food;
        if (collision.TryGetComponent(out food))
        {
            _currentFoodProgress.ReleaseDoubleSlider(food);
            _gameManager.AddNumFoodReleased = food.PartOfOne;
            if (food.PartOfOne == 1)
            {
                food.Despawn();
            }

            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
