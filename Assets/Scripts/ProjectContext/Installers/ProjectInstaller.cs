using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private GameData gameData;

    public override void InstallBindings()
    {
        Container.BindInstance(gameData).AsSingle().NonLazy();
    }
}