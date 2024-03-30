using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using System.Linq;
using System.Runtime.InteropServices;

[Serializable]
public struct ActorSpawnerWaitTime
{
    public ActorType ActorType;
    public SpawnSpotType SpawnSpotType;
    public float WaitTimeSeconds;

    public ActorSpawnerWaitTime(ActorType actorType, SpawnSpotType spawnSpotType, float waitTimeSeconds)
    {
        ActorType = actorType;
        WaitTimeSeconds = waitTimeSeconds;
        SpawnSpotType = spawnSpotType;
    }
}

public class GameManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();

    [SerializeField]
    private float waitToStartTime;

    [SerializeField]
    private ActorSpawnerWaitTime[] actorsSpawnerWaitTimeMap;

    [SerializeField]
    private SpawnSpotTypeMap[] spawnSpots;

    [Inject]
    private GameData gameData;

    [Inject]
    private SoundManager soundManager;

    private Dictionary<SpawnSpotType, Vector3> _spawnPositionsMap = new Dictionary<SpawnSpotType, Vector3>();
    private Dictionary<ActorGroupType, IActorPoolSpawner> _spawnersMap = new Dictionary<ActorGroupType, IActorPoolSpawner>();

    private TotalScoreSlider _totalScoreSlider;
    private UIManager _uiManager;

    private int _numStars = 0;
    private int _numFoodTotal = 0;
    private float _numFoodReleased = 0;
    public float AddNumFoodReleased
    { 
        set 
        {
            if (_numFoodReleased + value >= (float)_numFoodTotal)
            {
                EndGameLoseOrWin();
            }

            _numFoodReleased+=value;
        }
    }

    [Inject]
    private void Construct(ActorPoolFactory actorPoolFactory, UIManager uiManager, TotalScoreSlider totalScore, Food.FoodSettings[] foodSettings)
    {
        _uiManager = uiManager;
        _totalScoreSlider = totalScore;
        foreach (ActorGroupType actorGroup in Enum.GetValues(typeof(ActorGroupType)))
        {
            _spawnersMap.Add(actorGroup, actorPoolFactory.CreateSpawner(actorGroup));
        }

        foreach (SpawnSpotTypeMap spawnSpot in spawnSpots)
        {
            _spawnPositionsMap.Add(spawnSpot.Key, spawnSpot.Value.position);
        }

        List<ActorType> actorTypesInGame = new List<ActorType>();
        float maxScore = 0;

        foreach (ActorSpawnerWaitTime actorSpawnerWaitTime in actorsSpawnerWaitTimeMap)
        {
            ActorType actorType = actorSpawnerWaitTime.ActorType;
            
            ActorGroupType actorGroupType = actorType != ActorType.Bomb ? ActorGroupType.Food : ActorGroupType.Bomb;

            if (!actorTypesInGame.Contains(actorType))
            {
                _spawnersMap[actorGroupType].FillPool(actorType, 1);
                actorTypesInGame.Add(actorType);
            }
            
            if (actorGroupType == ActorGroupType.Food)
            {
                _numFoodTotal++;
                maxScore += foodSettings.Where(x => x.ActorType == actorType).FirstOrDefault().CookingActions.Length;
            }
        }

        totalScore.SetMaxScore(maxScore);
    }

    private void Start()
    {
#if UNITY_WEBGL
        if (Time.time - gameData.LastAdvShowTime > 60f)
        {
            gameData.LastAdvShowTime = Time.time;
            soundManager.PauseMusic();
            ShowAdv();
        }
        else
        {
            StartCoroutine(WaitToSpawnNewPrefab());
        }
#endif
#if UNITY_EDITOR
        StartCoroutine(WaitToSpawnNewPrefab());
#endif
    }

    public void StartGame()
    {
        StartCoroutine(WaitToSpawnNewPrefab());
    }

    private IEnumerator WaitToSpawnNewPrefab()
    {
        _uiManager.ChangeGroupType(UIGroupType.Play);
        yield return new WaitForSeconds(waitToStartTime);
        foreach (var actorWaitTime in actorsSpawnerWaitTimeMap)
        {
            ActorType actorType = actorWaitTime.ActorType;
            ActorGroupType actorGroupType = actorType != ActorType.Bomb ? ActorGroupType.Food : ActorGroupType.Bomb;
            Vector3 spawnPosition = _spawnPositionsMap[actorWaitTime.SpawnSpotType];
            _spawnersMap[actorGroupType].Spawn(actorType, spawnPosition);
            float waitTime = actorWaitTime.WaitTimeSeconds;

            yield return new WaitForSeconds(waitTime);
        }
    }

    private void EndGameLoseOrWin()
    {
        _numStars = _totalScoreSlider.GetNumStars();
        _totalScoreSlider.SaveScore(_numStars);
        _uiManager.ChangeGroupType(_numStars == 0 ? UIGroupType.Lose : UIGroupType.Win);
        StopAllCoroutines();
    }

    public void EndGame()
    {
        _uiManager.ChangeGroupType(UIGroupType.Lose);
        StopAllCoroutines();
    }
}
