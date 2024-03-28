using System.Runtime.InteropServices;

public class SignInButton : ButtonBase
{
    [DllImport("__Internal")]
    private static extern void Auth();

    protected override void OnClick()
    {
#if UNITY_WEBGL
        Auth();
#endif
    }
}
