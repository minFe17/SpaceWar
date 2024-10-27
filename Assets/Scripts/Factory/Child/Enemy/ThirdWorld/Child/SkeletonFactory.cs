using UnityEngine;

public class SkeletonFactory : ThirdWorldEnemyFactoryBase, IFactory<Enemy>
{
    protected override void Init()
    {
        _enemyType = EThirdWorldEnemyType.Skeleton;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    Enemy IFactory<Enemy>.MakeObject()
    {
        GameObject skeleton = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return skeleton.GetComponent<Enemy>();
    }
}