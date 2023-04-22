using UnityEngine;
using Utils;

public class ThirdWorld : WorldEnemyListManager
{
    public override void AddEnemyList()
    {
        for (int i = 0; i < (int)EThirdWorldEnemyType.Max; i++)
        {
            _worldEnemy.Add(Resources.Load($"Prefabs/Enemys/ThirdWorld/{(EThirdWorldEnemyType)i}") as GameObject);
        }
        GenericSingleton<EnemyManager>.Instance.Enemys = _worldEnemy;
    }
}