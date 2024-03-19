using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class HomeButton : ButtonBase
{
    [Inject]
    private Yandex _yandex;

    private int _currentLevel;

    [Inject]
    private void Construct(LevelSettingsInstaller.Settings levelSettings)
    {
        _currentLevel = levelSettings.NumLevel;
    }

    protected override void OnClick()
    {
#if UNITY_WEBGL

        if (_currentLevel > 1)
        {
            _yandex.RateGameButton();
        }
        
#endif
        SceneManager.LoadScene("LevelMenu");
    }
}
