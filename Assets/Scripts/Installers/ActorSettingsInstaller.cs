using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ActorSettingsInstaller", menuName = "Installers/ActorSettingsInstaller")]
public class ActorSettingsInstaller : ScriptableObjectInstaller<ActorSettingsInstaller>
{
    [SerializeField]
    private Actor.Settings settings;

    public override void InstallBindings()
    {
        Container.BindInstance(settings);
    }
}