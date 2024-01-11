using UnityEngine;
using System;

[Serializable]
public struct SpawnSpotTypeMap
{
    public SpawnSpotType Key;
    public Transform Value;

    public SpawnSpotTypeMap(SpawnSpotType spawnerType, Transform spawnerTransform)
    {
        Key = spawnerType;
        Value = spawnerTransform;
    }
}
