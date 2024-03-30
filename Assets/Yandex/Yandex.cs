using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void RateGame();

    public void RateGameButton()
    {
#if UNITY_WEBGL
        RateGame();
#endif
    }
}
