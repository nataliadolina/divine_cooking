using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System;
using System.Linq;
using Zenject;

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

    [Inject]
    private Settings _settings;

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
        LoadProgressFromFile(_settings.LevelCount);
        LoadMuteSettingsFromFile();

#if UNITY_WEBGL
        LoadExtern();
#endif
    }

    private void Save()
    {
        string jsonString = JsonUtility.ToJson(_gameData);
        SaveExtern(jsonString);
    }

    public void SetPlayerInfo(string value)
    {
        GameDataList newGameData = JsonUtility.FromJson<GameDataList>(value);
        _muteSound = _gameData.MuteSound;

        bool shouldSave = false;
        int maxLevelDatas = newGameData.LevelDatas.Count() > LevelDatasMap.Count() ? newGameData.LevelDatas.Count() : LevelDatasMap.Count();
        for (int i = 1; i <= maxLevelDatas; i++)
        {
            LevelData currentLevelData = null;
            LevelData levelDataExtern = newGameData.LevelDatas.Where(x => x.LevelNum == i).FirstOrDefault();
            if (LevelDatasMap.TryGetValue(i, out currentLevelData))
            {
                if (levelDataExtern != null)
                {
                    if (currentLevelData.NumStars > levelDataExtern.NumStars)
                    {
                        shouldSave = true;
                    }
                    else
                    {
                        _gameData.LevelDatas.Remove(currentLevelData);
                        _gameData.LevelDatas.Add(levelDataExtern);

                        LevelDatasMap.Remove(i);
                        LevelDatasMap.Add(i, levelDataExtern);
                    }
                }
            }
            else if (levelDataExtern != null)
            {
                _gameData.LevelDatas.Add(levelDataExtern);
                LevelDatasMap.Add(i, levelDataExtern);
            }
        }

        if (_gameData.LevelDatas.Where(x => x.LevelNum == 1).Count() == 0 )
        {
            LevelData firstLevelData = new LevelData(1, 0, 0);
            _gameData.LevelDatas.Add(firstLevelData);
            LevelDatasMap.Add(1, firstLevelData);
        }

#if UNITY_WEBGL
        if (shouldSave)
        {
            Save();
        }
#endif

        onGameDataLoaded?.Invoke();
    }

    private void LoadMuteSettingsFromFile()
    {
        if (PlayerPrefs.HasKey("MuteSound"))
        {
            MuteSound = PlayerPrefs.GetInt("MuteSound") == 1 ? true : false;
        }
        onGameDataLoaded?.Invoke();
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
                if (!LevelDatasMap.ContainsKey(i))
                {
                    LevelDatasMap.Add(i, levelData);
                    _gameData.LevelDatas.Add(levelData);
                }
                else if (LevelDatasMap[i].NumStars < levelData.NumStars)
                {
                    LevelDatasMap[i].NumStars = levelData.NumStars;
                }
            }
        }

        if (!LevelDatasMap.ContainsKey(1))
        {
            LevelData levelData = new LevelData(1, 0, 0);
            _gameData.LevelDatas.Add(levelData);
            LevelDatasMap.Add(1, levelData);
        }

        onGameDataLoaded?.Invoke();
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
    public class Settings
    {
        public int LevelCount;
    }
}
