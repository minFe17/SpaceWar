using UnityEngine;

public class BearFactory : ThirdWorldEnemyFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _enemyType = EThirdWorldEnemyType.Bear;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject bear = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return bear;
    }
}