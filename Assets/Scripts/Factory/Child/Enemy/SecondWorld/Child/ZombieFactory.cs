using UnityEngine;

public class ZombieFactory : SecondWorldEnemyFactoryBase, IFactory<Enemy>
{
    protected override void Init()
    {
        _enemyType = ESecondWorldEnemyType.Zombie;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    Enemy IFactory<Enemy>.MakeObject()
    {
        GameObject zombie = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return zombie.GetComponent<Enemy>();
    }
}