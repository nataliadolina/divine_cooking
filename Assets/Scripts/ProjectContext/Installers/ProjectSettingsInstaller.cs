using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectSettingsInstaller", menuName = "Installers/ProjectSettingsInstaller")]
public class ProjectSettingsInstaller : ScriptableObjectInstaller<ProjectSettingsInstaller>
{
    [SerializeField]
    private GameData.Settings _gameSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(_gameSettings);
    }
}