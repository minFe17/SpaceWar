using System;
using System.Collections.Generic;
using UnityEngine;

public class MapFactory : MonoBehaviour
{
    Dictionary<Enum, IFactory<GameObject>> _mapFactorys = new Dictionary<Enum, IFactory<GameObject>>();

    public void AddFactory<TEnum>(TEnum key, IFactory<GameObject> value) where TEnum : Enum
    {
        if (!_mapFactorys.ContainsKey(key))
            _mapFactorys.Add(key, value);
    }

    public GameObject MakeObject<TEnum>(TEnum key) where TEnum : Enum
    {
        IFactory<GameObject> factory;
        _mapFactorys.TryGetValue(key, out factory);
        return factory.MakeObject();
    }

    public void RemoveFactory()
    {
        if (_mapFactorys.Count != 0)
            _mapFactorys.Clear();
    }
}