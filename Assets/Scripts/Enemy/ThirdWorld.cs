using System.Collections.Generic;
using UnityEngine;

public class ThirdWorld : WorldEnemyListBase
{
    public override List<GameObject> AddEnemyList()
    {
        for (int i = 0; i < (int)EThirdWorldEnemyType.Max; i++)
        {
            _worldEnemy.Add(Resources.Load($"Prefabs/Enemys/ThirdWorld/{(EThirdWorldEnemyType)i}") as GameObject);
        }
        return _worldEnemy;
    }
}