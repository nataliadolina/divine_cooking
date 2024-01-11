using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    private static int _maxId = 0;
    public static int GetUniqueId()
    {
        _maxId++;
        return _maxId;
    }

    public static void AddNewValue<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue value) 
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary.Add(key, new List<TValue>());
        }

        dictionary[key].Add(value);
    }
}
