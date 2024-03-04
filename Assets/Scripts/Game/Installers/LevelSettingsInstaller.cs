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

    [Inject]
    private GameData _gameData;

    public override void InstallBindings()
    {
        _gameData.CurrentLevel = levelSettings.NumLevel;
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