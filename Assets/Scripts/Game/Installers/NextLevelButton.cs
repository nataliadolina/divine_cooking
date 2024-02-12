using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class NextLevelButton : ButtonBase
{
    [Inject]
    private GameData _gameData;

    protected override void OnClick()
    {
        _gameData.CurrentLevel++;
        SceneManager.LoadScene($"Level {_gameData.CurrentLevel}");
    }
}
