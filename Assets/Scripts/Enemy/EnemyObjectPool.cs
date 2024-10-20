using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyObjectPool : MonoBehaviour
{
    List<IWorldEnemyListBase> _worlds = new List<IWorldEnemyListBase>();
    IObjectPool _enemyPool = null;
    GameManager _gameMaanger;

    public IObjectPool EnemyPool { get=>_enemyPool; }

    void Init()
    {
        _gameMaanger = GenericSingleton<GameManager>.Instance;
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