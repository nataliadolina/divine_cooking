using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseFoodTrigger : MonoBehaviour
{
    private CurrentFoodCookingProgressPool _currentFoodProgress;

    private void Start()
    {
        _currentFoodProgress = FindObjectOfType<CurrentFoodCookingProgressPool>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Food food;
        if (collision.TryGetComponent(out food))
        {
            _currentFoodProgress.ReleaseDoubleSlider(food);
        }
    }
}
