using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    Dictionary<Type, IFactorys> _enemyFactorys = new Dictionary<Type, IFactorys>();
    public Dictionary<Type, IFactorys> EnemyFactorys { get => _enemyFactorys; }

    void Awake()
    {
        _enemyFactorys.Add(typeof(EFirstWorldEnemyType), new Factory<EFirstWorldEnemyType>());
        _enemyFactorys.Add(typeof(ESecondWorldEnemyType), new Factory<ESecondWorldEnemyType>());
        _enemyFactorys.Add(typeof(EThirdWorldEnemyType), new Factory<EThirdWorldEnemyType>());
    }

    public void AddFactory<TEnum, T>(TEnum key, IFactory<T> value) where TEnum : Enum
    {
        IFactorys factory;
        _enemyFactorys.TryGetValue(typeof(TEnum), out factory);
        factory.AddFactorys(key, value);
    }

    public T MakeObject<TEnum, T>(TEnum key) where TEnum : Enum
    {
        IFactorys factory;
        _enemyFactorys.TryGetValue(typeof(TEnum), out factory);
        return (T)factory.MakeObject(key);
    }
}