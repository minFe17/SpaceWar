using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    Dictionary<Enum, IFactory<Enemy>> _enemyFactorys = new Dictionary<Enum, IFactory<Enemy>>();

    public void AddFactory<TEnum>(TEnum key, IFactory<Enemy> value) where TEnum : Enum
    {
        if (_enemyFactorys.ContainsKey(key))
            return;
        _enemyFactorys.Add(key, value);
    }

    public Enemy MakeObject<TEnum>(TEnum key) where TEnum : Enum
    {
        IFactory<Enemy> factory;
        _enemyFactorys.TryGetValue(key, out factory);
        return factory.MakeObject();
    }

    public void RemoveFactory()
    {
        if (_enemyFactorys.Count != 0)
            _enemyFactorys.Clear();
    }
}