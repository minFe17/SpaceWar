using UnityEngine;

public class GolemFactory : ThirdWorldEnemyFactoryBase, IFactory<Enemy>
{
    protected override void Init()
    {
        _enemyType = EThirdWorldEnemyType.Golem;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    Enemy IFactory<Enemy>.MakeObject()
    {
        GameObject golem = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return golem.GetComponent<Enemy>();
    }
}