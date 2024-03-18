using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SoundManagerButtonMenu : ButtonBase
{
    [SerializeField]
    private Sprite muteIcon;
    [SerializeField]
    private Sprite unmuteIcon;

    [Inject]
    private GameData _gameData;

    private Image _image;

    private bool _isMute;
    private Dictionary<bool, Sprite> _iconMap;

    [Inject]
    private void Construct()
    {
        _image = GetComponent<Image>();
        _iconMap = new Dictionary<bool, Sprite>()
        {
            { true, muteIcon},
            { false, unmuteIcon}
        };

        _gameData.onGameDataLoaded += SetMuteSound;
    }

    private void OnDestroy()
    {
        _gameData.onGameDataLoaded -= SetMuteSound;
    }

    private void Start()
    {
        SetMuteSound();
    }

    private void SetMuteSound()
    {
        _isMute = _gameData.MuteSound;
        _image.sprite = _iconMap[_isMute];
    }

    protected override void OnClick()
    {
        _isMute = !_isMute;
        _image.sprite = _iconMap[_isMute];
        _gameData.MuteSound = _isMute;
    }
}
