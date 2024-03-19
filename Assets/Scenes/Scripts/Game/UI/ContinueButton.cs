using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ContinueButton : ButtonBase
{
    [Inject]
    private UIManager _uiManager;

    protected override void OnClick()
    {
        Time.timeScale = 1;
        _uiManager.ChangeGroupType(UIGroupType.Play);
    }
}
