using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectSettingsInstaller", menuName = "Installers/ProjectSettingsInstaller")]
public class ProjectSettingsInstaller : ScriptableObjectInstaller<ProjectSettingsInstaller>
{
    [SerializeField]
    private GameData.Setting _gameSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(_gameSettings);
    }
}