using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class NextLevelButton : ButtonBase
{
    [Inject]
    private GameData _gameData;

    private int _currentLevel;

    [Inject]
    private void Construct(LevelSettingsInstaller.Settings levelSettings)
    {
        _currentLevel = levelSettings.NumLevel;
    }

    protected override void OnClick()
    {
        _gameData.CurrentLevel = _currentLevel + 1;
        SceneManager.LoadScene($"Level {_currentLevel + 1}");
    }
}
