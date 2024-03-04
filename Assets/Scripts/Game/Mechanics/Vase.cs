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
        IFood food;
        if (collision.TryGetComponent(out food))
        {
            _totalScoreSlider.ChangeScore(food.CurrentScore);
            _currentFoodProgress.ReleaseDoubleSlider(food);
            _gameManager.AddNumFoodReleased = food.PartOfOne;
            Destroy(collision.gameObject);
        }
    }
}
