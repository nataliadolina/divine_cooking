using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PauseButtonState
{
    Pause,
    Play
}
public class GameStateButton : ButtonBase
{
    [SerializeField]
    private Sprite stopIcon;
    [SerializeField]
    private Sprite resumeIcon;

    private PauseButtonState _currentGameState = PauseButtonState.Play;
    private Image _image;
    private Dictionary<PauseButtonState, Sprite> _gameStateIconMap;

    private void Start()
    {
        _image = GetComponent<Image>();
        _gameStateIconMap = new Dictionary<PauseButtonState, Sprite>()
        {
            { PauseButtonState.Play, resumeIcon},
            { PauseButtonState.Pause, stopIcon}
        };
    }

    protected override void OnClick()
    {
        Sprite newSprite = _gameStateIconMap.GetValueOrDefault(_currentGameState);
        _image.sprite = newSprite;

        Time.timeScale = _currentGameState == PauseButtonState.Play ? 0 : 1;
        _currentGameState = _currentGameState == PauseButtonState.Play ? PauseButtonState.Pause : PauseButtonState.Play;
        
    }
}
