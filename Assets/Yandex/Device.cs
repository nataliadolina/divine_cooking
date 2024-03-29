using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Device : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetPlatform();

    public string Platform = "desktop";

    private void Start()
    {
#if UNITY_WEBGL
        Platform = GetPlatform();
#endif
    }
}
