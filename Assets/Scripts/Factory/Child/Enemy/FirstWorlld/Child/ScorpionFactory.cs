using UnityEngine;

public class ScorpionFactory : FirstWorldEnemyFactoryBase, IFactory<Enemy>
{
    protected override void Init()
    {
        _enemyType = EFirstWorldEnemyType.Scorpion;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    Enemy IFactory<Enemy>.MakeObject()
    {
        GameObject scorpion = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return scorpion.GetComponent<Enemy>();
    }
}