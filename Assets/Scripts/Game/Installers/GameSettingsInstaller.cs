using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField]
    private GameInstaller.Settings gameInstallerSetttings;
    [SerializeField]
    private Food.FoodSettings[] foodSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(gameInstallerSetttings);
        foreach (var food in foodSettings)
        {
            Container.BindInstance(food);
        }
    }
}