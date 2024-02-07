using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : ButtonBase
{
    protected override void OnClick()
    {
        SceneManager.LoadScene("LevelMenu");
    }
}
