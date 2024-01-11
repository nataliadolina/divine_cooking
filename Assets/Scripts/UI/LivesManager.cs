using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    [SerializeField]
    private int livesCount;

    private Slider _slider;
    private int _currentLives;
    private bool _gameOver = false;
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = livesCount;
        _slider.value = livesCount;
        _currentLives = livesCount;
    }

    public void SubLife()
    {
        _currentLives--;
        _slider.value--;
        if (_currentLives == 0)
        {
            _gameOver = true;
        }
    }
}
