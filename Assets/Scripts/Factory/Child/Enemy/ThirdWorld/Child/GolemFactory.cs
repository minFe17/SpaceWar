using UnityEngine;

public class GolemFactory : ThirdWorldEnemyFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _enemyType = EThirdWorldEnemyType.Golem;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject golem = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return golem;
    }
}