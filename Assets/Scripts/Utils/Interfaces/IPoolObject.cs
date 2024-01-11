using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolObject
{
    public void OnCreate();

    public void OnSpawn();
    public void OnDespawn();
}
