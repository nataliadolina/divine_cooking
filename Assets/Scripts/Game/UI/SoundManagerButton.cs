using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SoundManagerButton : ButtonBase
{
    [SerializeField]
    private Sprite muteIcon;
    [SerializeField]
    private Sprite unmuteIcon;

    [Inject]
    private SoundManager _soundManager;
    [Inject]
    private GameData _gameData;

    private Image _image;

    private bool _isMute;
    private Dictionary<bool, Sprite> _iconMap;

    private void Start()
    {
        _image = GetComponent<Image>();
        _iconMap = new Dictionary<bool, Sprite>()
        {
            { true, muteIcon},
            { false, unmuteIcon}
        };

        _isMute = _gameData.MuteSound;
        _image.sprite = _iconMap[_isMute];
    }

    protected override void OnClick()
    {
        _isMute = !_isMute;
        _image.sprite = _iconMap[_isMute];

        _soundManager.MuteSound = _isMute;
    }
}
