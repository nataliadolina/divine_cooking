using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private GameData gameData;
    [SerializeField]
    private Yandex yandex;
    [SerializeField]
    private Language language;
    [SerializeField]
    private Device device;

    public override void InstallBindings()
    {
        Container.BindInstance(gameData).AsSingle().NonLazy();
        Container.BindInstance(yandex).AsSingle().NonLazy();
        Container.BindInstance(language).AsSingle().NonLazy();
        Container.BindInstance(device).AsSingle().NonLazy();
    }
}