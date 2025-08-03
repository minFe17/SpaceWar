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

    /// <summary>
    /// 현재 맵 스테이지에 맞는 월드의 몬스터 오브젝트 풀을 생성
    /// </summary>
    void MakePool()
    {
        // 게임 데이터를 가져옴
        if (_gameData == null)
            _gameData = DataSingleton<GameData>.Instance;

        // 월드 리스트가 비어 있으면 초기화
        if (_worlds.Count == 0)
            MakeWorldList();

        // 현재 스테이지에 해당하는 월드의 풀 생성
        _worlds[_gameData.MapStage - 1].MakePool(this);
    }


    /// <summary>
    /// 기존 몬스터 풀을 제거하고, 현재 월드에 맞는 새로운 풀로 교체
    /// </summary>
    public void ChangePool()
    {
        // 기존 풀 오브젝트가 존재하면 모두 제거
        if (_enemyPool != null)
            DestroyChild();

        // 새로운 풀 생성
        MakePool();
    }

    public void CreatePool<T>() where T : Enum
    {
        _enemyPool = new ObjectPool<T>();
        _enemyPool.Init();
    }
}