using System.Runtime.InteropServices;
using UnityEngine;

public class Device : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetPlatform();

    [HideInInspector]
    public string Platform = "mobile";

    private void Start()
    {
#if UNITY_WEBGL
        Platform = GetPlatform();
#endif
    }
}
