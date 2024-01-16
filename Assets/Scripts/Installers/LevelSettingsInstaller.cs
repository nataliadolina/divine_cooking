using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LevelSettingsInstaller", menuName = "Installers/LevelSettingsInstaller")]
public class LevelSettingsInstaller : ScriptableObjectInstaller<LevelSettingsInstaller>
{
    [SerializeField]
    private Actor.Settings settings;

    public override void InstallBindings()
    {
        Container.BindInstance(settings);
    }
}