using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    Dictionary<Enum, IFactory<GameObject>> _obstacleFactorys = new Dictionary<Enum, IFactory<GameObject>>();

    public void AddFactory<TEnum>(TEnum key, IFactory<GameObject> value) where TEnum : Enum
    {
        if (!_obstacleFactorys.ContainsKey(key))
            _obstacleFactorys.Add(key, value);
    }

    public GameObject MakeObject<TEnum>(TEnum key) where TEnum : Enum
    {
        IFactory<GameObject> factory;
        _obstacleFactorys.TryGetValue(key, out factory);
        return factory.MakeObject();
    }

    public void RemoveFactory()
    {
        if (_obstacleFactorys.Count != 0)
            _obstacleFactorys.Clear();
    }
}