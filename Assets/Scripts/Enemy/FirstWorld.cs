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
