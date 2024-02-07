using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TotalScoreSlider : MonoBehaviour
{
    private Slider _slider;
    private float _oneStarScore;
    private float _twoStarsScore;
    private float _threeStarsScore;

    private float _maxScore;
    private float _currentScore = 0;

    [Inject]
    private GameData _gameData;

    [Inject]
    private void Construct()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0;
        _slider.maxValue = 1;
    }

    public void SetMaxScore(float score)
    {
        _maxScore = score;
        _oneStarScore = 1 / 6f;
        _twoStarsScore = 2 / 3f;
        _threeStarsScore = 5 / 6f;
    }

    public void ChangeScore(float value)
    {
        float addScore = value / _maxScore;
        _currentScore += addScore;
        _slider.value += addScore;
    }

    public int GetNumStars()
    {
        if (_currentScore < _oneStarScore)
        {
            return 0;
        }

        if (_currentScore >= _oneStarScore && _currentScore < _twoStarsScore)
        {
            return 1;
        }

        if (_currentScore >= _twoStarsScore && _currentScore < _threeStarsScore)
        {
            return 2;
        }

        if (_currentScore >= _threeStarsScore)
        {
            return 3;
        }

        return 0;
    }

    public void SaveScore(int numStars)
    {
        _gameData.UpdateLevelData(_currentScore, numStars);
    }
}
