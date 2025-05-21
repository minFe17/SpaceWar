using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyObjectPool : MonoBehaviour
{
    List<IEnemyList> _worlds = new List<IEnemyList>();
    IObjectPool _enemyPool = null;
    GameData _gameData;

    public IObjectPool EnemyPool { get=>_enemyPool; }

    void MakeWorldList()
    {
        _worlds.Add(new FirstWorld());
        _worlds.Add(new SecondWorld());
        _worlds.Add(new ThirdWorld());
    }

    void DestroyChild()
    {
        _enemyPool.Clear();
    }

    void MakePool()
    {
        if (_gameData == null)
            _gameData = DataSingleton<GameData>.Instance;
        if(_worlds.Count == 0)
            MakeWorldList();
        
        _worlds[_gameData.MapStage - 1].MakePool(this);
    }

    public void ChangePool()
    {
        if (_enemyPool != null)
            DestroyChild();
        MakePool();
    }

    public void CreatePool<T>() where T : Enum
    {
        _enemyPool = new ObjectPool<T>();
        _enemyPool.Init();
    }
}