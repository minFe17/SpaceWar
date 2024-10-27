using UnityEngine;

public class BearFactory : ThirdWorldEnemyFactoryBase, IFactory<Enemy>
{
    protected override void Init()
    {
        _enemyType = EThirdWorldEnemyType.Bear;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    Enemy IFactory<Enemy>.MakeObject()
    {
        GameObject bear = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return bear.GetComponent<Enemy>();
    }
}