using UnityEngine;

public class RaptorFactory : SecondWorldEnemyFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _enemyType = ESecondWorldEnemyType.Raptor;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject raptor = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return raptor;
    }
}