using Zenject;
using UnityEngine.SceneManagement;

public class NextLevelButton : ButtonBase
{
    [Inject]
    private GameData _gameData;

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

        _gameData.CurrentLevel = _currentLevel + 1;
        SceneManager.LoadScene($"Level {_currentLevel + 1}");
    }
}
