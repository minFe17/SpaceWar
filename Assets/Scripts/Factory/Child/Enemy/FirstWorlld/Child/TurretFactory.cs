using UnityEngine;

public class TurretFactory : FirstWorldEnemyFactoryBase, IFactory<Enemy>
{
    protected override void Init()
    {
        _enemyType = EFirstWorldEnemyType.Turret;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    Enemy IFactory<Enemy>.MakeObject()
    {
        GameObject turret = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return turret.GetComponent<Enemy>();
    }
}