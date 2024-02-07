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

    [Inject]
    private void Construct(Settings settings)
    {
        _levelImageMap = new Dictionary<int, Sprite>
        {
            { -1, settings.LockedLevelImage},
            { 0, settings.ZeroStarsImage},
            { 1, settings.OneStarImage},
            { 2, settings.TwoStarsImage},
            { 3, settings.ThreeStarsImage}
        };
    }

    private void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _text.text = levelNum.ToString();

        int starsCount = _gameData.GetLevelStarsCount(levelNum);
        GetComponent<Image>().sprite = _levelImageMap[starsCount];
        _button = GetComponent<Button>();
        if (starsCount == -1)
        {
            _button.interactable = false;
            _text.gameObject.SetActive(false);
        }
        else
        {
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
