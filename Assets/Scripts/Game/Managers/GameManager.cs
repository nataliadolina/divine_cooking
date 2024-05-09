using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using System.Linq;

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
    [SerializeField]
    private float waitToStartTime;

    [SerializeField]
    private ActorSpawnerWaitTime[] actorsSpawnerWaitTimeMap;

    [SerializeField]
    private SpawnSpotTypeMap[] spawnSpots;

    private Queue<ActorSpawnerWaitTime> _actorsSpawnerWaitTimeQueue = new Queue<ActorSpawnerWaitTime>();

    private Dictionary<SpawnSpotType, Vector3> _spawnPositionsMap = new Dictionary<SpawnSpotType, Vector3>();
    private Dictionary<ActorGroupType, IActorPoolSpawner> _spawnersMap = new Dictionary<ActorGroupType, IActorPoolSpawner>();

    private TotalScoreSlider _totalScoreSlider;
    private UIManager _uiManager;

    private int _numStars = 0;
    private int _numFoodTotal = 0;
    private float _numFoodReleased = 0;
    private bool _isPaused = false;
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

    public bool IsSpawnActorsCoroutinePaused { get => _isPaused; set => _isPaused = value; }

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
            _actorsSpawnerWaitTimeQueue.Enqueue(actorSpawnerWaitTime);
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
        StartCoroutine(WaitToSpawnNewPrefab());
    }

    private IEnumerator WaitToSpawnNewPrefab()
    {
        _uiManager.ChangeGroupType(UIGroupType.Play);
        yield return new WaitForSeconds(waitToStartTime);
        while (_actorsSpawnerWaitTimeQueue.Count() > 0)
        {
            if (!_isPaused)
            {
                var actorWaitTime = _actorsSpawnerWaitTimeQueue.Dequeue();
                ActorType actorType = actorWaitTime.ActorType;
                ActorGroupType actorGroupType = actorType != ActorType.Bomb ? ActorGroupType.Food : ActorGroupType.Bomb;
                Vector3 spawnPosition = _spawnPositionsMap[actorWaitTime.SpawnSpotType];
                _spawnersMap[actorGroupType].Spawn(actorType, spawnPosition);
                float waitTime = actorWaitTime.WaitTimeSeconds;

                yield return new WaitForSeconds(waitTime);
            }

            else
            {
                yield return new WaitUntil(() => !_isPaused);
            }
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
