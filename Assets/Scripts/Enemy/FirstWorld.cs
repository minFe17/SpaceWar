using System.Collections.Generic;
using UnityEngine;

public class FirstWorld : WorldEnemyListBase
{
    public override List<GameObject> AddEnemyList()
    {
        for (int i = 0; i < (int)EFirstWorldEnemyType.Max; i++)
        {
            _worldEnemy.Add(Resources.Load($"Prefabs/Enemys/FirstWorld/{(EFirstWorldEnemyType)i}") as GameObject);
        }
        return _worldEnemy;
    }
}