using System.Runtime.InteropServices;
using UnityEngine;

public class SignInPanel : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CheckAuthorized();

    private void Start()
    {
        //CheckAuthorized();
    }

    public void SetActiveGameObject()
    {
        gameObject.SetActive(true);
    }

    public void SetInactiveGameObject()
    {
        gameObject.SetActive(false);
    }
}
