using UnityEngine;

public class ContainerFactory : FirstWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EFirstWorldObstacleType.Container;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject container = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return container;
    }
}