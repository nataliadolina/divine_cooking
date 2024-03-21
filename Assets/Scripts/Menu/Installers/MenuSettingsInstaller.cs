using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MenuSettingsInstaller", menuName = "Installers/MenuSettingsInstaller")]
public class MenuSettingsInstaller : ScriptableObjectInstaller<MenuSettingsInstaller>
{
    [SerializeField]
    private LevelImage.Settings levelImageSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(levelImageSettings);
    }
}