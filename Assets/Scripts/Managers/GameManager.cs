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

    private Dictionary<SpawnSpotType, Vector3> _spawnPositionsMap = new Dictionary<SpawnSpotType, Vector3>();
    private Dictionary<ActorGroupType, IActorPoolSpawner> _spawnersMap = new Dictionary<ActorGroupType, IActorPoolSpawner>();
    
    [Inject]
    private void Construct(ActorPoolFactory actorPoolFactory, TotalScoreSlider totalScore)
    {
        foreach (ActorGroupType actorGroup in Enum.GetValues(typeof(ActorGroupType)))
        {
            _spawnersMap.Add(actorGroup, actorPoolFactory.CreateSpawner(actorGroup));
        }

        foreach (SpawnSpotTypeMap spawnSpot in spawnSpots)
        {
            _spawnPositionsMap.Add(spawnSpot.Key, spawnSpot.Value.position);
        }

        foreach (ActorSpawnerWaitTime actorSpawnerWaitTime in new HashSet<ActorSpawnerWaitTime>(actorsSpawnerWaitTimeMap))
        {
            ActorType actorType = actorSpawnerWaitTime.ActorType;
            ActorGroupType actorGroupType = actorType != ActorType.Bomb ? ActorGroupType.Food : ActorGroupType.Bomb;
            _spawnersMap[actorGroupType].FillPool(actorType, 2);
        }

        totalScore.SetMaxScore(actorsSpawnerWaitTimeMap.Where(x => x.ActorType != ActorType.Bomb).Count());
        StartCoroutine(WaitToSpawnNewPrefab());
    }

    private IEnumerator WaitToSpawnNewPrefab()
    {
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
        
        StopAllCoroutines();
    }
}
