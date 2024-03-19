using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour
{
    private Dictionary<UIGroupType, GameObject> _panelsMap = new Dictionary<UIGroupType, GameObject>();
    private UIGroupType? _lastGroupType = null;

    [Inject]
    private void Construct(UIGroup[] uiGroups)
    {
        foreach (var uiGroup in uiGroups)
        {
            _panelsMap.Add(uiGroup.GroupType, uiGroup.gameObject);
        }    
    }

    public void ChangeGroupType(UIGroupType groupType)
    {
        if (_lastGroupType != null)
        {
            _panelsMap[(UIGroupType)_lastGroupType].SetActive(false);
        }
        GameObject newGroupType = _panelsMap[groupType];
        newGroupType.SetActive(true);
        _lastGroupType = groupType;
    }
}
