using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    //╫л╠шео
    Dictionary<Type, IObjectPool> _objectPools = new Dictionary<Type, IObjectPool>();
    EnemyObjectPool _enemyObjectPool = new EnemyObjectPool();
    ObstacleObjectPool _obstacleObjectPool = new ObstacleObjectPool();

    public EnemyObjectPool EnemyObjectPool { get => _enemyObjectPool; }
    public ObstacleObjectPool ObstacleObjectPool { get => _obstacleObjectPool; }

    void Awake()
    {
        _enemyObjectPool = new EnemyObjectPool();
        _obstacleObjectPool = new ObstacleObjectPool();

        _objectPools.Add(typeof(EBulletPoolType), new ObjectPool<EBulletPoolType>());
        _objectPools.Add(typeof(ECoinType), new ObjectPool<ECoinType>());
        _objectPools.Add(typeof(EEventRoomType), new ObjectPool<EEventRoomType>());
        _objectPools.Add(typeof(EGroundWorkType), new ObjectPool<EGroundWorkType>());
        _objectPools.Add(typeof(ECameraType), new ObjectPool<ECameraType>());
        _objectPools.Add(typeof(EMapPoolType), new ObjectPool<EMapPoolType>());
        _objectPools.Add(typeof(EPlayerType), new ObjectPool<EPlayerType>());
        CreateQueue();
    }

    void CreateQueue()
    {
        _objectPools[typeof(EBulletPoolType)].Init();
        _objectPools[typeof(ECoinType)].Init();
        _objectPools[typeof(EEventRoomType)].Init();
        _objectPools[typeof(EGroundWorkType)].Init();
        _objectPools[typeof(ECameraType)].Init();
        _objectPools[typeof(EMapPoolType)].Init();
        _objectPools[typeof(EPlayerType)].Init();
    }

    public GameObject Push<TEnum>(TEnum type, GameObject prefab) where TEnum : Enum
    {
        IObjectPool objectPool;
        _objectPools.TryGetValue(typeof(TEnum), out objectPool);
        return objectPool.Push(type, prefab);
    }

    public void Pull<TEnum>(TEnum type, GameObject obj) where TEnum : Enum
    {
        IObjectPool objectPool;
        _objectPools.TryGetValue(typeof(TEnum), out objectPool);
        objectPool.Pull(type, obj);
    }

    public void ChangeWorldPool()
    {
        _enemyObjectPool.ChangePool();
        _obstacleObjectPool.ChangePool();

        if (_objectPools[typeof(EMapPoolType)] != null)
            _objectPools[typeof(EMapPoolType)].ClearChild();
    }
}