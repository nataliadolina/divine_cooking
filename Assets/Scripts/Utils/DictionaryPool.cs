
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DictionaryPool<TKey, TValue, TFactory>
    where TFactory : IFactory<UnityEngine.Object, TValue>
    where TValue : MonoBehaviour, IPoolObject
{
    protected TFactory _factory;
    protected Dictionary<TKey, UnityEngine.Object> _prefabMap;
    protected Dictionary<TKey, List<TValue>> _freeObjects = new Dictionary<TKey, List<TValue>>();
    public DictionaryPool(Dictionary<TKey, UnityEngine.Object> prefabMap, TFactory factory)
    {
        _factory = factory;
        _prefabMap = prefabMap;
    }

    public virtual TValue Create(TKey key)
    {
        TValue obj = _factory.Create(_prefabMap[key]);
        obj.gameObject.SetActive(false);
        obj.OnCreate();
        _freeObjects.AddNewValue<TKey, TValue>(key, obj); 
        return obj;
    }

    public virtual TValue Spawn(TKey key)
    {
        List<TValue> valueList;
        TValue value = _freeObjects.TryGetValue(key, out valueList) ? valueList.Count > 0 ? valueList[0] : Create(key) : Create(key);
        value.gameObject.SetActive(true);
        _freeObjects[key].Remove(value);
        value.OnSpawn();
        Reinitialize(value);
        return value;
    }

    public virtual void Despawn(TKey key, TValue value)
    {
        value.gameObject.SetActive(false);
        value.OnDespawn();
        _freeObjects[key].Add(value);
    }

    protected virtual void Reinitialize(TValue value)
    {

    }
}
