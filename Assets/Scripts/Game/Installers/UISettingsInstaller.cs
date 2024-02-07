using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "UISettingsInstaller", menuName = "Installers/UISettingsInstaller")]
public class UISettingsInstaller : ScriptableObjectInstaller<UISettingsInstaller>
{
    [SerializeField]
    private StarsManager.Settings starsManagerSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(starsManagerSettings);
    }
}