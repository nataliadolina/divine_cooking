using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PauseButton : ButtonBase
{
    [Inject]
    private UIManager _uiManager;

    protected override void OnClick()
    {
        Time.timeScale = 0;
        _uiManager.ChangeGroupType(UIGroupType.Pause);
    }
}
