using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    Dictionary<Type, IFactorys> _obstacleFactorys = new Dictionary<Type, IFactorys>();
    public Dictionary<Type, IFactorys> ObstacleFactorys { get => _obstacleFactorys; }

    void Awake()
    {
        _obstacleFactorys.Add(typeof(EFirstWorldObstacleType), new Factory<EFirstWorldObstacleType>());
        _obstacleFactorys.Add(typeof(ESecondWorldObstacleType), new Factory<ESecondWorldObstacleType>());
        _obstacleFactorys.Add(typeof(EThirdWorldObstacleType), new Factory<EThirdWorldObstacleType>());
    }

    public void AddFactory<TEnum, T>(TEnum key, IFactory<T> value) where TEnum : Enum
    {
        IFactorys factory;
        _obstacleFactorys.TryGetValue(typeof(TEnum), out factory);
        factory.AddFactorys(key, value);
    }

    public T MakeObject<TEnum, T>(TEnum key) where TEnum : Enum
    {
        IFactorys factory;
        _obstacleFactorys.TryGetValue(typeof(TEnum), out factory);
        return (T)factory.MakeObject(key);
    }
}