using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
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

    private GameState _currentGameState = GameState.Play;
    private Image _image;
    private Dictionary<GameState, Sprite> _gameStateIconMap;

    private void Start()
    {
        _image = GetComponent<Image>();
        _gameStateIconMap = new Dictionary<GameState, Sprite>()
        {
            { GameState.Play, resumeIcon},
            { GameState.Pause, stopIcon}
        };
    }

    protected override void OnClick()
    {
        Sprite newSprite = _gameStateIconMap.GetValueOrDefault(_currentGameState);
        _image.sprite = newSprite;

        Time.timeScale = _currentGameState == GameState.Play ? 0 : 1;
        _currentGameState = _currentGameState == GameState.Play ? GameState.Pause : GameState.Play;
        
    }
}
