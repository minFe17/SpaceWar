using System.Collections.Generic;
using UnityEngine;

public class SecondWorld : WorldEnemyListBase
{
    public override List<GameObject> AddEnemyList()
    {
        for (int i = 0; i < (int)ESecondWorldEnemyType.Max; i++)
        {
            _worldEnemy.Add(Resources.Load($"Prefabs/Enemys/SecondWorld/{(ESecondWorldEnemyType)i}") as GameObject);
        }
        return _worldEnemy;
    }
}