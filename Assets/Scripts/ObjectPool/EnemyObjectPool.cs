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
    /// ���� �� ���������� �´� ������ ���� ������Ʈ Ǯ�� ����
    /// </summary>
    void MakePool()
    {
        // ���� �����͸� ������
        if (_gameData == null)
            _gameData = DataSingleton<GameData>.Instance;

        // ���� ����Ʈ�� ��� ������ �ʱ�ȭ
        if (_worlds.Count == 0)
            MakeWorldList();

        // ���� ���������� �ش��ϴ� ������ Ǯ ����
        _worlds[_gameData.MapStage - 1].MakePool(this);
    }


    /// <summary>
    /// ���� ���� Ǯ�� �����ϰ�, ���� ���忡 �´� ���ο� Ǯ�� ��ü
    /// </summary>
    public void ChangePool()
    {
        // ���� Ǯ ������Ʈ�� �����ϸ� ��� ����
        if (_enemyPool != null)
            DestroyChild();

        // ���ο� Ǯ ����
        MakePool();
    }

    public void CreatePool<T>() where T : Enum
    {
        _enemyPool = new ObjectPool<T>();
        _enemyPool.Init();
    }
}