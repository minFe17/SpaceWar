using UnityEngine;

public class RaptorFactory : SecondWorldEnemyFactoryBase, IFactory<Enemy>
{
    protected override void Init()
    {
        _enemyType = ESecondWorldEnemyType.Raptor;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    Enemy IFactory<Enemy>.MakeObject()
    {
        GameObject raptor = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return raptor.GetComponent<Enemy>();
    }
}