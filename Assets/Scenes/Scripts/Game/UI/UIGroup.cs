using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIGroupType
{
    Play,
    Win,
    Lose,
    Pause
}

public class UIGroup : MonoBehaviour
{
    [SerializeField]
    private UIGroupType groupType;

    public UIGroupType GroupType { get => groupType; }
}
