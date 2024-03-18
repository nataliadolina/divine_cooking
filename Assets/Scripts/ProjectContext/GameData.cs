using System.Collections;
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
    public event Action onGameDataLoaded;

    public int CurrentLevel = 1;
    private Dictionary<int, LevelData> LevelDatasMap = new Dictionary<int, LevelData>();
    private GameDataList _gameData = new GameDataList();
    private bool _muteSound = false;
    public bool MuteSound { get => _muteSound;
        set
        { 
            if (_muteSound != value)
            {
                _muteSound = value;
                _gameData.MuteSound = value;

#if UNITY_WEBGL
                Save();
#endif
            }
        }
    }

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);
    [DllImport("__Internal")]
    private static extern void LoadExtern();

    private void Start()
    {
#if UNITY_WEBGL
        LoadExtern();
#endif
#if UNITY_EDITOR
        LevelData levelData = new LevelData(1, 0, 0);
        _gameData.LevelDatas.Add(levelData);
        LevelDatasMap.Add(1, levelData);
#endif
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

    public void UpdateLevelData(float score, int numStars, int? level = null)
    {
        int _level = level ?? CurrentLevel;
        bool shouldSave = false;
        if (!LevelDatasMap.ContainsKey(_level))
        {
            LevelData newLevelData = new LevelData(_level, score, numStars);
            _gameData.LevelDatas.Add(newLevelData);
            LevelDatasMap.Add(_level, newLevelData);
            shouldSave = true;
        }
        else if (LevelDatasMap[_level].Score < score)
        {
            LevelData levelData = LevelDatasMap[_level];
            levelData.Score = score;
            levelData.NumStars = numStars;
            shouldSave = true;
        }

        if (!LevelDatasMap.ContainsKey(_level + 1) && numStars > 0)
        {
            LevelData nextLevelData = new LevelData(_level + 1, 0, 0);
            _gameData.LevelDatas.Add(nextLevelData);
            LevelDatasMap.Add(_level + 1, nextLevelData);
            shouldSave = true;
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
}
