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

    private GameData()
    {
        LevelDatasMap.Add(1, new LevelData(1, 0, 0));
    }

    public void UpdateLevelData(float score, int numStars)
    {
        if (!LevelDatasMap.ContainsKey(CurrentLevel))
        {
            LevelDatasMap.Add(CurrentLevel, new LevelData(CurrentLevel, score, numStars));
        }
        else if (LevelDatasMap[CurrentLevel].Score < score)
        {
            LevelDatasMap[CurrentLevel] = new LevelData(CurrentLevel, score, numStars);
        }

        if (!LevelDatasMap.ContainsKey(CurrentLevel + 1) && numStars > 0)
        {
            LevelDatasMap.Add(CurrentLevel + 1, new LevelData(CurrentLevel + 1, 0, 0));
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
