using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

public class GameData
{
    public int CurrentLevel = 1;
    public Dictionary<int, LevelData> LevelDatasMap = new Dictionary<int, LevelData>();
    public bool MuteSound = false;

    private GameData(Setting setting)
    {
        LoadProgressFromFile(setting.LevelCount);
        if (!LevelDatasMap.ContainsKey(1))
        {
            LevelDatasMap.Add(1, new LevelData(1, 0, 0));
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
        bool needToSavePlayerPrefs = false;
        if (!LevelDatasMap.ContainsKey(_level))
        {
            LevelData newLevelData = new LevelData(_level, score, numStars);
            LevelDatasMap.Add(_level, newLevelData);
            needToSavePlayerPrefs = true;

            PlayerPrefs.SetString(_level.ToString(), newLevelData.ToString());
        }
        else if (LevelDatasMap[_level].Score < score)
        {
            LevelData levelData = LevelDatasMap[_level];
            levelData.Score = score;
            levelData.NumStars = numStars;
            PlayerPrefs.SetString(_level.ToString(), levelData.ToString());
            needToSavePlayerPrefs = true;
        }

        if (!LevelDatasMap.ContainsKey(_level + 1) && numStars > 0)
        {
            int nextLevel = _level + 1;
            LevelData nextLevelData = new LevelData(nextLevel, 0, 0);
            PlayerPrefs.SetString(nextLevel.ToString(), nextLevelData.ToString());
            LevelDatasMap.Add(nextLevel, nextLevelData);
            needToSavePlayerPrefs = true;
        }

        if (needToSavePlayerPrefs)
        {
            PlayerPrefs.Save();
        }
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
