using UnityEngine;

public class ScorpionFactory : FirstWorldEnemyFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _enemyType = EFirstWorldEnemyType.Scorpion;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject scorpion = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return scorpion;
    }
}