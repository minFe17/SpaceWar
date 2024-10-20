using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ObstacleObjectPool : MonoBehaviour
{
    List<IObstacleList> _worlds = new List<IObstacleList>();
    IObjectPool _enemyPool = null;
    GameManager _gameMaanger;

    public IObjectPool EnemyPool { get => _enemyPool; }

    void Init()
    {
        _gameMaanger = GenericSingleton<GameManager>.Instance;
        _worlds.Add(new FirstWorldObstacleList());
        _worlds.Add(new SecondWorldObstacleList());
        _worlds.Add(new ThirdWorldObstacleList());
    }

    void DestroyChild()
    {
        _enemyPool.Clear();
    }

    void MakePool()
    {
        _worlds[_gameMaanger.MapStage - 1].MakePool(this);
    }

    public void ChangePool()
    {
        if (_enemyPool != null)
            DestroyChild();
        MakePool();
    }

    public void CreateEnemyPool()
    {
        Init();
        MakePool();
    }

    public void CreatePool<T>() where T : Enum
    {
        _enemyPool = new ObjectPool<T>();
    }
}