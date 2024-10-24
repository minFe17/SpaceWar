using UnityEngine;

public class TurretFactory : FirstWorldEnemyFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _enemyType = EFirstWorldEnemyType.Turret;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject turret = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return turret;
    }
}