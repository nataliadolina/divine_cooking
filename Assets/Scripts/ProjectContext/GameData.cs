using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class LevelData
{
    public int LevelNum;
    public float Score;
    public int NumStars;

    public LevelData(int levelNum, float score, int numStars)
    {
        LevelNum = levelNum;
        Score = score;
        NumStars = numStars;
    }

    public override string ToString()
    {
        return $"{Score} {NumStars}";
    }
}

[Serializable]
public class GameDataList
{
    public bool MuteSound;
    public List<LevelData> LevelDatas;
    public GameDataList()
    {
        MuteSound = false;
        LevelDatas = new List<LevelData>();
    }
}

public class GameData : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);
    [DllImport("__Internal")]
    private static extern void LoadExtern();

    public event Action onGameDataLoaded;

    public int CurrentLevel = 1;
    private Dictionary<int, LevelData> LevelDatasMap = new Dictionary<int, LevelData>();
    private GameDataList _gameData = new GameDataList();
    private bool _muteSound = false;

    public float LastAdvShowTime = 0f;
    public int NumSwitchedLevelsAfterAdvWasShown = 0;

    public bool MuteSound { get => _muteSound;
        set
        { 
            if (_muteSound != value)
            {
                _muteSound = value;
                _gameData.MuteSound = value;
                PlayerPrefs.SetInt("MuteSound", value ? 1 : 0);
                PlayerPrefs.Save();
#if UNITY_WEBGL
                Save();
#endif
            }
        }
    }

    private void Start()
    {
#if UNITY_WEBGL
        LoadExtern();
#endif
        LoadProgressFromFile(setting.LevelCount);
        LoadMuteSettingsFromFile();

        if (!LevelDatasMap.ContainsKey(1))
        {
            LevelData levelData = new LevelData(1, 0, 0);
            _gameData.LevelDatas.Add(levelData);
            LevelDatasMap.Add(1, levelData);
        }
    }

    private void Save()
    {
        string jsonString = JsonUtility.ToJson(_gameData);
        SaveExtern(jsonString);
    }

    public void SetPlayerInfo(string value)
    {
        _gameData = JsonUtility.FromJson<GameDataList>(value);
        _muteSound = _gameData.MuteSound;
        if (_gameData.LevelDatas.Where(x => x.LevelNum == 1).Count() == 0)
        {
            _gameData.LevelDatas.Add(new LevelData(1, 0, 0));
        }

        foreach (var levelData in _gameData.LevelDatas)
        {
            LevelDatasMap.Add(levelData.LevelNum, levelData);
        }

        onGameDataLoaded?.Invoke();
    }

    private void LoadMuteSettingsFromFile()
    {
        if (PlayerPrefs.HasKey("MuteSound"))
        {
            MuteSound = PlayerPrefs.GetInt("MuteSound") == 1 ? true : false;
        }
    }

    private void LoadProgressFromFile(int levelCount)
    {
        for (int i = 1; i <= levelCount; i++)
        {
            string levelNum = i.ToString();
            if (PlayerPrefs.HasKey(levelNum))
            {
                string[] value = PlayerPrefs.GetString(levelNum).Split();
                LevelData levelData = new LevelData(i, float.Parse(value[0]), int.Parse(value[1]));
                LevelDatasMap.Add(i, levelData);
            }
        }
    }

    public void UpdateLevelData(float score, int numStars, int? level = null)
    {
        int _level = level ?? CurrentLevel;
        bool shouldSave = false;
        bool needToSavePlayerPrefs = false;

        if (!LevelDatasMap.ContainsKey(_level))
        {
            LevelData newLevelData = new LevelData(_level, score, numStars);
            _gameData.LevelDatas.Add(newLevelData);
            LevelDatasMap.Add(_level, newLevelData);

            PlayerPrefs.SetString(_level.ToString(), newLevelData.ToString());

            shouldSave = true;
            needToSavePlayerPrefs = true;
        }

        else if (LevelDatasMap[_level].Score < score)
        {
            LevelData levelData = LevelDatasMap[_level];
            levelData.Score = score;
            levelData.NumStars = numStars;
            
            PlayerPrefs.SetString(_level.ToString(), levelData.ToString());
            needToSavePlayerPrefs = true;
            shouldSave = true;
        }

        if (!LevelDatasMap.ContainsKey(_level + 1) && numStars > 0)
        {
            int nextLevel = _level + 1;
            LevelData nextLevelData = new LevelData(nextLevel, 0, 0);
            _gameData.LevelDatas.Add(nextLevelData);
            
            LevelDatasMap.Add(nextLevel, nextLevelData);

            PlayerPrefs.SetString(nextLevel.ToString(), nextLevelData.ToString());

            needToSavePlayerPrefs = true;
            shouldSave = true;
        }

        if (needToSavePlayerPrefs)
        {
            PlayerPrefs.Save();
        }

#if UNITY_WEBGL

        if (shouldSave)
        {
            Save();
        }
        
#endif
    }

    public int GetLevelStarsCount(int level)
    {
        if (!LevelDatasMap.ContainsKey(level))
        {
            return -1;
        }

        return LevelDatasMap[level].NumStars;
    }

    [System.Serializable]
    public class Setting
    {
        public int LevelCount;
    }
}
