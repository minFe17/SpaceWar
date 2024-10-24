using UnityEngine;

public class SkeletonFactory : ThirdWorldEnemyFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _enemyType = EThirdWorldEnemyType.Skeleton;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject skeleton = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return skeleton;
    }
}