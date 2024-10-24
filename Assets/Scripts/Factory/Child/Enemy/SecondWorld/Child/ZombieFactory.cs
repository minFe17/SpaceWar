using UnityEngine;

public class ZombieFactory : SecondWorldEnemyFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _enemyType = ESecondWorldEnemyType.Zombie;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject zombie = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return zombie;
    }
}