using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsWriter
{
    private int _levelNum;
    public PlayerPrefsWriter(LevelSettingsInstaller.Settings settings)
    {
        _levelNum = settings.NumLevel;
    }

    public void SetNumStars(int numStars)
    {
        if (!PlayerPrefs.HasKey($"level{_levelNum} numStars") || (PlayerPrefs.GetInt($"level{_levelNum} numStars") < numStars))
        {
            PlayerPrefs.SetInt($"level{_levelNum} numStars", numStars);
        }
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }
}
