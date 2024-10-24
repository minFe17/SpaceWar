using UnityEngine;

public class PachyFactory : SecondWorldEnemyFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _enemyType = ESecondWorldEnemyType.Pachy;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject pachy = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return pachy;
    }
}