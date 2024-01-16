using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "FoodSettingsInstaller", menuName = "Installers/FoodSettingsInstaller")]
public class FoodSettingsInstaller : ScriptableObjectInstaller<FoodSettingsInstaller>
{
    [SerializeField]
    private Food.FoodSettings _foodSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(_foodSettings);
    }
}