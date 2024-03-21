using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TotalScoreSlider : MonoBehaviour
{
    private Slider _slider;
    private float _oneStarScore;
    private float _twoStarsScore;
    private float _threeStarsScore;

    private float _currentScore = 0;
    private int _currentLevel;
    public int NumStars = 0;

    [Inject]
    private GameData _gameData;

    [Inject]
    private void Construct(LevelSettingsInstaller.Settings levelSettings)
    {
        _currentLevel = levelSettings.NumLevel;
    }

    public void SetMaxScore(float score)
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0;
        _slider.maxValue = score;
        _oneStarScore = score * 0.5f;
        _twoStarsScore = score * 5 / 6f;
        _threeStarsScore = score * 9 / 10f;
    }

    public void ChangeScore(float value)
    {
        _currentScore += value;
        _slider.value += value;
    }

    public int GetNumStars()
    {
        if (_currentScore < _oneStarScore)
        {
            NumStars = 0;
        }

        if (_currentScore >= _oneStarScore && _currentScore < _twoStarsScore)
        {
            NumStars = 1;
        }

        if (_currentScore >= _twoStarsScore && _currentScore < _threeStarsScore)
        {
            NumStars = 2;
        }

        if (_currentScore >= _threeStarsScore)
        {
            NumStars = 3;
        }

        return NumStars;
    }

    public void SaveScore(int numStars)
    {
        _gameData.UpdateLevelData(_currentScore, numStars, _currentLevel);
    }
}
