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

    public override void InstallBindings()
    {
        Container.BindInstance(gameData).AsSingle().NonLazy();
        Container.BindInstance(yandex).AsSingle().NonLazy();
        Container.BindInstance(language).AsSingle().NonLazy();
    }
}