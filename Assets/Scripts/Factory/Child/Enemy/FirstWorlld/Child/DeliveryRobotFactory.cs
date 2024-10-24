using UnityEngine;

public class DeliveryRobotFactory : FirstWorldEnemyFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _enemyType = EFirstWorldEnemyType.DeliveryRobot;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject deliveryRobot = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return deliveryRobot;
    }
}