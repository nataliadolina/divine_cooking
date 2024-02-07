using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class ReplayButton : ButtonBase
{
    [Inject]
    private GameData _gameData;

    protected override void OnClick()
    {
        SceneManager.LoadScene($"Level {_gameData.CurrentLevel}");
    }
}
