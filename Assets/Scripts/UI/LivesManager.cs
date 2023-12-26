using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private int _livesCount;

    private int _currentLives;
    private bool _gameOver = false;
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _livesCount;
        _slider.value = _livesCount;
        _currentLives = _livesCount;
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
