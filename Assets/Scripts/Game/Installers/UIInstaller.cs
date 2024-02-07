using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject star;
    public override void InstallBindings()
    {
        Container.BindMemoryPool<StarUI, StarUI.Pool>()
            .WithInitialSize(2)
            .FromComponentInNewPrefab(star)
            .AsCached()
            .NonLazy();
    }
}