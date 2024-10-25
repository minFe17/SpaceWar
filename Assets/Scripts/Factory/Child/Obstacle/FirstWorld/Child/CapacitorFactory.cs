using UnityEngine;

public class CapacitorFactory : FirstWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EFirstWorldObstacleType.Capacitor;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject capacitor = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return capacitor;
    }
}