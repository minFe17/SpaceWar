using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ObstacleObjectPool : MonoBehaviour
{
    List<IObstacleList> _worlds = new List<IObstacleList>();
    IObjectPool _obstaclePool = null;
    GameManager _gameMaanger;

    public IObjectPool ObstaclePool { get => _obstaclePool; }

    void MakeWorldList()
    {
        _worlds.Add(new FirstWorldObstacleList());
        _worlds.Add(new SecondWorldObstacleList());
        _worlds.Add(new ThirdWorldObstacleList());
    }

    void DestroyChild()
    {
        _obstaclePool.Clear();
    }

    void MakePool()
    {
        if (_gameMaanger == null)
            _gameMaanger = GenericSingleton<GameManager>.Instance;
        if (_worlds.Count == 0)
            MakeWorldList();
        _worlds[_gameMaanger.MapStage - 1].MakePool(this);
    }

    public void ChangePool()
    {
        if (_obstaclePool != null)
            DestroyChild();
        MakePool();
    }

    public void CreatePool<T>() where T : Enum
    {
        _obstaclePool = new ObjectPool<T>();
    }
}