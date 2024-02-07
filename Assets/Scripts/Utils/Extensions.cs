using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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

    public static List<T> Copy<T>(this List<T> lst)
    {
        List<T> newList = new List<T>();
        foreach (var l in lst)
        {
            newList.Add(l);
        }

        return newList;
    }

    public static T[] EnumToArray<T>(this T _enum, T[] enumValues) where T : Enum
    {
        return enumValues.Where(x => _enum.HasFlag(x)).ToArray();
    }
}
