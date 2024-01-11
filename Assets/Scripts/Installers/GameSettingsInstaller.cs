using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField]
    private GameInstaller.Settings gameInstallerSetttings;
    public override void InstallBindings()
    {
        Container.BindInstance(gameInstallerSetttings);
    }
}