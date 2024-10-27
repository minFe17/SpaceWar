using UnityEngine;

public class DeliveryRobotFactory : FirstWorldEnemyFactoryBase, IFactory<Enemy>
{
    protected override void Init()
    {
        _enemyType = EFirstWorldEnemyType.DeliveryRobot;
        _factoryManager.EnemyFactory.AddFactory(_enemyType, this);
    }

    Enemy IFactory<Enemy>.MakeObject()
    {
        GameObject deliveryRobot = _enemyObjectPool.EnemyPool.Push(_enemyType, _prefab);
        return deliveryRobot.GetComponent<Enemy>();
    }
}