using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Vase : MonoBehaviour
{
    [Inject]
    private CurrentFoodCookingProgressPool _currentFoodProgress;

    [Inject]
    private GameManager _gameManager;

    [Inject]
    private TotalScoreSlider _totalScoreSlider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food food;
        if (collision.TryGetComponent(out food))
        {
            _totalScoreSlider.ChangeScore(food.CurrentTotalScore);
            _currentFoodProgress.ReleaseDoubleSlider(food);
            _gameManager.AddNumFoodReleased = food.PartOfOne;

            if (food.PartOfOne == 1)
            {
                food.Despawn();
            }

            else
            {
                Destroy(food.gameObject);
            }
        }
    }
}
