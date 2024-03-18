using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LevelImage : MonoBehaviour
{
    [SerializeField]
    private int levelNum;

    [Inject]
    private GameData _gameData;

    private Dictionary<int, Sprite> _levelImageMap;
    private Button _button;
    private TMP_Text _text;

    private Image _image;

    [Inject]
    private void Construct(Settings settings)
    {
        _image = GetComponent<Image>();
        _levelImageMap = new Dictionary<int, Sprite>
        {
            { -1, settings.LockedLevelImage},
            { 0, settings.ZeroStarsImage},
            { 1, settings.OneStarImage},
            { 2, settings.TwoStarsImage},
            { 3, settings.ThreeStarsImage}
        };

        _text = GetComponentInChildren<TMP_Text>();
        _text.text = levelNum.ToString();
        _button = GetComponent<Button>();

        _gameData.onGameDataLoaded += SetStarsCount;
    }

    private void OnDestroy()
    {
        _gameData.onGameDataLoaded -= SetStarsCount;
    }

    private void Start()
    {
        SetStarsCount();
    }

    private void SetStarsCount()
    {
        int starsCount = _gameData.GetLevelStarsCount(levelNum);
        _image.sprite = _levelImageMap[starsCount];

        if (starsCount == -1)
        {
            _button.interactable = false;
            _text.gameObject.SetActive(false);
        }
        else
        {
            _button.interactable = true;
            _text.gameObject.SetActive(true);
            _button.onClick.AddListener(LoadLevel);
        }
    }

    private void LoadLevel()
    {
        _gameData.CurrentLevel = levelNum;
        SceneManager.LoadScene($"Level {levelNum}");
    }

    [Serializable]
    public struct Settings
    {
        public Sprite LockedLevelImage;
        public Sprite ZeroStarsImage;
        public Sprite OneStarImage;
        public Sprite TwoStarsImage;
        public Sprite ThreeStarsImage;
    }
}
