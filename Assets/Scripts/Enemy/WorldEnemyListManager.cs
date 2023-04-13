using UnityEngine;
using Utils;

public abstract class WorldEnemyListManager : MonoBehaviour
{
    public abstract void AddEnemyList();
}

public class FirstWorld : WorldEnemyListManager
{
    public override void AddEnemyList()
    {
        for (int i = 0; i < (int)EFirstWorldEnemyType.Max; i++)
        {
            GenericSingleton<EnemyManager>.Instance.Enemys.Add(Resources.Load($"Prefabs/Enemys/{(EWorldType)GenericSingleton<GameManager>.Instance.MapStage}/{(EFirstWorldEnemyType)i}") as GameObject);
        }
    }
}

public class SecondWorld : WorldEnemyListManager
{
    public override void AddEnemyList()
    {
        for (int i = 0; i < (int)ESecondWorldEnemyType.Max; i++)
        {
            GenericSingleton<EnemyManager>.Instance.Enemys.Add(Resources.Load($"Prefabs/Enemys/{(EWorldType)GenericSingleton<GameManager>.Instance.MapStage}/{(ESecondWorldEnemyType)i}") as GameObject);
        }
    }
}

public class ThirdWorld : WorldEnemyListManager
{
    public override void AddEnemyList()
    {
        for (int i = 0; i < (int)EThirdWorldEnemyType.Max; i++)
        {
            GenericSingleton<EnemyManager>.Instance.Enemys.Add(Resources.Load($"Prefabs/Enemys/{(EWorldType)GenericSingleton<GameManager>.Instance.MapStage}/{(EThirdWorldEnemyType)i}") as GameObject);
        }
    }
}
