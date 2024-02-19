using UnityEngine;
using Zenject;
using System;

[CreateAssetMenu(fileName = "LevelSettingsInstaller", menuName = "Installers/LevelSettingsInstaller")]
public class LevelSettingsInstaller : ScriptableObjectInstaller<LevelSettingsInstaller>
{
    [SerializeField]
    private Actor.Settings settings;
    [SerializeField]
    private Settings levelSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(settings);
        Container.BindInstance(levelSettings);
    }

    [Serializable]
    public struct Settings
    {
        public int NumLevel;
        public float ActorScale;
    }
}