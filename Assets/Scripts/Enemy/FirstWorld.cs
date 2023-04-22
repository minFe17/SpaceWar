using UnityEngine;
using Utils;

public class FirstWorld : WorldEnemyListManager
{
    public override void AddEnemyList()
    {
        for (int i = 0; i < (int)EFirstWorldEnemyType.Max; i++)
        {
            _worldEnemy.Add(Resources.Load($"Prefabs/Enemys/FirstWorld/{(EFirstWorldEnemyType)i}") as GameObject);
        }
        GenericSingleton<EnemyManager>.Instance.Enemys = _worldEnemy;
    }
}