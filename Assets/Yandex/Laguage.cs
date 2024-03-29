using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;
using System;

public class Language : MonoBehaviour
{
    public event Action onSetLanguage;

    [DllImport("__Internal")]
    private static extern string GetLang();

    public string CurrentLanguage = "en";

    private void Start()
    {
#if UNITY_WEBGL
        //CurrentLanguage = GetLang();
        onSetLanguage?.Invoke();
#endif
    }
}
