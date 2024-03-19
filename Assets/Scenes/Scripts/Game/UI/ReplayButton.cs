using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class ReplayButton : ButtonBase
{
    private int _currentLevel;

    [Inject]
    private void Construct(LevelSettingsInstaller.Settings settings)
    {
        _currentLevel = settings.NumLevel;
    }

    protected override void OnClick()
    {
        SceneManager.LoadScene($"Level {_currentLevel}");
    }
}
