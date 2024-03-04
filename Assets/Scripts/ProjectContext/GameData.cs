using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelData
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

    public void UpdateData(float score, int numStars)
    {
        Score = score;
        NumStars = numStars;
    }
}

public class GameData
{
    public int CurrentLevel = 1;
    public Dictionary<int, LevelData> LevelDatasMap = new Dictionary<int, LevelData>();
    public bool MuteSound = false;

    private GameData()
    {
        LevelDatasMap.Add(1, new LevelData(1, 0, 0));
    }

    public void UpdateLevelData(float score, int numStars, int? level = null)
    {
        int _level = level ?? CurrentLevel;
        if (!LevelDatasMap.ContainsKey(_level))
        {
            LevelDatasMap.Add(_level, new LevelData(_level, score, numStars));
        }
        else if (LevelDatasMap[_level].Score < score)
        {
            LevelDatasMap[_level] = new LevelData(_level, score, numStars);
        }

        if (!LevelDatasMap.ContainsKey(_level + 1) && numStars > 0)
        {
            LevelDatasMap.Add(_level + 1, new LevelData(_level + 1, 0, 0));
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
}
