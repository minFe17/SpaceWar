using UnityEngine;
using Utils;

public class SecondWorld : WorldEnemyListManager
{
    public override void AddEnemyList()
    {
        for (int i = 0; i < (int)ESecondWorldEnemyType.Max; i++)
        {
            _worldEnemy.Add(Resources.Load($"Prefabs/Enemys/SecondWorld/{(ESecondWorldEnemyType)i}") as GameObject);
        }
        GenericSingleton<EnemyManager>.Instance.Enemys = _worldEnemy;
    }
}