using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnumList : MonoBehaviour
{
    List<Enum> _enemyEnumLists = new List<Enum>();

    public List<Enum> EnemyEnumLists { get => _enemyEnumLists; }

    void Awake()
    {
        _enemyEnumLists.Add(new EFirstWorldEnemyType());
        _enemyEnumLists.Add(new ESecondWorldEnemyType());
        _enemyEnumLists.Add(new EThirdWorldEnemyType());
    }

    public Enum GetZero()
    {
       return _enemyEnumLists[0];
    }
}