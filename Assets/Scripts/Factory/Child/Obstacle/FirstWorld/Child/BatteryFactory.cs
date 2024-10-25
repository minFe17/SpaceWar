using UnityEngine;

public class BatteryFactory : FirstWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EFirstWorldObstacleType.Battery;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject battery = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return battery;
    }
}