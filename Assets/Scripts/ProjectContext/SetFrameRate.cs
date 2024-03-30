using UnityEngine;

public class SetFrameRate : MonoBehaviour
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 0; 
        Application.targetFrameRate = 60;
    }
}
